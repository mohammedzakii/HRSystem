using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HRSystem.Models
{
    public class HRSystemContext : DbContext
    {
        public HRSystemContext()
        {

        }

        public HRSystemContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> employees { get; set; }
        public DbSet<AttendanceReport> attendanceReports { get; set; }
       
        public DbSet<Department> departments { get; set; }
        public DbSet<GeneralSetting> generalSettings { get; set; }
        public DbSet<OfficialHolidays> officialHolidays { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=HrSystem;Integrated Security=True ");
        }


    }
}
