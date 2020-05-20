using System.Data.Entity;
using TestProjectSyberry.Database_Models;

namespace TestProjectSyberry
{
    public class DataContext : DbContext
    {
        static DataContext()
        {
            Database.SetInitializer<DataContext>(new DBInitializer());
        }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Time_report> time_reports { get; set; }
        public DataContext(string connection) : base(connection) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasMany(em =>em.time_reports);

        }
    }
}