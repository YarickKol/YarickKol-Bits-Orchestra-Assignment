using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReadCSV_App.Models;
using ReadCSV_App.Repositories;
using ReadCSV_App.Services;

namespace ReadCSV_App.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeRepository _repository;
        IWebHostEnvironment _appEnvironment;

        public HomeController(ILogger<HomeController> logger, IEmployeeRepository repository,
            IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _repository = repository;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index(string sortOrder)
        {

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PhoneSortParm"] = sortOrder == "Phone" ? "phone_desc" : "Phone";
            ViewData["SalarySortParm"] = sortOrder == "Salary" ? "salary_desc" : "Salary";
            ViewData["MarriedSortParm"] = sortOrder == "Married" ? "married_desc" : "Married";
            ViewData["BirthSortParm"] = sortOrder == "Birth" ? "birth_desc" : "Birth";
            var li = from s in _repository.GetAllItems()
                     select s;

            switch (sortOrder)
            {
                case "name_desc":
                    li = li.OrderByDescending(s => s.Name);
                    break;
                case "Phone":
                    li = li.OrderBy(s => s.Phone);
                    break;
                case "phone_desc":
                    li = li.OrderByDescending(s => s.Phone);
                    break;
                case "Salary":
                    li = li.OrderBy(s => s.Salary);
                    break;
                case "salary_desc":
                    li = li.OrderByDescending(s => s.Salary);
                    break;
                case "Birth":
                    li = li.OrderBy(s => s.DateOfBirth);
                    break;
                case "birth_desc":
                    li = li.OrderByDescending(s => s.Phone);
                    break;
                case "Married":
                    li = li.OrderBy(s => s.Married);
                    break;
                case "married_desc":
                    li = li.OrderByDescending(s => s.Married);
                    break;
                default:
                    li = li.OrderBy(s => s.Name);
                    break;
            }
            return View(li);
        }

        [HttpPost]
        public IActionResult OpenFile(IFormFile file)
        {
            
            if (file != null)
            {
                string path = Path.GetFullPath(file.FileName);
                foreach (var employee in ReadCSVFile.ReadCSV(path))
                {
                    _repository.CreateItem(employee);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
