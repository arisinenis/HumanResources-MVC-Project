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
using HR_ManagementProject.Areas.Employee.Models;

namespace HR_ManagementProject.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Route("Employee/[controller]/[action]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeManager;
        private readonly IPermissionService permissionManager;
        private readonly IMapper mapper;

        public EmployeeController(IEmployeeService employeeManager, IPermissionService permissionManager, IMapper mapper)
        {
            this.employeeManager = employeeManager;
            this.permissionManager = permissionManager;
            this.mapper = mapper;
            //Sİlcezz
        }

        // GET: Employee/Employee
        public async Task<IActionResult> Index()
        {
            // Employee anasayfası. Son 5 permission burada listelenecek. Sonra yazılacak.
            //var employee = employeeManager.GetById(id);
            return View();
        }
        


        // GET: Employee/Employee/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var employee = employeeManager.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

       

        

        // GET: Employee/Employee/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var employeeDb = employeeManager.GetById(id);
            EmployeeEditVM employeeEditVM = new EmployeeEditVM();
            mapper.Map(employeeEditVM, employeeDb);

            if (employeeDb == null)
            {
                return NotFound();
            }
            return View(employeeEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, /*[Bind("FirstName,SecondName,LastName,CitizenNo,PhoneNumber,Password,Address,BirthDate,StartDate,EndDate,Status,JobTitle,Job,PhotoPath,CompanyId,Id")]*/ EmployeeEditVM employeeEditVM)
        {

            var employeeDb = employeeManager.GetById(id);
            mapper.Map(employeeEditVM, employeeDb);

            if (id != employeeDb.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    employeeManager.Update(employeeDb);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employeeDb.Id))
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
            return View(employeeEditVM);
        }
        private bool EmployeeExists(int id)
        {
            return employeeManager.Exists(id);
        }

        
    }
}
