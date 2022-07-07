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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace HR_ManagementProject.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager"), Authorize(Roles = "Manager")]
    [Route("CompanyManager/[controller]/[action]")]

    public class CreditCardController : Controller
    {
        private readonly ICreditCardService creditCardManager;
        private readonly ICompanyService _companyManager;
        private readonly IWalletService walletService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CreditCardController(ICreditCardService _creditCardManager, IWebHostEnvironment _hostEnvironment, ICompanyService companyManager,IWalletService walletService)
        {
            creditCardManager = _creditCardManager;
            _companyManager = companyManager;
            this.walletService = walletService;
            this._hostEnvironment = _hostEnvironment;
        }

        // GET: CompanyManager/CreditCard
        public async Task<IActionResult> Index()
        {
            var card = creditCardManager.GetAllCreditCardById(Convert.ToInt32(HttpContext.Session.GetString("CompanyId")));
            return View(card);
        }

        // GET: CompanyManager/CreditCard/Create
        public IActionResult Create()
        {
            return View(new CreditCard());
        }

        // POST: CompanyManager/CreditCard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreditCard creditCard)
        {
            //var company = _companyManager.GetById(Convert.ToInt32(HttpContext.Session.GetString("CompanyId")));
            var wallet = walletService.GetWalletWithCompany(Convert.ToInt32(HttpContext.Session.GetString("CompanyId")));
            creditCard.Wallet = wallet;
            //creditCard.CompanyId = company.Id;
            if (ModelState.IsValid)
            {
                creditCardManager.Add(creditCard);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CompanyID"] = new SelectList(_context.Companies, "Id", "Name", creditCard.CompanyID);
            return View(creditCard);
        }
        private bool CreditCardExists(int id)
        {
            return creditCardManager.Exists(id);
        }
    }
}
