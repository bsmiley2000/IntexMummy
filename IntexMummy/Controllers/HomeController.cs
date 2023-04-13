using IntexMummy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IntexMummy.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

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

        public IActionResult Burials(int pageNum = 1, long idSearchString = 0, string GenderSearchString = null, string PreservationSearchString = null, string HeadDirectionSearchString = null, string AgeSearchString = null, string ColorSearchString = null)
        {
            

            int pageSize = 10;
            long id = idSearchString;
            string gender = GenderSearchString;
            string preservation = PreservationSearchString;
            string headdirection = HeadDirectionSearchString;
            string age = AgeSearchString;
            string color = ColorSearchString;
   
            var burialsQuery = context.Burialmain.AsQueryable();
            if (id != 0)
            {
                burialsQuery = burialsQuery.Where(p => p.Id == id);
                
            }
            if (gender != null)
            {
                burialsQuery = burialsQuery.Where(p => p.Sex == gender);
                
            }
            if (preservation != null)
            {
                burialsQuery = burialsQuery.Where(p => p.Preservation.Contains(preservation));
                
            }
            if (headdirection != null)
            {
                burialsQuery = burialsQuery.Where(p => p.Headdirection == headdirection);
                
            }
            if (age != null)
            {
                burialsQuery = burialsQuery.Where(p => p.Ageatdeath == age);
            }
            if (color != null)
            {
                burialsQuery = burialsQuery.Where(p => p.Headdirection == "YOURMOM!!");
            }

            var burials = burialsQuery.Skip((pageNum - 1) * pageSize).Take(pageSize);

            var pageInfo = new PageInfo
            {
                TotalNumBurials = burialsQuery.Count(),
                BurialsPerPage = pageSize,
                CurrentPage = pageNum,
                GenderSearchString = gender,
                idSearchString = id,
                HeadDirectionSearchString = headdirection,
                PreservationSearchString = preservation,
                AgeSearchString = age
                

            };
            
            var viewModel = new BurialMainViewModel { Burialmain = burials, PageInfo = pageInfo };

            return View(viewModel);
        }
        
        public IActionResult SingleBurial(long id = 0)
        {
            Burialmain singleBurial = context.Burialmain.Where(p => p.Id == id).First();
            try
            {
                BurialmainTextile bmt = context.BurialmainTextile.Where(p => p.MainBurialmainid == singleBurial.Id).First();
                Textile textile = context.Textile.Where(p => p.Id == bmt.MainTextileid).First();
                ViewBag.TId = textile.Id;
                ViewBag.TextileId = textile.Textileid;
                ViewBag.Locale = textile.Locale;
                ViewBag.Description = textile.Description;
                ViewBag.BurialNumber = textile.Burialnumber;
                ViewBag.EstimatedPeriod = textile.Estimatedperiod;
                ViewBag.SampleDate = textile.Sampledate;
                ViewBag.PhotographedDate = textile.Photographeddate;
                ViewBag.Direction = textile.Direction;
                ViewBag.HasTextiles = true;
                // continue with the code
                try
                {
                    ColorTextile ct = context.ColorTextile.Where(p => p.MainTextileid == textile.Id).First();
                    Color color = context.Color.Where(p => p.Id == ct.MainColorid).First();


                    ViewBag.Color = color.Value;
                    ViewBag.HasColor = true;
                }
                catch
                {
                    ViewBag.HasColor = false;
                }
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.HasTextiles = false;
            }
            
            //Viewbags for singleburial
            ViewBag.Id = singleBurial.Id;
            ViewBag.SquareNorthSouth = singleBurial.Squarenorthsouth;
            ViewBag.HeadDirection = singleBurial.Headdirection;
            ViewBag.Sex = singleBurial.Sex;
            ViewBag.NorthSouth = singleBurial.Northsouth;
            ViewBag.Depth = singleBurial.Depth;
            ViewBag.EastWest = singleBurial.Eastwest;
            ViewBag.AdultSubAdult = singleBurial.Adultsubadult;
            ViewBag.FaceBundles = singleBurial.Facebundles;
            ViewBag.SouthToHead = singleBurial.Southtohead;
            ViewBag.Preservation = singleBurial.Preservation;
            ViewBag.FieldBookPage = singleBurial.Fieldbookpage;
            ViewBag.SquareEastWest = singleBurial.Squareeastwest;
            ViewBag.Goods = singleBurial.Goods;
            ViewBag.Text = singleBurial.Text;
            ViewBag.Wrapping = singleBurial.Wrapping;
            ViewBag.HairColor = singleBurial.Haircolor;
            ViewBag.WestToHead = singleBurial.Westtohead;
            ViewBag.SamplesCollected = singleBurial.Samplescollected;
            ViewBag.Area = singleBurial.Area;
            ViewBag.BurialId = singleBurial.Burialid;
            ViewBag.Length = singleBurial.Length;
            ViewBag.BurialNumber = singleBurial.Burialnumber;
            ViewBag.DataExpertInitials = singleBurial.Dataexpertinitials;
            ViewBag.WestToFeet = singleBurial.Westtofeet;
            ViewBag.AgeAtDeath = singleBurial.Ageatdeath;
            ViewBag.SouthToFeet = singleBurial.Southtofeet;
            ViewBag.ExcavationRecorder = singleBurial.Excavationrecorder;
            ViewBag.Photos = singleBurial.Photos;
            ViewBag.Hair = singleBurial.Hair;
            ViewBag.BurialMaterials = singleBurial.Burialmaterials;
            ViewBag.DateOfExcavation = singleBurial.Dateofexcavation;
            ViewBag.FieldBookExcavationYear = singleBurial.Fieldbookexcavationyear;
            ViewBag.ClusterNumber = singleBurial.Clusternumber;
            ViewBag.ShaftNumber = singleBurial.Shaftnumber;
           

            //viewbags for textile
            

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
