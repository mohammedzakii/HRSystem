using HRSystem.Models;
using HRSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Microsoft.Data.SqlClient;


namespace HRSystem.Controllers
{
    public class AttentanceReportController : Controller
    {
        IDepartmentRepository departmentRepository;
        IEmployeeRepository employeeRepository;
        IAttentanceReportRepository attentanceReportRepository;
        public AttentanceReportController(IDepartmentRepository department, IAttentanceReportRepository attentance, IEmployeeRepository employee)
        {
            departmentRepository = department;
            employeeRepository = employee;
            attentanceReportRepository = attentance;
        }

        public IActionResult Index(int ID=0,string EmpName = null, int DeptID = 0, System.DateTime DateFrom = new System.DateTime(), System.DateTime DateTo = new System.DateTime())
        {           //Get All Reports with filtring

            List<AttendanceReport> reports = attentanceReportRepository.GetAll();
            List<Department> departments = departmentRepository.GetAll();
            string namerr = "";
            if (EmpName != null)
            {
                reports = reports.Where(e => e.Employee.Name.Contains(EmpName)).ToList();
                if (reports.Count == 0)
                {
                    namerr = "Enter Valid Name";
                }
            }

            if (DeptID != 0)
            {
                reports = reports.Where(e => e.Employee.DeptID == DeptID).ToList();
            }
            string msg = "";
            if (DatePeriod(DateFrom, DateTo))
            {
                reports = reports.Where(r => r.Date >= DateFrom && r.Date <= DateTo).ToList();
            }
            else if(DateFrom.Year != 1) { msg = "Enter Valid Date "; }
            ViewData["EmpName"] = EmpName;
            ViewData["DeptId"] = DeptID;
            ViewData["DateFrom"] = DateFrom;
            ViewData["DateTo"] = DateTo;
            ViewData["Depts"] = departments;
            ViewData["msg"] = msg;
            ViewData["namerr"] = namerr;
            ViewData["id"] = ID;
            return View(reports);
        }


        public IActionResult Create()
        {
            List<Department> departments = departmentRepository.GetAll();//context.departments.ToList();
            ViewData["Depts"] = departments;

            List<Employee> employees = employeeRepository.GetAll();//context.employees.ToList();
            ViewData["emps"] = employees;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAttentance(AttendanceReport attendance)
        {
            if (ModelState.IsValid && attendance.EmpId != 0 && attendance.Date.Year >=2008)
            {
                attentanceReportRepository.Insert(attendance);
                return RedirectToAction("Index");
            }
            return RedirectToAction("create");

        }

        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                attentanceReportRepository.Delete(id);
            }
            return RedirectToAction("Index");
        }

        //Edit Attendance 
        public IActionResult Edit(int id)
        {
            AttendanceReport att = attentanceReportRepository.GetById(id);
            List<Department> departments = departmentRepository.GetAll();
            ViewData["Depts"] = departments;

            List<Employee> employees = employeeRepository.GetAll();
            ViewData["emps"] = employees;
            return View(att);
        }
        public IActionResult SaveEdit(int id, AttendanceReport newattendance)
        {
            if (ModelState.IsValid == true)
            {
                attentanceReportRepository.Update(id, newattendance);
                return RedirectToAction("Index");
            }

            List<Department> departments = departmentRepository.GetAll();
            ViewData["Depts"] = departments;

            List<Employee> employees = employeeRepository.GetAll();
            ViewData["emps"] = employees;

            return View("Edit", newattendance);

        }


        /// ----------Exce; -----------------------------------//
        /// 
        //public IActionResult Excel()
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> ImportExcelFile(IFormFile FormFile)
        {
            //get file name
            var filename = ContentDispositionHeaderValue.Parse(FormFile.ContentDisposition).FileName.Trim('"');
            //get path
            var MainPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");
            //create directory "Uploads" if it doesn't exists
            if (!Directory.Exists(MainPath))
            {
                Directory.CreateDirectory(MainPath);
            }
            //get file path 
            var filePath = Path.Combine(MainPath, FormFile.FileName);
            using (System.IO.Stream stream = new FileStream(filePath, FileMode.Create))
            {
                await FormFile.CopyToAsync(stream);
            }

            //get extension
            string extension = Path.GetExtension(filename);


            string conString = string.Empty;

            switch (extension)
            {
                case ".xls": //Excel 97-03.
                    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                    break;
                case ".xlsx": //Excel 07 and above.
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                    break;
            }

            DataTable dt = new DataTable();
            conString = string.Format(conString, filePath);

            using (OleDbConnection connExcel = new OleDbConnection(conString))
            {
                using (OleDbCommand cmdExcel = new OleDbCommand())
                {
                    using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                    {
                        cmdExcel.Connection = connExcel;

                        //Get the name of First Sheet.
                        connExcel.Open();
                        DataTable dtExcelSchema;
                        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        connExcel.Close();

                        //Read Data from First Sheet.
                        connExcel.Open();
                        cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                        odaExcel.SelectCommand = cmdExcel;
                        odaExcel.Fill(dt);
                        connExcel.Close();
                    }
                }
            }
            //your database connection string
            conString = @"Data Source=.;Initial Catalog=HrSystem;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    //Set the database table name.
                    sqlBulkCopy.DestinationTableName = "dbo.attendanceReports";
                    sqlBulkCopy.ColumnMappings.Add("Id", "Id");
                    sqlBulkCopy.ColumnMappings.Add("LeavingTime", "LeavingTime");
                    sqlBulkCopy.ColumnMappings.Add("AttendanceTime", "AttendanceTime");
                    sqlBulkCopy.ColumnMappings.Add("Date", "Date");
                    sqlBulkCopy.ColumnMappings.Add("EmpId", "EmpId");
                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }
            //if the code reach here means everthing goes fine and excel data is imported into database
            List<Department> departments = departmentRepository.GetAll();
            List<AttendanceReport> reports = attentanceReportRepository.GetAll();

            ViewBag.Message = "File Imported and excel data saved into database";
            ViewData["EmpName"] = "";
            ViewData["DeptId"] = 0;
            ViewData["DateFrom"] = new System.DateTime();
            ViewData["DateTo"] = new System.DateTime();
            ViewData["Depts"] = departments;
            ViewData["msg"] = "";
            ViewData["namerr"] = "";
            return View("Index", reports);
        }
        //// -------------EndExel-------------//
        ///


        /// ------------validation-----------------------------//


        private bool DatePeriod(System.DateTime DateFrom = new System.DateTime(), System.DateTime DateTo = new System.DateTime())
        {
            if (DateFrom.Year < 2008  || DateTo.Year < 2008)
            {
                return false;
            }
            if (DateFrom.Year < DateTo.Year)
            {
                return true;
            }
            else if (DateFrom.Year == DateTo.Year && DateFrom.Month < DateTo.Month)
            {
                return true;
            }
            else if (DateFrom.Year == DateTo.Year && DateFrom.Month == DateTo.Month && DateFrom.Day <= DateTo.Day)
            {
                return true;
            }
            return false;
        }
        public bool testTime(System.DateTime LeavingTime, System.DateTime AttendanceTime)
        {

            if (LeavingTime <= AttendanceTime)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool testDate(System.DateTime Date)
        {

            if (Date <= System.DateTime.Now && Date.Year >=2008)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public IActionResult TestId(int EmpId)
        {
            Employee id = employeeRepository.GetById(EmpId);//context.employees.FirstOrDefault(s => s.Id == EmpId);
            if (id == null)
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }



    }

}
