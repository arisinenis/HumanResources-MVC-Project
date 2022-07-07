using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HumanResources.Core.Entities;
using HumanResources.DAL.Context;
using HumanResources.BLL.Abstract;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace HR_ManagementProject.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager"), Authorize(Roles = "Manager")]
    [Route("CompanyManager/[controller]/[action]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeManager;
        private readonly ICompanyService _companyManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper mapper;

        public EmployeeController(IEmployeeService employeeManager, IWebHostEnvironment hostEnvironment, ICompanyService companyManager)
        {
            _companyManager = companyManager;
            _employeeManager = employeeManager;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: CompanyManager/Employee
        public async Task<IActionResult> Index()
        {
            return View(_employeeManager.GetAll());
        }

        // GET: CompanyManager/Employee/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var employee = _employeeManager.GetById(id);
            return View(employee);
        }

        // GET: CompanyManager/Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompanyManager/Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HumanResources.Core.Entities.Employee employee)
        {
            var company = _companyManager.GetById(Convert.ToInt32(HttpContext.Session.GetString("CompanyId")));
            

            employee.CompanyId = Convert.ToInt32(HttpContext.Session.GetString("CompanyId"));

            employee.Email = employee.FirstName.ToLower() + "." + employee.LastName.ToLower() + "@" + company.Name.ToLower() + "." + "com";
            employee.Password = company.Name + "123"; //Default bir şifre
            employee.CompanyId =company.Id;
            employee.Status =true;
            company.PersonelSayisi += 1 ;
            _companyManager.Update(company);
            if (ModelState.IsValid)
            {
                if (_employeeManager.Add(employee))
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(employee.Email);
                    mail.From = new MailAddress("humanresourcesprojectmvc@gmail.com");
                    mail.Subject = "Merhaba Human Resources Programıza Hoşgeldiniz" +  employee.Company.Name.ToLower() + "Firması olarak seni aramızda görmekten çok mutluyuz. ";
                    mail.Body = "Hoşgeldin " + employee.FirstName + "," + "</br>" + "Şifreniz: " + employee.Password;
                    mail.IsBodyHtml = true;

                    SmtpClient client = new SmtpClient();
                    client.Credentials = new NetworkCredential("humanresourcesprojectmvc@gmail.com", "jvfypcjvedzbhwhh");
                    client.Port = 587;
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;

                    try
                    {
                        client.Send(mail);
                        TempData["Message"] = "Gönderildi.";
                    }
                    catch (Exception ex)
                    {

                        TempData["Message"] = "Hata Var; " + ex.Message;
                    }
                    return RedirectToAction(nameof(Index));
                }else
                {
                    ViewData["YasHatasi"] = "Yönetici olabilmek için 18 yaşından büyük olmalısınız.";
                    return View(employee);
                }
                
            }
            return View(employee);
        }

        // GET: CompanyManager/Employee/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var employee = _employeeManager.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: CompanyManager/Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HumanResources.Core.Entities.Employee employee)
        {
            var company = _companyManager.GetById(Convert.ToInt32(HttpContext.Session.GetString("CompanyId")));
            if (id != employee.Id)
            {
                return NotFound();
            }
            employee.Email = employee.FirstName + "." + employee.LastName + "@" + company.Name + "." + "com";
            employee.Password = company.Name + "123"; //Default bir şifre
            employee.CompanyId = company.Id;
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeManager.Update(employee);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        

        // GET: CompanyManager/Employee/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var employee = _employeeManager.GetById(id);
            var company = _companyManager.GetById(Convert.ToInt32(HttpContext.Session.GetString("CompanyId")));
            company.PersonelSayisi -= 1;
            _companyManager.Update(company);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: CompanyManager/Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = _employeeManager.GetById(id);
            _employeeManager.Delete(employee);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
        return _employeeManager.Exists(id);
        }
    }
}
