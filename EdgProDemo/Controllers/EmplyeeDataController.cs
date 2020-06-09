using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLayer.Services;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using DLL.Context;
using EdgProDemo.Models;

namespace EdgProDemo.Controllers
{
    public class EmplyeeDataController : Controller
    {
        private static Repository<Employee> Employee { get; set; }
        private static Repository<EmployeeData> EmployeeData { get; set; }


        EmployeeDataService employeeDataService = new EmployeeDataService();
        EmployeeService employeeService = new EmployeeService();


        [HttpGet]
        public ActionResult AddEmployeeData(long id)
        {
            EmployeeDataVM model = new EmployeeDataVM();
            return View( model);
        }
        [HttpPost]
        public ActionResult AddEmployeeData(int id, EmployeeDataVM model)
        {
            EmployeeData book = new EmployeeData
            {
                EmployeeId = id,

                Address=model.Address,
                State=model.State,
                ZipCode=model.ZipCode,
                 Age=model.Age,
                 City=model.City,
                 Latitude=model.Latitude,
                 Longitude=model.Longitude,
                AddedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            };
            employeeDataService.InsertEmployeeData(book);
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            List<EmployeeDataListingVM> model = new List<EmployeeDataListingVM>();
            employeeDataService.GetAllEmployeesData().ToList().ForEach(b => {
                EmployeeDataListingVM employeeData = new EmployeeDataListingVM
                {
                    Id = b.Id,
                    Address=b.Address,
                    Age=b.Age,
                    City=b.City,
                    Longitude=b.Longitude,
                    Latitude=b.Latitude,
                     State=b.State,
                     ZipCode=b.ZipCode
                };
                Employee employee = employeeService.GetEmployeeById(b.EmployeeId);
                employeeData.EmployeeName = employee.Name ;
                model.Add(employeeData);
            });
            return View("Index", model);
        }
        public ActionResult EditEmployeeData(long id)
        {
            EmployeeDataVM model = new EmployeeDataVM();
            model.Employees = employeeService.GetAllEmployees().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToList();
            EmployeeData emplyee = employeeDataService.GetEmployeeDataById(id);
            if (emplyee != null)
            {
                model.Address = emplyee.Address;
                model.Age = emplyee.Age;
                model.City = emplyee.City;
                model.Id = emplyee.Id;
                model.Latitude = emplyee.Latitude;
                model.Longitude = emplyee.Longitude;
                model.State = emplyee.State;
                model.ZipCode = emplyee.ZipCode;
                
            }
            return View("EditEmployeeData", model);
        }
        [HttpPost]
        public ActionResult EditEmployeeData(long id, EmployeeDataVM model)
        {
            EmployeeData employee = employeeDataService.GetEmployeeDataById(id);
            if (employee != null)
            {
                employee.Address = model.Address;
                employee.Age = model.Age;
                employee.City = model.City;
                employee.Id = model.Id;
                employee.Latitude = model.Latitude;
                employee.Longitude = model.Longitude;
                employee.State = model.State;
                employee.ZipCode = model.ZipCode;
                employee.ModifiedDate = DateTime.UtcNow;
                employee.EmployeeId = (int)model.EmployeeId;
                employeeDataService.UpdateEmployeeData(employee);
            }
            return RedirectToAction("Index");
        }
        //[HttpGet]
        //public ActionResult DeleteEmployeeData(long id)
        //{
        //    EmployeeData employee = employeeDataService.GetEmployeeDataById(id);
        //    return View("DeleteEmployeeData", employee?.Employee.Name);
        //}
        [HttpPost]
        public ActionResult DeleteEmployeeData(long id, FormCollection form)
        {
            EmployeeData employee = employeeDataService.GetEmployeeDataById(id);
            if (employee != null)
            {
                employeeDataService.DeleteEmployeeData(employee);
            }
            return RedirectToAction("Index");
        }
    }
}