using HumanResources.BLL.Abstract;
using HumanResources.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HR_ManagementProject.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager"), Authorize(Roles = "Manager")]
    [Route("CompanyManager/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserService userManager;

        public UserController(IUserService userManager)
        {
            this.userManager = userManager;
        }

        // GET: CompanyAdmin
        public async Task<IActionResult> Index()
        {
            var id = HttpContext.Session.GetString("id");
            var companyAdmin = userManager.GetById(Convert.ToInt32(id));
            return View(userManager.GetAll());
        }

        // GET: CompanyAdmin/Details/5
        public async Task<IActionResult> Details()
        {
            var id = HttpContext.Session.GetString("id");
            var companyAdmin = userManager.GetById(Convert.ToInt32(id));
            if (id == null)
            {
                return NotFound();
            }

            return View(companyAdmin);
        }

        // GET: CompanyAdmin/Create
        public IActionResult Create()
        {
            return View(new User());
        }

        // POST: CompanyAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,SecondName,LastName,Email,Address,PhoneNumber,Password,CitizenNo,BirthDate,BloodType,JobTitle,Profession,PhotoPath,CompanyId,AdminId,Id")] User person)
        {
            if (ModelState.IsValid)
            {
                userManager.Add(person);
                return RedirectToAction(nameof(Index));
            }
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
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,SecondName,LastName,Email,Address,PhoneNumber,Password,CitizenNo,BirthDate,BloodType,JobTitle,Profession,PhotoPath,CompanyId,AdminId,Id")] User person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            userManager.Delete(person);
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return userManager.Exists(id);
        }
    }
}
