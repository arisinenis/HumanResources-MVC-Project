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
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace HR_ManagementProject.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager"), Authorize(Roles = "Manager")]
    [Route("CompanyManager/[controller]/[action]")]
    public class CompanyManagerExpenseController : Controller
    {
        private readonly IExpenseService expenseManager;

        public CompanyManagerExpenseController(IExpenseService expenseManager)
        {
            this.expenseManager = expenseManager;
        }

        // GET: CompanyManager/CompanyManagerExpense
        public async Task<IActionResult> Index()
        {
            return View(expenseManager.GetAllWithEmployee());
        }

        // GET: CompanyManager/CompanyManagerExpense/Details/5
        public IActionResult Details(int id)
        {
            var expense = expenseManager.GetByIdWithEmployee(id);

            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: CompanyManager/CompanyManagerExpense/Create
        //public IActionResult Create()
        //{
        //    ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
        //    return View();
        //}

        // POST: CompanyManager/CompanyManagerExpense/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Total,Description,ExpenseType,Status,RequestDate,ResponseDate,FileName,EmployeeId,Id")] Expense expense)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(expense);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", expense.EmployeeId);
        //    return View(expense);
        //}

        // GET: CompanyManager/CompanyManagerExpense/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = expenseManager.GetById(id);

            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: CompanyManager/CompanyManagerExpense/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    expenseManager.Update(expense);
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!ExpenseExists(expense.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(expense);
        }

        // GET: CompanyManager/CompanyManagerExpense/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = expenseManager.GetById(id);

            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: CompanyManager/CompanyManagerExpense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = expenseManager.GetById(id);
            expenseManager.Delete(expense);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> WaitingExpenses()
        {
            var userID = HttpContext.Session.GetString("CompanyId");
            var expenses = expenseManager.GetAllWaitingExpensesWithEmployees(Convert.ToInt32(userID));


            if (expenses == null)
            {
                return NotFound();
            }

            return View(expenses);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveExpense(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = expenseManager.GetById(id);

            if (ModelState.IsValid)
            {

                expenseManager.ApproveExpense(expense);

                ViewBag.Message = "Harcama Onaylandı !";

                return RedirectToAction(nameof(WaitingExpenses));
            }

            return View(expense);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectExpense(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = expenseManager.GetById(id);

            if (ModelState.IsValid)
            {
                expenseManager.RejectExpense(expense);

                ViewBag.Message = "Harcama Reddedildi !";

                return RedirectToAction(nameof(WaitingExpenses));
            }

            return View(expense);

        }

        public IActionResult Download(int id)
        {
            var expense = expenseManager.GetById(id);
            if (expense.FileName == null)
            {
                TempData["FileError"] = "Böyle bir dosya bulunamadı.";
                return RedirectToAction(nameof(Details), new { id = expense.Id});
            }
            var memory = DownloadFile(expense.FileName, "wwwroot\\files");
            return File(memory.ToArray(), "application/*", expense.FileName);
        }

        private MemoryStream DownloadFile(string fileName, string uploadPath)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), uploadPath, fileName);
            var memory = new MemoryStream();

            if (System.IO.File.Exists(path))
            {
                var net = new System.Net.WebClient();
                var data = net.DownloadData(path);
                var content = new System.IO.MemoryStream(data);
                memory = content;
            }

            memory.Position = 0;
            return memory;
        } 
    }
}
