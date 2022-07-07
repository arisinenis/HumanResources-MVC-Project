using HumanResources.BLL.Abstract;
using HumanResources.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HR_ManagementProject.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserService userManager;
        private readonly ICompanyService companyService;
        

        public UserController(IUserService userManager, ICompanyService companyService)
        {
            this.userManager = userManager;
            this.companyService = companyService;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(userManager.GetAll());
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var companyAdmin = userManager.GetById(id);
            if (id == null)
            {
                return NotFound();
            }

            return View(companyAdmin);
        }

        // GET: CompanyAdmin/Create
        public IActionResult Create()
        {
            
            ViewData["UserData"] = new SelectList(companyService.GetAll(), "Id", "Name");
            return View(new User());
        }

        // POST: CompanyAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User person)
        {
            var company = companyService.GetById(person.CompanyId);
            if (ModelState.IsValid)
            {
                
                if (userManager.Add(person))
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(person.Email);
                    mail.From = new MailAddress("humanresourcesprojectmvc@gmail.com");
                    mail.Subject = "Human Resources Project";
                    mail.Body = "Hoşgeldin " + person.FirstName + "," + "</br>" + "Şifreniz: " + person.Password;
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

                    company.PersonelSayisi += 1;
                    companyService.Update(company);
                    userManager.Add(person);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["YasHatasi"] = "Yönetici olabilmek için 18 yaşından büyük olmalısınız.";
                    ViewData["UserData"] = new SelectList(companyService.GetAll(), "Id", "Name");
                    return View(person);
                }
                
                
            }
            ViewData["UserData"] = new SelectList(companyService.GetAll(), "Id", "Name");
            return View(person);
        }

        // GET: CompanyAdmin/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var person = userManager.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: CompanyAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User person /*[Bind("FirstName,SecondName,LastName,Email,Address,PhoneNumber,Password,CitizenNo,BirthDate,BloodType,JobTitle,Profession,Photo,CompanyId,AdminId,Id")]*/)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    person.Company = companyService.GetById(id);
                    userManager.Update(person);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            return View(person);
        }

        // GET: CompanyAdmin/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var person = userManager.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: CompanyAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var person = userManager.GetById(id);
            var company = companyService.GetById(person.CompanyId);
            company.PersonelSayisi -= 1;
            companyService.Update(company);
            userManager.Delete(person);
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return userManager.Exists(id);
        }
    }
}
