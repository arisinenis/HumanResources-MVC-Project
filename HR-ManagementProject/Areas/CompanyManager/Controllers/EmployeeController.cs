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

namespace HR_ManagementProject.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager")]
    [Route("CompanyManager/[controller]/[action]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper mapper;

        public EmployeeController(IEmployeeService employeeManager, IWebHostEnvironment hostEnvironment)
        {
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
            if (ModelState.IsValid)
            {
                _employeeManager.CreateCMAreaEmployee(employee);
                return RedirectToAction(nameof(Index));
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
            if (id != employee.Id)
            {
                return NotFound();
            }

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
