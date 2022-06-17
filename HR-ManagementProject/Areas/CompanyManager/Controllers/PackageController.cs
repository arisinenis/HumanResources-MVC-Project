using AutoMapper;
using HumanResources.BLL.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HR_ManagementProject.Areas.CompanyManager.Controllers
{
    [Area("CompanyManager")]
    [Route("CompanyManager/[controller]/[action]")]
    public class PackageController : Controller
    {
        private readonly IPackageService _packageManager;
        private readonly IMapper mapper;

        public PackageController(IPackageService packageManager, IMapper mapper)
        {
            _packageManager = packageManager;
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
            return View(_packageManager.GetAll());
        }
    }
}
