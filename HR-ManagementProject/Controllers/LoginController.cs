using HumanResources.BLL.Abstract;
using HumanResources.Core.Entities;
using HR_ManagementProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;
using HR_ManagementProject.Exceptions;
using System.Linq;

namespace HR_ManagementProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAdminService adminManager;
        private readonly IUserService userManager;
        private readonly IEmployeeService employeeManager;
        private readonly IPermissionService permissionManager;

        public LoginController(IAdminService adminManager, IUserService userService,IEmployeeService employeeService,IPermissionService permissionService)
        {
            this.adminManager = adminManager;
            this.userManager = userService;
            this.employeeManager = employeeService;
            this.permissionManager = permissionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View(new Admin());
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var userAdmin = adminManager.GetByEmailAndPassword(email, password);
            var user = userManager.GetByEmailAndPassword(email, password);
            var employee=employeeManager.GetByEmailAndPassword(email, password);

            
            //var permissions = permissionManager.GetAllWaitingPermission().Where(x => x.Employee.CompanyId == user.CompanyId);




            if (userAdmin != null)
            {
                HttpContext.Session.SetString("email", userAdmin.Email);
                HttpContext.Session.SetString("id", userAdmin.Id.ToString());
                HttpContext.Session.SetString("name", userAdmin.FirstName.ToString());
                HttpContext.Session.SetString("surname", userAdmin.LastName.ToString());
                if (userAdmin.ProfilePictureName != null)
                {
                    HttpContext.Session.SetString("photoPath", userAdmin.ProfilePictureName.ToString());
                }
                HttpContext.Session.SetString("ProfilePictureName", "default");
                TempData["welcome"] = "Hoşgeldin " + userAdmin.FirstName + "!";
                return RedirectToAction("Index", "Home");
            }
            else if (user != null && user.Role == "Manager")
            {
                var permissionCount = userManager.GetManagerWaitingPermissions(user.CompanyId).Count();

                HttpContext.Session.SetString("email", user.Email);
                HttpContext.Session.SetString("id", user.Id.ToString());
                HttpContext.Session.SetString("name", user.FirstName.ToString());
                HttpContext.Session.SetString("surname", user.LastName.ToString());
                HttpContext.Session.SetString("title", user.JobTitle.ToString());
                HttpContext.Session.SetString("CompanyId", user.CompanyId.ToString());
                //HttpContext.Session.SetString("Company", user.Company.Name.ToString());
                HttpContext.Session.SetString("role", user.Role.ToString());
                HttpContext.Session.SetString("MessageCount", permissionCount.ToString());
                if (user.PhotoPath != null)
                {
                    HttpContext.Session.SetString("photoPath", user.PhotoPath.ToString());
                }else HttpContext.Session.SetString("photoPath", "default");
                TempData["welcome"] = "Hoşgeldin " + user.FirstName + "!";
                return RedirectToAction("Index", "Home");
            }
            else if (employee != null && employee.Role == "Employee")
            {
                HttpContext.Session.SetString("email", employee.Email);
                HttpContext.Session.SetString("id", employee.Id.ToString());
                HttpContext.Session.SetString("name", employee.FirstName.ToString());
                HttpContext.Session.SetString("surname", employee.LastName.ToString());
                HttpContext.Session.SetString("CompanyId", employee.CompanyId.ToString());
                HttpContext.Session.SetString("role", employee.Role.ToString());
                if (employee.PhotoPath != null)
                {
                    HttpContext.Session.SetString("photoPath", employee.PhotoPath.ToString());
                }
                else HttpContext.Session.SetString("photoPath", "default");
                TempData["welcome"] = "Hoşgeldin " + employee.FirstName + "!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = ExceptionMessages.loginFailed;
                return RedirectToAction("Index", "Login");
            }
            

        }
        
       public IActionResult LogOut()
        {
            HttpContext.Session.Remove("email");
            HttpContext.Session.Remove("id");
            HttpContext.Session.Remove("name");
            return RedirectToAction("Index", "Login");
        }
    }
}
