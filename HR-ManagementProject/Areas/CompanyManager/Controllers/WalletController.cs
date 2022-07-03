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
using Microsoft.AspNetCore.Http;

namespace HR_ManagementProject.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager")]
    [Route("CompanyManager/[controller]/[action]")]
    public class WalletController : Controller
    {
        private readonly IWalletService walletManager;
        private readonly ICompanyService companyManager;

        public WalletController(IWalletService walletManager, ICompanyService companyManager)
        {
            this.walletManager = walletManager;
            this.companyManager = companyManager;
        }

        // GET: CompanyManager/Wallet
        public async Task<IActionResult> Index()
        {
            var wallet = walletManager.GetWalletWithCompany(Convert.ToInt32(HttpContext.Session.GetString("CompanyId")));
            return View(wallet);
        }

        // GET: CompanyManager/Wallet/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var wallet = await _context.Wallets
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (wallet == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(wallet);
        //}

        // GET: CompanyManager/Wallet/Create
        public IActionResult CreateBalance()
        {
            var wallet = walletManager.GetWalletWithCompany(Convert.ToInt32(HttpContext.Session.GetString("CompanyId")));
            return View(wallet);
        }

        // POST: CompanyManager/Wallet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBalance(Wallet wallet)
        {
            wallet.Company = companyManager.GetById(Convert.ToInt32(HttpContext.Session.GetString("CompanyId")));
            var walletDb = walletManager.GetWalletWithCompany(Convert.ToInt32(HttpContext.Session.GetString("CompanyId")));

            if (ModelState.IsValid)
            {
                walletDb.TopUpDate = DateTime.Now.Date;
                walletDb.Balance += wallet.Balance;
                walletManager.Update(walletDb);

                return RedirectToAction(nameof(Index));
            }
            return View(wallet);
        }

        // GET: CompanyManager/Wallet/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var wallet = await _context.Wallets.FindAsync(id);
        //    if (wallet == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(wallet);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Balance,TopUpDate,Id")] Wallet wallet)
        //{
        //    if (id != wallet.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(wallet);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!WalletExists(wallet.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(wallet);
        //}

        // GET: CompanyManager/Wallet/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var wallet = await _context.Wallets
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (wallet == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(wallet);
        //}

        // POST: CompanyManager/Wallet/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var wallet = await _context.Wallets.FindAsync(id);
        //    _context.Wallets.Remove(wallet);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool WalletExists(int id)
        //{
        //    return _context.Wallets.Any(e => e.Id == id);
        //}
    }
}
