using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RadiostationWeb.Models;

namespace RadiostationWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string search, PageInfo p, int? mmm)
        {
        
            return View();
        }

        public IActionResult Privacy(string search, PageInfo p, int? mmm)
        {
            var te = Convert.ToInt32(HttpContext.Request.Query["mmm"]);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int SeacrchString { get; set; }

        public PageInfo()
        {
            PageNumber = 1;
            PageSize = 20;
            Total = 0;
        }
    }

}
