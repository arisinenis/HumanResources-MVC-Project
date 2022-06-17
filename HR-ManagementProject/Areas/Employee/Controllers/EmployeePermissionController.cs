using HumanResources.BLL.Abstract;
using HumanResources.Core.Entities;
using HumanResources.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HR_ManagementProject.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Route("Employee/[controller]/[action]")]
    public class EmployeePermissionController : Controller
    {
        private readonly IPermissionService permissionManager;

        public EmployeePermissionController(IPermissionService permissionManager)
        {
            this.permissionManager = permissionManager;
        }

        public async Task<IActionResult> Index(int id)
        {
            var permissions = permissionManager.GetAllPermissionById(id);
            return View(permissions);
        }

        public async Task<IActionResult> Details(int id)
        {

            var permission = permissionManager.GetById(id);
            if (permission == null)
            {
                return NotFound();
            }

            return View(permission);
        }

        public IActionResult Create()
        {
            //ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Address");
            return View();
        }

        // POST: Employee/Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("FirstName,SecondName,LastName,CitizenNo,PhoneNumber,Password,Address,BirthDate,StartDate,EndDate,Status,JobTitle,Job,PhotoPath,CompanyId,Id")]*/ Permission permission)
        {
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
            var permission = permissionManager.GetById(id);
            if (permission == null)
            {
                return NotFound();
            }
            return View(permission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, /*[Bind("FirstName,SecondName,LastName,CitizenNo,PhoneNumber,Password,Address,BirthDate,StartDate,EndDate,Status,JobTitle,Job,PhotoPath,CompanyId,Id")]*/ Permission permission)
        {
            if (id != permission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    permissionManager.Update(permission);

                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!PermissionExists(permission.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(permission);
        }
    }
}
