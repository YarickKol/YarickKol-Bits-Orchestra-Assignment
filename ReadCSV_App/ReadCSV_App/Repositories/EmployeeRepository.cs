using Microsoft.EntityFrameworkCore;
using ReadCSV_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadCSV_App.Repositories
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly EmployeeDBContext _context; // database context        

        public EmployeeRepository(EmployeeDBContext context)
        {
            _context = context;            
        }

        public void CreateItem(Employee item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if(IsInDatabase(item) == false)
                _context.Entry(item).State = EntityState.Added;
            SaveChanges();
        }

        public IEnumerable<Employee> GetAllItems()
        {
            return _context.Employees.ToList();
        }
              
        public Employee GetItemById(int id)
        {
            return _context.Employees.SingleOrDefault(e => e.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
        private bool IsInDatabase(Employee employee)
        {
            _context.Employees.FirstOrDefault(e => e.Phone == employee.Phone);
            if(_context.Employees.Where(e => e.Phone == employee.Phone).Any())
                return true;
            return false;
        }
    }
}
