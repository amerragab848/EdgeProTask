using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer.Services;
using EdgProDemo.Models;
using Microsoft.Reporting.WebForms;

namespace EdgProDemo.Views.Shared
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        EmployeeService employeeService = new EmployeeService();
        EmployeeDataService employeeDataService = new EmployeeDataService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateReportData();
            }
        }
        private void PopulateReportData()
        {
            var employees = employeeService.GetAllEmployees().Where(c => c.ManagerId == 1);
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
                            Address = emp.Address,
                            Age = emp.Age,
                            City = emp.City,
                            Latitude = emp.Latitude,
                            Longitude = emp.Longitude,
                            State = emp.State,
                            ZipCode = emp.ZipCode,
                            Name = item.Name
                        };

                        employeeDataVM.Add(employeeData);

                    }
                }
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rd1 = new ReportDataSource("MyDataSet", employeeDataVM);
                ReportViewer1.LocalReport.DataSources.Add(rd1);
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}