using ArtHub.Models;
using ArtHub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtHub.Controllers
{
    public class GigController : Controller
    {
        private ApplicationDbContext context;

        public GigController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Gig
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres=context.Genres.ToList()
            };

            return View(viewModel);
        }
    }
}