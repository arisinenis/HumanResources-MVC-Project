using HumanResources.BLL.Abstract;
using HumanResources.Core.Entities;
using HumanResources.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_ManagementProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService companyManager;
        private readonly IPackageService packageService;
        private readonly ApplicationDbContext _context;

        public CompanyController(ICompanyService companyManager, IPackageService packageService, ApplicationDbContext context)
        {
            this._context = context;
            this.companyManager = companyManager;
            this.packageService = packageService;
        }

        // GET: Company
        public async Task<IActionResult> Index()
        {
            var list = _context.Companies.Include(p => p.Package);
            return View(await list.ToListAsync());
        }

        // GET: Company/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var company = companyManager.GetById(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Company/Create
        public IActionResult Create()
        {
            var packageData = new SelectList(_context.Packages, "Id", "Name");
            ViewData["PackagesData"] = packageData;

            return View(new Company());
        }

        // POST: Company/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company company)
        {

            if (ModelState.IsValid)
            {
                companyManager.Add(company);
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Company/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var company = companyManager.GetById(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Company/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    companyManager.Update(company);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
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

            return View(company);
        }

        // GET: Company/Delete/5
        public async Task<IActionResult> Delete(int id)
        {


            var company = companyManager.GetById(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = companyManager.GetById(id);
            companyManager.Delete(company);
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return companyManager.Exists(id);
        }
    }
}
