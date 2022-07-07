


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
using Microsoft.AspNetCore.Authorization;

namespace HR_ManagementProject.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager"), Authorize(Roles = "Manager")]
    [Route("CompanyManager/[controller]/[action]")]
    public class CompanyManagerPermissionController : Controller
    {
        private readonly IPermissionService permissionManager;

        public CompanyManagerPermissionController(IPermissionService permissionManager)
        {
            this.permissionManager = permissionManager;
        }

        // GET: CompanyManager/CompanyManagerPermission
        public async Task<IActionResult> Index()
        {
            return View(permissionManager.GetAll());
            
        }

        // GET: CompanyManager/CompanyManagerPermission/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var permission = permissionManager.GetById(id);
            if (permission == null)
            {
                return NotFound();
            }

            return View(permission);
        }

        // GET: CompanyManager/CompanyManagerPermission/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompanyManager/CompanyManagerPermission/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Permission permission)
        {
            if (ModelState.IsValid)
            {
                permissionManager.Add(permission);
                return RedirectToAction(nameof(Index));
            }
            return View(permission);
        }

        // GET: CompanyManager/CompanyManagerPermission/Edit/5
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
        public async Task<IActionResult> Edit(int id, [Bind("PermissionType,TotalDayOfPermissionType,RequestDate,StartDate,EndDate,PermissionStatus,Id")] Permission permission)
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

        // GET: CompanyManager/CompanyManagerPermission/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var permission = permissionManager.GetById(id);
            if (permission == null)
            {
                return NotFound();
            }

            return View(permission);
        }

        // POST: CompanyManager/CompanyManagerPermission/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var permission = permissionManager.GetById(id);
            permissionManager.Delete(permission);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> WaitingPermission()
        {
            var permission = permissionManager.GetAllWaitingPermission();
            

            if (permission == null)
            {
                return NotFound();
            }

            return View(permission);
        }
       

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApprovePermission(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var permission = permissionManager.GetById(id);
            if (ModelState.IsValid)
            {
                
                permissionManager.ApprovePermission(permission);
                
                ViewBag.Message = "İzin Onaylandı !";

                return RedirectToAction(nameof(WaitingPermission));
            }
            return View(permission);
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectPermission(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var permission = permissionManager.GetById(id);
            if (ModelState.IsValid)
            {

                permissionManager.RejectPermission(permission);

                ViewBag.Message = "İzin Reddedildi !";

                return RedirectToAction(nameof(WaitingPermission));
            }
            return View(permission);

        }


       
    }
}
