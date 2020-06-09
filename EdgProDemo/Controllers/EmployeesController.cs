using BusinessLayer.Services;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using DLL.Context;
using EdgProDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EdgProDemo.Controllers
{
    public class EmployeesController : Controller
    {
        // IRepository<Employee> repository;


        EmployeeService employeeService = new EmployeeService();
        EmployeeDataService employeeDataService = new EmployeeDataService();
        public EmployeesController()
        {

        }
        public ActionResult Index()
        {
            List<EmployeeListingVM> model = new List<EmployeeListingVM>();
            employeeService.GetAllEmployees().ToList().ForEach(a => {
                EmployeeListingVM author = new EmployeeListingVM
                {
                    Id = a.Id,
                    Name =a.Name,
                    SSN = a.SSN
                };
                model.Add(author);
            });
            return View("Index", model);
        }
        [HttpGet]
        public ActionResult AddEmployee()
        {
            EmployeeEmployeeData model = new EmployeeEmployeeData();
            return View("AddEmployee", model);
        }
        [HttpPost]
        public ActionResult AddEmployee(EmployeeEmployeeData model)
        {
            Employee employee = new Employee
            {
                Name = model.Name,
                SSN = model.SSN,
                AddedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                EmployeeDatas = new List<EmployeeData> {
                new EmployeeData {
                   
                        AddedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                        Age=model.Age,
                        Address=model.Address,
                        Latitude=model.Latitude,
                        City=model.City,
                        ZipCode=model.ZipCode,
                         Longitude=model.Longitude,
                         State=model.State
                }
            }
            };
            employeeService.InsertEmployee(employee);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditEmployee(long id)
        {
            EmployeeVM model = new EmployeeVM();
            Employee employee = employeeService.GetEmployeeById(id);
            model.Employees = employeeService.GetAllEmployees().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToList();
            if (employee != null)
            {
                model.Name = employee.Name;
                model.SSN = employee.SSN;
              
            }
            return PartialView("EditEmployee", model);
        }
        public ActionResult TempIndex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditEmployee(long id, EmployeeVM model)
        {
            Employee employee = employeeService.GetEmployeeById(id);
            if (employee != null)
            {
                employee.Name = model.Name;
                employee.SSN = model.SSN;
                employee.ManagerId = model.ManagerId;
                employee.ModifiedDate = DateTime.UtcNow;
                employeeService.UpdateEmployee(employee);
            }
            return RedirectToAction("Index");
        }
        public ActionResult TreesEmployee()
        {
            var res=  GetAllEmployeeForTree();

            ViewBag.Tree = res;
            return View();
        }
        public ActionResult ReportViewer(int id)
        {
           var employees=  employeeService.GetAllEmployees().Where(c => c.ManagerId == id);
            var employeesData = employeeDataService.GetAllEmployeesData();
            List<EmployeeEmployeeData> employeeDataVM = new List<EmployeeEmployeeData>();
            foreach (var item in employees)
            {
                foreach (var emp in employeesData)
                {
                    if (emp.Id == item.Id)
                    {
                        EmployeeEmployeeData employeeData = new EmployeeEmployeeData
                        {
                            Address=emp.Address,
                            Age=emp.Age,
                            City=emp.City,
                            Latitude=emp.Latitude,
                            Longitude=emp.Longitude,
                            State=emp.State,
                            ZipCode=emp.ZipCode,
                            Name=item.Name
                        };

                        employeeDataVM.Add(employeeData);

                    }
                }
            }
            return View(employeeDataVM);
        }
        /////////////////
        /// <summary>
        /// publ
        /// </summary>
        /// <returns></returns>
        public string GetAllEmployeeForTree()
        {
            List<EmployeeTreeNodeVM> Employees = new List<EmployeeTreeNodeVM>();
            var data = employeeService.GetAllEmployees().ToList();

            if (data != null )
            {
                foreach (var row in data)
                {
                    Employees.Add(
                        new EmployeeTreeNodeVM
                        {
                           Id=row.Id,
                           Name=row.Name,
                           ManagerId=row.ManagerId

                        });
                }

                List<TreeNode> headerTree = FillRecursive(Employees, null);

                #region BindingHeaderMenus

                string root_li = string.Empty;
                string down1_names = string.Empty;
                string down2_names = string.Empty;

                foreach (var item in headerTree)
                {
                    root_li += "<li class=\"dropdown mega-menu-fullwidth\">"
                               + "<a href=\"/Employees/ReportViewer?id=" + item.EmployeeId + "\" class=\"dropdown-toggle\" data-hover=\"dropdown\" data-toggle=\"dropdown\">" + item.EmployeeName + "</a>";

                    down1_names = "";
                    foreach (var down1 in item.Children)
                    {
                        down2_names = "";
                        foreach (var down2 in down1.Children)
                        {
                            down2_names += "<li><a href=\"/Employees/ReportViewer?id=" + down2.EmployeeId + "\">" + down2.EmployeeName + "</a></li>";
                        }
                        down1_names += "<div class=\"col-md-2 col-sm-6\">"
                                        + "<h3 class=\"mega-menu-heading\"><a href=\"/Employees/ReportViewer?id=" + down1.EmployeeId + "\">" + down1.EmployeeName + "</a></h3>"
                                        + "<ul class=\"list-unstyled style-list\">"
                                        + down2_names
                                        + "</ul>"
                                      + "</div>";
                    }
                    root_li += "<ul class=\"dropdown-menu\">"
                                + "<li>"
                                    + "<div class=\"mega-menu-content\">"
                                        + "<div class=\"container\">"
                                            + "<div class=\"row\">"
                                                + down1_names
                                            + "</div>"
                                        + "</div>"
                                    + "<div>"
                                + "</li>"
                                + "</ul>"
                         + "</li>";
                }
                #endregion

                return "<ul class=\"nav navbar-nav\">" + root_li + "</ul>";
            }
            return "Record Not Found!!";
        }


        private static List<TreeNode> FillRecursive(List<EmployeeTreeNodeVM> flatObjects, int? parentId = null)
        {
            return flatObjects.Where(x => x.ManagerId.Equals(parentId)).Select(item => new TreeNode
            {
                EmployeeName = item.Name,
                EmployeeId = item.Id,
                Children = FillRecursive(flatObjects, item.Id)
            }).ToList();
        }
    }
}