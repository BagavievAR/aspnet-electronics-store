using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatsController : Controller
    {
        private readonly AppStats _stats;

        public StatsController(AppStats stats)
        {
            _stats = stats;
        }

        public IActionResult Index()
        {
            var vm = new StatsViewModel
            {
                HomeViews = _stats.HomeViews,
                CatalogViews = _stats.CatalogViews,
                TotalViews = _stats.TotalViews
            };

            return View(vm);
        }
    }
}
