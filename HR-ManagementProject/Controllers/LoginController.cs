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
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;

namespace HR_ManagementProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAdminService adminManager;
        private readonly IUserService userManager;
        private readonly IEmployeeService employeeManager;
        private readonly IPermissionService permissionManager;
        private readonly ICompanyService companyManager;
        private readonly IPackageService packageManager;
        private readonly IWalletService walletManager;
        private readonly IAdvancePaymentService advancePaymentService;
        private readonly IExpenseService expenseService;

        public LoginController(IAdminService adminManager, IUserService userService,IEmployeeService employeeService,IPermissionService permissionService,IAdvancePaymentService advancePaymentService,IExpenseService expenseService, ICompanyService companyManager,IPackageService packageManager, IWalletService walletManager)
        { 
            this.adminManager = adminManager;
            this.userManager = userService;
            this.employeeManager = employeeService;
            this.permissionManager = permissionService;
            this.advancePaymentService = advancePaymentService;
            this.companyManager = companyManager;
            this.packageManager = packageManager;
            this.walletManager = walletManager;
            this.expenseService = expenseService;

            foreach ( Package item in packageManager.GetAll())
            {
                if (item.StartDate < DateTime.Now)
                {
                    item.PackageStatus = true;
                    packageManager.Update(item);
                }
            }
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
        public async Task<IActionResult> Login(string email, string password)
        {
            var userAdmin = adminManager.GetByEmailAndPassword(email, password);
            var user = userManager.GetByEmailAndPassword(email, password);
            var employee=employeeManager.GetByEmailAndPassword(email, password);

            
            //var permissions = permissionManager.GetAllWaitingPermission().Where(x => x.Employee.CompanyId == user.CompanyId);




            if (userAdmin != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, userAdmin.Email),
                    new Claim(ClaimTypes.Role,"Admin")
                    

                };

                var userIdentity=new ClaimsIdentity(claims,"Login");
                ClaimsPrincipal principal=new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                HttpContext.Session.SetString("email", userAdmin.Email);
                HttpContext.Session.SetString("id", userAdmin.Id.ToString());
                HttpContext.Session.SetString("name", userAdmin.FirstName.ToString());
                HttpContext.Session.SetString("surname", userAdmin.LastName.ToString());
                
                
                if (userAdmin.ProfilePictureName != null)
                {
                    HttpContext.Session.SetString("photoPath", userAdmin.ProfilePictureName.ToString());
                }
                else HttpContext.Session.SetString("photoPath", "default");
                TempData["welcome"] = "Hoşgeldin " + userAdmin.FirstName + "!";
                return RedirectToAction("Index", "Home");
            }
            else if (user != null && user.Role == "Manager")
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role,"Manager")

                };

                var userIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                var permissionCount = userManager.GetManagerWaitingPermissions(user.CompanyId).Count();
                var advancePaymentCount = advancePaymentService.GetAllWaitingAdvancePayments(user.CompanyId).Count();
                var expensesCount = expenseService.GetAllWaitingExpensesWithEmployees(user.CompanyId).Count();
                var package = companyManager.GetpackagesByCompanyID(user.CompanyId);
                var wallet = walletManager.GetWalletWithCompany(user.CompanyId);

                HttpContext.Session.SetString("email", user.Email);
                HttpContext.Session.SetString("id", user.Id.ToString());
                HttpContext.Session.SetString("name", user.FirstName.ToString());
                HttpContext.Session.SetString("surname", user.LastName.ToString());
                HttpContext.Session.SetString("title", user.JobTitle.ToString());
                HttpContext.Session.SetString("CompanyId", user.CompanyId.ToString());
                HttpContext.Session.SetString("role", user.Role.ToString());
                HttpContext.Session.SetString("MessageCount", permissionCount.ToString());
                HttpContext.Session.SetString("AdvancePaymentCount", advancePaymentCount.ToString());
                HttpContext.Session.SetString("ExpensesCount", expensesCount.ToString());
                HttpContext.Session.SetString("Package", package.ToString());
                HttpContext.Session.SetString("WalletBalance", wallet.Balance.ToString());
                

                if (user.PhotoPath != null)
                {
                    HttpContext.Session.SetString("photoPath", user.PhotoPath.ToString());
                }else HttpContext.Session.SetString("photoPath", "default");
                TempData["welcome"] = "Hoşgeldin " + user.FirstName + "!";
                return RedirectToAction("Index", "Home");
            }
            else if (employee != null && employee.Role == "Employee")
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, employee.Email),
                    new Claim(ClaimTypes.Role,"Employee")

                };

                var userIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                if (employee.IsFirstTime == false)
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(employee.Email);
                    mail.From = new MailAddress("humanresourcesprojectmvc@gmail.com");
                    mail.Subject = "Şifre Yenile";
                    mail.Body = "Hoşgeldin " + employee.FirstName + "," + "</br>" + "Şifreniz: " + employee.Password+ "</br>" + "Şifrenizizi değiştirmek için lütfen aşağıdaki linke tıklayınız. </br>"+ "http://localhost:45529/Employee/Employee/ChangePassword";
                    mail.IsBodyHtml = true;

                    SmtpClient client = new SmtpClient();
                    client.Credentials = new NetworkCredential("humanresourcesprojectmvc@gmail.com", "jvfypcjvedzbhwhh");
                    client.Port = 587;
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;

                    employee.IsFirstTime = true;
                    employeeManager.Update(employee);

                    try
                    {
                        client.Send(mail);
                        TempData["Message"] = "Gönderildi.";
                    }
                    catch (Exception ex)
                    {

                        TempData["Message"] = "Hata Var; " + ex.Message;
                    }
                }

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
                TempData["Id"] = employee.Id;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = ExceptionMessages.loginFailed;
                return RedirectToAction("Index", "Login");
            }
            

        }
        
       public async Task<IActionResult> LogOut()
        {
            HttpContext.Session.Remove("email");
            HttpContext.Session.Remove("id");
            HttpContext.Session.Remove("name");
            HttpContext.Session.Remove("surname");
            HttpContext.Session.Remove("CompanyId");
            HttpContext.Session.Remove("role");
            HttpContext.Session.Remove("MessageCount");
            HttpContext.Session.Remove("title");
            HttpContext.Session.Remove("photoPath");
            HttpContext.Session.Remove("Package");
            HttpContext.Session.Remove("Wallet");

            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Login");
        }
    }
}
