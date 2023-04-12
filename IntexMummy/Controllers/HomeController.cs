using IntexMummy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IntexMummy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private fagelgamousContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, fagelgamousContext temp)
        {
            _logger = logger;
            context = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Burials(int pageNum = 1)
        {
            int pageSize = 10;
            var burials = context.Burialmain.Skip((pageNum - 1) * pageSize).Take(pageSize);
            var pageInfo = new PageInfo
            {
                TotalNumBurials = context.Burialmain.Count(),
                BurialsPerPage = pageSize,
                CurrentPage = pageNum


            };
            var viewModel = new BurialMainViewModel { Burialmain = burials, PageInfo = pageInfo };

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult SingleBurial(int id)
        {
            ViewBag.Id = id;
            ViewBag.Message = "Hello, world!";
            return View();
        }



        [Authorize]
        public IActionResult AdminData()
        {
            return View();
        }

        public IActionResult Unsupervised()
        {
            return View();
        }

        public IActionResult Supervised()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
