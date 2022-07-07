using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HumanResources.Core.Entities;
using HumanResources.BLL.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace HR_ManagementProject.Areas.Employee.Controllers
{
    [Area("Employee"), Authorize(Roles = "Employee")]
    [Route("Employee/[controller]/[action]")]
    public class AdvancePaymentController : Controller
    {
        
        private readonly IAdvancePaymentService advancePaymentManager;
        private readonly IEmployeeService employeeService;

        public AdvancePaymentController(IAdvancePaymentService advancePaymentManager,IEmployeeService employeeService)
        {
            
            this.advancePaymentManager = advancePaymentManager;
            this.employeeService = employeeService;
        }

        // GET: Employee/AdvancePayment
        public IActionResult Index()
        {
            var id = HttpContext.Session.GetString("id");
            var advancePayments = advancePaymentManager.GetByEmployeeId(Convert.ToInt32(id));
            return View(advancePayments);
        }

        // GET: Employee/AdvancePayment/Details/5
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advancePayment = advancePaymentManager.GetById(id);
            if (advancePayment == null)
            {
                return NotFound();
            }

            return View(advancePayment);
        }

        // GET: Employee/AdvancePayment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/AdvancePayment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdvancePayment advancePayment)
        {

            advancePayment.EmployeeId = Convert.ToInt32(HttpContext.Session.GetString("id"));
            var employeSalary = employeeService.GetById(advancePayment.EmployeeId).Salary;
            if (ModelState.IsValid)
            {
                if (advancePayment.TotalPaymentRequest<employeSalary*12*30/100)
                {
                    advancePaymentManager.Add(advancePayment);

                    return RedirectToAction(nameof(Index));
                }else
                {
                    TempData["hata"] = "Avans talebiniz maaşınızın %30'undan fazla olamaz!";
                    return View(advancePayment);
                }
               
            }
            
            return View(advancePayment);
        }

        // GET: Employee/AdvancePayment/Edit/5
        public IActionResult Edit(int id)
        {
            
            var advancePayment = advancePaymentManager.GetById(id);
            if (advancePayment == null)
            {
                return NotFound();
            }
            return View(advancePayment);
        }

        // POST: Employee/AdvancePayment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdvancePayment advancePayment)
        {
            if (id != advancePayment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    advancePaymentManager.Update(advancePayment);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvancePaymentExists(advancePayment.Id))
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
            return View(advancePayment);
        }

        // GET: Employee/AdvancePayment/Delete/5
        public IActionResult Delete(int id)
        {


            var advancePayment = advancePaymentManager.GetById(id);
            if (advancePayment == null)
            {
                return NotFound();
            }

            return View(advancePayment);
        }

        // POST: Employee/AdvancePayment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advancePayment = advancePaymentManager.GetById(id);
            advancePaymentManager.Delete(advancePayment);
            return RedirectToAction(nameof(Index));
        }

        private bool AdvancePaymentExists(int id)
        {
            return advancePaymentManager.Exists(id);
        }
    }
}
