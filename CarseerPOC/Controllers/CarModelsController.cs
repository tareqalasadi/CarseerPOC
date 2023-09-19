using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarseerPOC.Controllers
{
    public class CarModelsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
