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

        public IActionResult Index()
        {           
                
            //foreach (var employee in ReadCSVFile.ReadCSV(Path.GetFullPath(file.FileName)))
            //{
            //    _repository.CreateItem(employee);                
            //}            
               
            return View(_repository.GetAllItems());
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
