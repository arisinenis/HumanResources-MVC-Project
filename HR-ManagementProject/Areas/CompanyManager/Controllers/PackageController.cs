using AutoMapper;
using HumanResources.BLL.Abstract;
using HumanResources.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HR_ManagementProject.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager"), Authorize(Roles = "Manager")]
    [Route("CompanyManager/[controller]/[action]")]
    public class PackageController : Controller
    {
        private readonly IPackageService _packageManager;
        private readonly IWalletService walletManager;
        private readonly ICompanyService companyManager;
        private readonly IEmployeeService employeeService;
        private readonly IMapper mapper;

        public PackageController(IPackageService packageManager, IMapper mapper, IWalletService walletManager, ICompanyService companyManager, IEmployeeService employeeService)
        {
            _packageManager = packageManager;
            this.walletManager = walletManager;
            this.companyManager = companyManager;
            this.employeeService = employeeService;
            this.companyManager = companyManager;
        }

        // GET: Package/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var package = _packageManager.GetById(id);
            if (package == null)
            {
                return NotFound();
            }
            return View(package);
        }
        public async Task<IActionResult> Index()
        {
            var companyId = HttpContext.Session.GetString("CompanyId");
            return View( _packageManager.GetByUsageAmount(Convert.ToInt32(companyId)));
        }

        public IActionResult AddPackage(int packageId)
        {
            Package package = _packageManager.GetById(packageId);
            return View(package);
        }

        [HttpPost]
        public IActionResult AddPackage(Package package)
        {
            string id = HttpContext.Session.GetString("id");
            HumanResources.Core.Entities.Employee employee = employeeService.GetById(Convert.ToInt32(id));
            Company company = companyManager.GetById(employee.CompanyId);
            if (company.PersonelSayisi > package.UsageAmount)
            {
                ModelState.AddModelError("", "Paket çalışan sayısı personel sayısından az olamaz.");
                return View("Index", _packageManager.GetAll());
            }
            //package.Companies.Add(company);
            _packageManager.Update(package);
            return RedirectToAction("Index", _packageManager.GetAll());

        }

        [HttpGet]
        public IActionResult BuyPackage(int id)
        {
            var package = _packageManager.GetById(id);
            
            return View(package);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyPackage(int id, Package package )
        {
            package = _packageManager.GetById(id);
            
            //if (ModelState.IsValid)
            //{
            var company = companyManager.GetById(Convert.ToInt32(HttpContext.Session.GetString("CompanyId")));
                var wallet = walletManager.GetWalletWithCompany(Convert.ToInt32(HttpContext.Session.GetString("CompanyId")));

                if (wallet.Balance < package.Cost)
                {
                    ViewBag.BuyError = "Bakiye Yetersiz! Bakiye yüklemek için, bakiye yükle butonuna tıklayınız.";
                    return View(package);
                }
                if (companyManager.GetpackagesByCompanyID(company.Id) > 0)
                {
                    ViewBag.BuyError = "Kullanmakta olduğunuz başka bir paket mevcut. Paket değişikliği yapmak için lütfen paket değiştirme sayfasına gidiniz.";
                    return View(package);
                }

                // Şirketin satın aldığı paketi atadık.

                package.PurchaseDate = DateTime.Now.Date;
                package.Occupancy = DateTime.Now.Date.AddYears(1);
                company.Packages.Add(package);
                companyManager.Update(company);
            HttpContext.Session.Remove("Package");
            var packageCount = companyManager.GetpackagesByCompanyID(company.Id);
            
            HttpContext.Session.SetString("Package",packageCount.ToString());

            // Şirketin cüzdanından, paketin fiyatı kadar düşeceğiz.

            wallet.Balance -= package.Cost;
                walletManager.Update(wallet);

                return RedirectToAction("Index","Home");
            //}
            //return View(package);
        }
    }
}
