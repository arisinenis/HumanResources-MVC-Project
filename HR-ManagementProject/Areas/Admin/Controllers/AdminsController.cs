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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace HumanResources.Controllers
{
    [Area("Admin"),Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class AdminsController : Controller
    {
        private readonly IAdminService _adminManager;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IMapper mapper;

        public AdminsController(IAdminService adminManager, IMapper mapper,IWebHostEnvironment hostEnvironment)
        {
            _adminManager = adminManager;
            this.hostEnvironment = hostEnvironment;
        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            return View(_adminManager.GetAll());
        }
        
        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var admin = _adminManager.GetById(id);
            return View(admin);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Admin admin)
        {
            if (ModelState.IsValid)
            {
                _adminManager.Add(admin);
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var admin = _adminManager.GetById(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Admin admin)
        {
            if (id != admin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _adminManager.Update(admin);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Id))
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
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var admin = _adminManager.GetById(id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = _adminManager.GetById(id);
            _adminManager.Delete(admin);
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
            return _adminManager.Exists(id);
        }
    }
}
