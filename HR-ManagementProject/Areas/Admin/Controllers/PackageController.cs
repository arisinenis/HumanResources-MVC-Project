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
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace HR_ManagementProject.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class PackageController : Controller
    {
        private readonly IPackageService _packageManager;
        private readonly IMapper mapper;

        public PackageController(IPackageService packageManager, IMapper mapper)
        {
            _packageManager = packageManager;
        }

        // GET: Package
        public async Task<IActionResult> Index()
        {
            return View(_packageManager.GetAll());
        }

        // GET: Package/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var package = _packageManager.GetById(id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // GET: Package/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Package/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Package package)
        {
            if (ModelState.IsValid)
            {

                package.StartDate = DateTime.Now;
                if (package.StartDate < DateTime.Now)
                {
                    package.PackageStatus = false;
                }else
                package.PackageStatus = true;
                _packageManager.Add(package);
                return RedirectToAction(nameof(Index));
            }
            return View(package);
        }

        // GET: Package/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var package = _packageManager.GetById(id);
            Package packagedb = _packageManager.GetAll().Where(x => x.Id == id)
            .Select(x => new Package
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                PurchaseDate = x.PurchaseDate,
                Cost = x.Cost,
                UsageAmount = x.UsageAmount,
                PhotoPath = x.PhotoPath,
                Companies = x.Companies
            }).FirstOrDefault();

            if (packagedb == null)
            {
                return NotFound();
            }

            return View(packagedb);
        }

        // POST: Package/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Package package)
        {
            if (id != package.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _packageManager.Update(package);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackageExists(package.Id))
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
            return View(package);
        }

        // GET: Package/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var package = _packageManager.GetById(id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // POST: Package/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var package = _packageManager.GetById(id);
            _packageManager.Delete(package);
            return RedirectToAction(nameof(Index));
        }

        private bool PackageExists(int id)
        {
            return _packageManager.Exists(id);
        }
    }
}
