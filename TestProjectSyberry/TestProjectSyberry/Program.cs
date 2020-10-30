using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace TestProjectSyberry
{
    enum Days
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }
    class Program
    {
        static void Main(string[] args)
        {
            var connection = ConfigurationManager.AppSettings["Database1"];
            DataContext db = new DataContext(connection);

            var dailyReports = db.time_reports
                  .GroupBy(w => new { day = System.Data.Entity.SqlServer.SqlFunctions.DatePart("weekday", w.date), emp = w.employee })
                  .Select(g => new
                  {
                      Day = g.Key.day,
                      Name = g.Key.emp.name,
                      AverageHours = g.Average(tr => tr.hours)

                  })
                  .GroupBy(w => w.Day)
                  .SelectMany(x => x.OrderByDescending(y => y.AverageHours).Take(3))
                  .ToList();

            int[] daysIndex = {1,2,3,4,5,6,7 };

            var myJoinWithEmptyDays = from day in daysIndex
                         join report in dailyReports on day equals report.Day into aaaa
                         from bbbb in aaaa.DefaultIfEmpty()
                         select new {Day = day , Name = bbbb?.Name , Average = bbbb?.AverageHours};
            var result = myJoinWithEmptyDays
                .GroupBy(d => d.Day)
                .Select(gr => gr.ToArray())
                .ToArray();


            for (var indexOfDay = 0; indexOfDay < 7; indexOfDay++)
            {
                var currentIndex = (indexOfDay == 6) ? 0 : indexOfDay + 1;




                if (result[currentIndex].FirstOrDefault().Name != null)
                {

                    StringBuilder sb = new StringBuilder("| " + Enum.GetName(typeof(Days), currentIndex) + " |");


                    for (var i = 0; i < result[currentIndex].Count(); i++)
                    {
                        if (result[currentIndex][i].Name != null)
                        {
                            sb.Append(" " + result[currentIndex][i].Name + " (" + Math.Round((decimal)result[currentIndex][i].Average, 2).ToString() + " hours" + "),");
                        }
                    }

                    sb.Remove(sb.Length - 1, 1).Append(" |");
                    Console.WriteLine(sb);
                }
                else
                {
                    StringBuilder sb = new StringBuilder("| " + Enum.GetName(typeof(Days), currentIndex) + " | " + "Nobody worked that day |");
                    Console.WriteLine(sb);
                }
            }
        }
    }
}
