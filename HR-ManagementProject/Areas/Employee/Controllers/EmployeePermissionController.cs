using AutoMapper;
using HR_ManagementProject.Areas.Employee.Models;
using HumanResources.BLL.Abstract;
using HumanResources.Core.Entities;
using HumanResources.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HR_ManagementProject.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Route("Employee/[controller]/[action]")]
    public class EmployeePermissionController : Controller
    {
        private readonly IPermissionService permissionManager;
        private readonly IEmployeeService employeeManager;
        private readonly IMapper mapper;

        public EmployeePermissionController(IPermissionService permissionManager, IEmployeeService employeeManager, IMapper mapper)
        {
            this.permissionManager = permissionManager;
            this.employeeManager = employeeManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var permissions = permissionManager.GetAllPermissionById(Convert.ToInt32(HttpContext.Session.GetString("id")));
            var employee = employeeManager.GetById(Convert.ToInt32(HttpContext.Session.GetString("id")));

            var tupleObject = (permissions, employee);

            return View(tupleObject);
        }

        public async Task<IActionResult> Details(int id)
        {

            var permission = permissionManager.GetById(id);
            var employee = employeeManager.GetById(Convert.ToInt32(HttpContext.Session.GetString("id")));

            var tupleObject = (permission, employee);

            if (permission == null)
            {
                return NotFound();
            }

            return View(tupleObject);
        }

        public IActionResult Create()
        {
            //ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Address");
            var employee = employeeManager.GetById(Convert.ToInt32(HttpContext.Session.GetString("id")));

            PermissionEmployeeVM permissionEmployeeVM = new PermissionEmployeeVM();
            permissionEmployeeVM.Permission = new Permission();
            permissionEmployeeVM.Employee = employee;

            return View(permissionEmployeeVM);
        }

        // POST: Employee/Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("FirstName,SecondName,LastName,CitizenNo,PhoneNumber,Password,Address,BirthDate,StartDate,EndDate,Status,JobTitle,Job,PhotoPath,CompanyId,Id")]*/ PermissionEmployeeVM permissionEmployeeVM)
        {
            var employee = employeeManager.GetById(Convert.ToInt32(HttpContext.Session.GetString("id")));
            permissionEmployeeVM.Employee = employee;
            Permission permission = permissionEmployeeVM.Permission;

            permissionEmployeeVM.Permission.EmployeeId = employee.Id;

            if (ModelState.IsValid)
            {
                permissionManager.Add(permission);
                return RedirectToAction(nameof(Index));
            }
            return View(permission);
        }

      
        
        public async Task<IActionResult> Delete(int id)
        {
            var permission = permissionManager.GetById(id);
            if (permission == null)
            {
                return NotFound();
            }

            return View(permission);
        }

        [HttpPost, ActionName("DeletePermission")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var permission = permissionManager.GetById(id);

            if (permission.PermissionStatus == PermissionStatus.Bekliyor)
            {
                permissionManager.Delete(permission);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.ErrorMessage = "Bu izin silinemez !";
                return View(nameof(Delete));
            }

        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = employeeManager.GetById(Convert.ToInt32(HttpContext.Session.GetString("id")));

            PermissionEmployeeVM permissionEmployeeVM = new PermissionEmployeeVM();
            permissionEmployeeVM.Permission = permissionManager.GetById(id);
            permissionEmployeeVM.Employee = employee;

            return View(permissionEmployeeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, /*[Bind("FirstName,SecondName,LastName,CitizenNo,PhoneNumber,Password,Address,BirthDate,StartDate,EndDate,Status,JobTitle,Job,PhotoPath,CompanyId,Id")]*/ PermissionEmployeeVM permissionEmployeeVM)
        {

            var employee = employeeManager.GetById(Convert.ToInt32(HttpContext.Session.GetString("id")));
            permissionEmployeeVM.Employee = employee;
            Permission permission = permissionEmployeeVM.Permission;

            permissionEmployeeVM.Permission.EmployeeId = employee.Id;

            if (ModelState.IsValid)
            {
                //permission = permissionManager.GetById(id);

                if (permission.PermissionStatus == PermissionStatus.Bekliyor)
                {
                    permissionManager.Update(permission);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMessage = "Bu izin düzeltilemez !";
                    return View(nameof(Delete));
                }
            }
            return View(permission);
        }
    }
}
