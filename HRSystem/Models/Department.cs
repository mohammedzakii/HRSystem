using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Employee> employees { get; set; }   
    }
}
