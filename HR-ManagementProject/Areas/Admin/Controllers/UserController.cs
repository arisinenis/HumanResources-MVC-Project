using HumanResources.BLL.Abstract;
using HumanResources.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HR_ManagementProject.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            var userData = new SelectList(companyService.GetAll().ToList(), "Id", "Name");
            ViewData["UserData"] = userData;
            return View(new User());
        }

        // POST: CompanyAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,SecondName,LastName,Email,Address,PhoneNumber,Password,CitizenNo,BirthDate,BloodType,JobTitle,Profession,Photo,CompanyId,AdminId,Id")] User person)
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
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,SecondName,LastName,Email,Address,PhoneNumber,Password,CitizenNo,BirthDate,BloodType,JobTitle,Profession,Photo,CompanyId,AdminId,Id")] User person)
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
