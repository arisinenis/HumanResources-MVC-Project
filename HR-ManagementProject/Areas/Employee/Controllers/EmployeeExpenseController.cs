﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HumanResources.Core.Entities;
using HumanResources.DAL.Context;
using HumanResources.BLL.Abstract;
using Microsoft.AspNetCore.Http;
using HumanResources.Core.Enums;

namespace HR_ManagementProject.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Route("Employee/[controller]/[action]")]
    public class EmployeeExpenseController : Controller
    {
        private readonly IExpenseService expenseManager;

        public EmployeeExpenseController(IExpenseService expenseManager)
        {
            this.expenseManager = expenseManager;
        }

        // GET: Employee/EmployeeExpense
        public async Task<IActionResult> Index()
        {
            var expenses = expenseManager.GetAllExpensesById(Convert.ToInt32(HttpContext.Session.GetString("id")));

            return View(expenses);
        }

        // GET: Employee/EmployeeExpense/Details/5
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expenses = expenseManager.GetById(id);

            if (expenses == null)
            {
                return NotFound();
            }

            return View(expenses);
        }

        // GET: Employee/EmployeeExpense/Create
        public IActionResult Create()
        {
            return View(new Expense());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expense expense)
        {
            if (Convert.ToInt32(HttpContext.Session.GetString("id")) < 1)
            {
                return BadRequest();
            }

            expense.EmployeeId = Convert.ToInt32(HttpContext.Session.GetString("id"));
            expense.RequestDate = DateTime.Now.Date;

            if (ModelState.IsValid)
            {
                expenseManager.Add(expense);
                return RedirectToAction(nameof(Index));
            }

            return View(expense);
        }

        // GET: Employee/EmployeeExpense/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var expense = expenseManager.GetById(id);
            TempData["file"] = expense.File;
            TempData["filepath"] = expense.FileName;

            if (expense == null)
            {
                return NotFound();
            }
            return View(expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Expense expense)
        {

            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {

                    if (expense.Status == PermissionStatus.Bekliyor)
                    {
                        expense.EmployeeId = Convert.ToInt32(HttpContext.Session.GetString("id"));

                        if (expense.File == null)
                        {
                            expense.File = (IFormFile)TempData["file"];
                            expense.FileName = (string)TempData["filepath"];
                        }

                        expenseManager.Update(expense);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Bu harcama düzeltilemez !";
                        return View(nameof(Delete));
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(expense);
        }

        // GET: Employee/EmployeeExpense/Delete/5
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = expenseManager.GetById(id);

            if (expense.Status == PermissionStatus.Bekliyor)
            {
                expenseManager.Delete(expense);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.ErrorMessage = "Bu harcama silinemez !";
                return View(nameof(Delete));
            }
        }

    }
}
