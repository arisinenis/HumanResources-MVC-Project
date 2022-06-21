using HumanResources.BLL.Abstract;
using HR_ManagementProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HR_ManagementProject.ViewModels;
using HumanResources.Core.Entities;

namespace HR_ManagementProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPackageService packageManager;
        private readonly IEmployeeService employeeManager;
        private readonly IUserService userManager;

        public HomeController(IPackageService packageManager,IEmployeeService employeeService,IUserService userService)
        {
            this.packageManager = packageManager;
            this.employeeManager = employeeService;
            this.userManager = userService;
        }

        public IActionResult Index()
        {
            
            PackageManagerEmployeeVM packageManagerEmployeeVM = new PackageManagerEmployeeVM();
            packageManagerEmployeeVM.Packages = packageManager.GetAll();
            packageManagerEmployeeVM.Employees = employeeManager.GetAll();
            packageManagerEmployeeVM.Managers = userManager.GetAll();

            
           
            return View(packageManagerEmployeeVM);
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
