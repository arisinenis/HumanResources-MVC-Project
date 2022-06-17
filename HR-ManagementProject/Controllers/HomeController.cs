using HumanResources.BLL.Abstract;
using HR_ManagementProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HR_ManagementProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPackageService packageManager;

        public HomeController(IPackageService packageManager)
        {
            this.packageManager = packageManager;
        }

        public IActionResult Index()
        {
            return View(packageManager.GetAll());
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
