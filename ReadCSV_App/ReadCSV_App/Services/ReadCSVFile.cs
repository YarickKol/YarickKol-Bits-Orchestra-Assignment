using LumenWorks.Framework.IO.Csv;
using Microsoft.AspNetCore.Http;
using ReadCSV_App.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadCSV_App.Services
{
    public  class ReadCSVFile
    {
        public static List<Employee> ReadCSV(string location)
        {
            //string location = @"C:\Users\Yarick\Desktop\Retards.csv";
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(File.OpenRead(location)), true))
            {
                csvTable.Load(csvReader);
            }

            List<Employee> employees = new List<Employee>();
            foreach (DataRow row in csvTable.Rows)
            {
                for (int x = 0; x < csvTable.Columns.Count; x++)
                {
                    string[] values = row[x].ToString().Split(";");
                    employees.Add(new Employee
                    {
                        Name = values[0],
                        DateOfBirth = DateTime.Parse(values[1]),
                        Married = bool.Parse(values[2]),
                        Phone = values[3],
                        Salary = Decimal.Parse(values[4])
                    });
                }

            }
            return employees;
        }
    }
}
