using aspfunda.Models;
using aspfunda.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace aspfunda.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
          private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ILogger logger;

        public HomeController(IEmployeeRepository employeeRepository , IHostingEnvironment hostingEnvironment,ILogger<HomeController> logger)
           {
               _employeeRepository = employeeRepository;
               this.hostingEnvironment = hostingEnvironment;
               this.logger = logger;
        }
        [AllowAnonymous]
          public ViewResult Index()
           {
            var model = _employeeRepository.GetAllEmployee();
                return View(model);
           }
        [AllowAnonymous]
        public ViewResult Details(int? id)
        {
            //throw new Exception("Error in Details View");
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");
            Employee employee = _employeeRepository.GetEmployee(id.Value);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("NotFoundPage", id.Value);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id??1),
                PageTitle = "Employee Details"

            };
            
            //ViewData["Employee"] = model;
            //ViewData["PageTitle"] = "Employee Details";

            //ViewBag.Employee = model;
            //ViewBag.PageTitle = "Employee Details";
          
            return View(homeDetailsViewModel);
        }
        [HttpGet]

        public ViewResult Create()
        {
            
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            if (employee == null)
            {
                return View("NotFoundPage");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Delete(DeleteViewModel model)
        {
            var employee = _employeeRepository.Delete(model.Id);
            _employeeRepository.Commit();
            if (employee == null)
            {
                return View("NotFoundPage");
            }
            TempData["Message"] = $"{employee.Name} Deleted";
            return RedirectToAction("index");
        }
        [HttpGet]
        public ViewResult Edit( int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id =employee.Id,
                Name = employee.Name,
                Email=employee.Email,
                Department=employee.Department,
                ExistingPhotoPath= employee.PhotoPath
            };

            return View(employeeEditViewModel);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {

                        String FilePath= Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(FilePath);
                    }
                    employee.PhotoPath = ProcessUploadedFile(model);

                }
                
                _employeeRepository.Update(employee);
                return RedirectToAction("index");
            }
            return View();
        }

        private string ProcessUploadedFile(EmployeeCreateViewmodel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {


                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string FilePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

           return uniqueFileName;
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewmodel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Employee NewEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employeeRepository.Add(NewEmployee);
                 return RedirectToAction("details", new { id = NewEmployee.Id });
            }
            return View();
        }
    }
}
