using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestProjectSyberry.Database_Models
{
    [Table("employees")]
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Time_report> time_reports { get; set; }
    }
}
