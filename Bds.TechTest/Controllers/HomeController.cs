using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bds.TechTest.Models;
using Bds.TechTest.Models.ViewModels;
using Bds.TechTest.Models.WebSearchers;
using Bds.TechTest.Models.Services;

namespace Bds.TechTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebSearcherService webSearcherService;

        public HomeController()
        {
            // This would be injected using something like Ninject
            webSearcherService = new WebSearcherService(new QuerySourceService());
        }

        public IActionResult Index()
        {
            return View(new HomePageViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomePageViewModel model)
        {
            var searchResults = await webSearcherService.Search(model.SearchTerm);
            model.SearchResults = searchResults.Results;
            model.Errors = searchResults.ErrorList;
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
