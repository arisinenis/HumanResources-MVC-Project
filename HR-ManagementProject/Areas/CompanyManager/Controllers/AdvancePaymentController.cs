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

namespace HR_ManagementProject.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager"), Authorize(Roles = "Manager")]
    [Route("CompanyManager/[controller]/[action]")]
    public class AdvancePaymentController : Controller
    {
        
        private readonly IAdvancePaymentService advancePaymentManager;

        public AdvancePaymentController(IAdvancePaymentService advancePaymentManager)
        {
            
            this.advancePaymentManager = advancePaymentManager;
        }

        // GET: Employee/AdvancePayment
        public IActionResult Index()
        {

            var advancePayments = advancePaymentManager.GetAll();
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

        public IActionResult WaitingAdvancePayment()
        {
            var userID = HttpContext.Session.GetString("CompanyId");
            var advancePayments = advancePaymentManager.GetAllWaitingAdvancePayments(Convert.ToInt32(userID));


            if (advancePayments == null)
            {
                return NotFound();
            }

            return View(advancePayments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveAdvancePayment(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var advancePayment = advancePaymentManager.GetById(id);
            if (ModelState.IsValid)
            {

                advancePaymentManager.ApproveAdvancePayment(advancePayment);

                ViewBag.Message = "İzin Onaylandı !";

                return RedirectToAction(nameof(WaitingAdvancePayment));
            }
            return View(advancePayment);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectAdvancePayment(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var advancePayment = advancePaymentManager.GetById(id);
            if (ModelState.IsValid)
            {

                advancePaymentManager.RejectAdvancePayment(advancePayment);

                ViewBag.Message = "İzin Onaylandı !";

                return RedirectToAction(nameof(WaitingAdvancePayment));
            }
            return View(advancePayment);

        }

        private bool AdvancePaymentExists(int id)
        {
            return advancePaymentManager.Exists(id);
        }
    }
}
