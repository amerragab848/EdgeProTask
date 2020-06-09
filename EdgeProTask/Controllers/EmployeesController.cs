using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DLL.Models;
using DLL.Repository;
using EdgeProTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace EdgeProTask.Controllers
{
    public class EmployeesController : Controller
    {
        private IRepository<Employee> repoEmployee;
        private IRepository<EmployeeData> repoEmployeeData;
        public EmployeesController(IRepository<Employee> repoEmployee, IRepository<EmployeeData> repoEmployeeData)
        {
            this.repoEmployee = repoEmployee;
            this.repoEmployeeData = repoEmployeeData;
        }
        public IActionResult Index()
        {
            List<EmployeeListingVM> model = new List<EmployeeListingVM>();
            repoEmployee.GetAll().ToList().ForEach(a => {
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
        public PartialViewResult AddEmployee()
        {
            EmployeeEmployeeData model = new EmployeeEmployeeData();
            return PartialView("_AddEmployee", model);
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
            repoEmployee.Insert(employee);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditEmployee(long id)
        {
            EmployeeVM model = new EmployeeVM();
            Employee employee = repoEmployee.Get(id);
            if (employee != null)
            {
                model.Name = employee.Name;
                model.SSN = employee.SSN;
              
            }
            return PartialView("_EditEmployee", model);
        }
        public IActionResult TempIndex()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EditEmployee(long id, EmployeeVM model)
        {
            Employee author = repoEmployee.Get(id);
            if (author != null)
            {
                author.Name = model.Name;
                author.SSN = model.SSN;
                author.ModifiedDate = DateTime.UtcNow;
                repoEmployee.Update(author);
            }
            return RedirectToAction("Index");
        }
        public IActionResult TreesEmployee()
        {
            var res=  GetAllEmployeeForTree();

            ViewBag.Tree = res;
            return View();
        }

        /////////////////
        ///
        public string GetAllEmployeeForTree()
        {
            List<EmployeeTreeNodeVM> Employees = new List<EmployeeTreeNodeVM>();
            var data = repoEmployee.GetAll().ToList();

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
                               + "<a href=\"/Product/ListProduct?cat=" + item.EmployeeId + "\" class=\"dropdown-toggle\" data-hover=\"dropdown\" data-toggle=\"dropdown\">" + item.EmployeeName + "</a>";

                    down1_names = "";
                    foreach (var down1 in item.Children)
                    {
                        down2_names = "";
                        foreach (var down2 in down1.Children)
                        {
                            down2_names += "<li><a href=\"/Product/ListProduct?cat=" + down2.EmployeeId + "\">" + down2.EmployeeName + "</a></li>";
                        }
                        down1_names += "<div class=\"col-md-2 col-sm-6\">"
                                        + "<h3 class=\"mega-menu-heading\"><a href=\"/Product/ListProduct?cat=" + down1.EmployeeId + "\">" + down1.EmployeeName + "</a></h3>"
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