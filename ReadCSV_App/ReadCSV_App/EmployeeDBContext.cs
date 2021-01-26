using Microsoft.EntityFrameworkCore;
using ReadCSV_App.Models;

namespace ReadCSV_App
{
    public class EmployeeDBContext:DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public EmployeeDBContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(
           DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EmployeeDb;Trusted_Connection=True;");
        }
    }
}
