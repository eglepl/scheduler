using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scheduler.ViewModels.HealthCenter;

namespace Scheduler.Controllers.UI
{
    public class VisitsController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}