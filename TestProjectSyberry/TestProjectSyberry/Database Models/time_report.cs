using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestProjectSyberry.Database_Models
{
    [Table("time_reports")]
    public class Time_report
    {
        public int id { get; set; }
        public Employee employee { get; set; }
        public float hours { get; set; }

        [DisplayFormat(DataFormatString = "{0:M/d/YYYY}")]
        public DateTime date { get; set; }

        
    }
}
