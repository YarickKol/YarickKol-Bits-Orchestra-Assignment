
using ReadCSV_App.Models;
using System.Collections.Generic;

namespace ReadCSV_App.Repositories
{
    public interface IEmployeeRepository
    {
        public void CreateItem(Employee item);

        public IEnumerable<Employee> GetAllItems();

        public Employee GetItemById(int id);

        public bool SaveChanges();
        
    }
}
