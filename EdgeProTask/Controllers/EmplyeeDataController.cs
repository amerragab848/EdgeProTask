using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DLL.Models;
using DLL.Repository;
using EdgeProTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EdgeProTask.Controllers
{
    public class EmplyeeDataController : Controller
    {
        private IRepository<Employee> repoEmployee;
        private IRepository<EmployeeData> repoEmployeeData;
        public EmplyeeDataController(IRepository<Employee> repoEmployee, IRepository<EmployeeData> repoEmployeeData)
        {
            this.repoEmployee = repoEmployee;
            this.repoEmployeeData = repoEmployeeData;
        }

        [HttpGet]
        public PartialViewResult AddEmployeeData(long id)
        {
            EmployeeDataVM model = new EmployeeDataVM();
            return PartialView("_AddEmployeeData", model);
        }
        [HttpPost]
        public IActionResult AddEmployeeData(int id, EmployeeDataVM model)
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
            repoEmployeeData.Insert(book);
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            List<EmployeeDataListingVM> model = new List<EmployeeDataListingVM>();
            repoEmployeeData.GetAll().ToList().ForEach(b => {
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
                Employee author = repoEmployee.Get(b.EmployeeId);
                employeeData.EmployeeName =author.Name ;
                model.Add(employeeData);
            });
            return View("Index", model);
        }
        public PartialViewResult EditEmployeeData(long id)
        {
            EmployeeDataVM model = new EmployeeDataVM();
            model.Employees = repoEmployee.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToList();
            EmployeeData emplyee = repoEmployeeData.Get(id);
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
            return PartialView("_EditEmployeeData", model);
        }
        [HttpPost]
        public ActionResult EditEmployeeData(long id, EmployeeDataVM model)
        {
            EmployeeData employee = repoEmployeeData.Get(id);
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
                repoEmployeeData.Update(employee);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public PartialViewResult DeleteEmployeeData(long id)
        {
            EmployeeData employee = repoEmployeeData.Get(id);
            return PartialView("_DeleteEmployeeData", employee?.Employee.Name);
        }
        [HttpPost]
        public ActionResult DeleteEmployeeData(long id, FormCollection form)
        {
            EmployeeData employee = repoEmployeeData.Get(id);
            if (employee != null)
            {
                repoEmployeeData.Delete(employee);
            }
            return RedirectToAction("Index");
        }
    }
}