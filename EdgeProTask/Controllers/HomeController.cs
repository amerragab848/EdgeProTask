using System;
using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EdgeProTask.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Reporting.WebForms;
using DLL.Repository;
using DLL.Models;

namespace EdgeProTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IRepository<Employee> repoEmployee;
        private IRepository<EmployeeData> repoEmployeeData;
        public HomeController(IRepository<Employee> repoEmployee, IRepository<EmployeeData> repoEmployeeData)
        {
            this.repoEmployee = repoEmployee;
            this.repoEmployeeData = repoEmployeeData;
        }
        //public byte[] ReportHelper<T>(string reportFileName, string dataSetName, EmployeeDataVM employeeDataVM,
        //      out string mimeType, string reportFormat = "PDF", List<ReportParameter> parameters = null)
        //{
        //    mimeType = "tst";

        //    LocalReport lr = new LocalReport
        //    {
        //        EnableExternalImages = true
        //    };
        //    string path = Path.Combine(Server.MapPath("/bin/Reports/"), reportFileName);
        //    if (System.IO.File.Exists(path))
        //    {
        //        lr.ReportPath = path;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //    lr.EnableExternalImages = true;

        //    ReportDataSource rd = new ReportDataSource(dataSetName, employeeDataVM);
        //    lr.DataSources.Add(rd);
        //    if (parameters != null)
        //    {
        //        lr.SetParameters(parameters);
        //    }

        //    string deviceInfo;

        //    deviceInfo = "<DeviceInfo>" +
        //                 "<OutputFormate>" + reportFormat + "</OutputFormate>" +                    
        //                 "</DeviceInfo>";
        //    byte[] renderedBytes;
        //    string encoding;
        //    string fileNameExtension;
        //    string[] streams;
        //    Warning[] warnings;

        //    renderedBytes = lr.Render(reportFormat, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

        //    return renderedBytes;
        //}
        //public IActionResult RentValuePerSiteReport(long id)
        //{
        //    string formate = "PDF";
        //   var emp = repoEmployeeData.Get(id);
        //    EmployeeDataVM employeeDataVM = new EmployeeDataVM { 
        //        Address=emp.Address,
        //        Age=emp.Age,
        //        City=emp.City,
        //        Latitude=emp.Latitude,
        //        Longitude=emp.Longitude,
        //        State=emp.State,
        //        ZipCode=emp.State
        //    };

        //    string fileExt = formate.ToUpper() == "EXCEL" ? "xls" : formate.ToUpper() == "WORD" ? "doc" : "pdf";
        //    string mimeType = "";
        //    byte[] report = ReportHelper<EmployeeDataVM>("RentValuePerSite.rdlc", "AMSDataSet", employeeDataVM, out mimeType, formate);

        //    return File(report, mimeType, $"توزيع القيم الإيجارية وفقاً للجهات التابعة.{DateTime.Now:ddMMyy}.{fileExt}");
        //}
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
