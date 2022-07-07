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
using HR_ManagementProject.Areas.CompanyManager.Model;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HR_ManagementProject.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager"), Authorize(Roles = "Manager")]
    [Route("CompanyManager/[controller]/[action]")]
    public class WalletController : Controller
    {
        private readonly IWalletService _walletManager;
        private readonly ICompanyService _companyManager;
        private readonly ICreditCardService _creditCardManager;

        public WalletController(IWalletService walletManager, ICompanyService companyManager, ICreditCardService creditCardManager)
        {
            _walletManager = walletManager;
            _companyManager = companyManager;
            _creditCardManager = creditCardManager;
        }

        // GET: CompanyManager/Wallet
        public async Task<IActionResult> Index()
        {
            var wallet = _walletManager.GetWalletWithCompany(Convert.ToInt32(HttpContext.Session.GetString("CompanyId")));
            return View(wallet);
        }
        // GET: CompanyManager/Wallet/Create
        public IActionResult CreateBalance()
        {
            //ViewData["CardName"] = new SelectList(_creditCardManager.GetAllCreditCardById(Convert.ToInt32(HttpContext.Session.GetString("CompanyId"))), "Id", "Name");
            //var wallet = _walletManager.GetWalletWithCompany(Convert.ToInt32(HttpContext.Session.GetString("CompanyId")));
            //WalletCreditCardVM walletCreditCardVM = new WalletCreditCardVM();
            //walletCreditCardVM.CreditCard = _creditCardManager.GetAllCreditCardById(Convert.ToInt32(HttpContext.Session.GetString("CompanyId")));
            //walletCreditCardVM.Wallet = wallet;
            Wallet wallet = new Wallet();
            wallet.CreditCards = _creditCardManager.GetAllCreditCardById(Convert.ToInt32(HttpContext.Session.GetString("CompanyId")));
            if (wallet.CreditCards.Count() == 0)
            {
                TempData["KartYok"] = "Lütfen Kredi Kartı Kaydı Oluşturunuz ";

            }
            wallet.Company = _companyManager.GetById(Convert.ToInt32(HttpContext.Session.GetString("CompanyId")));
            TempData["wallet1"] = JsonConvert.SerializeObject(wallet, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return View(wallet);
        }

        // POST: CompanyManager/Wallet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBalance(int id,Wallet wallet)
        {
            //wallet.Company = _companyManager.GetById(Convert.ToInt32(HttpContext.Session.GetString("CompanyId")));
            //var creditCard = _creditCardManager.GetById(creditCardID);
            var walletDb = _walletManager.GetWalletWithCompany(Convert.ToInt32(HttpContext.Session.GetString("CompanyId")));

            var creditCard = _creditCardManager.GetById(id);
            
            if (ModelState.IsValid)
            {
                
                walletDb.TopUpDate = DateTime.Now;
                if (creditCard.Bakiye - wallet.Balance > 0)
                {
                    creditCard.Bakiye -= wallet.Balance;
                    walletDb.Balance += wallet.Balance;

                    _walletManager.Update(walletDb);
                    _creditCardManager.Update(creditCard);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["hata"] = "Bakiyeniz yetersiz.";



                    var data = TempData["wallet1"].ToString();
                    var wallet1=JsonConvert.DeserializeObject<Wallet>(data);
                     

                     
                    return View(wallet1);
                }
               
            }
            return View(walletDb);
        }
    }
}
