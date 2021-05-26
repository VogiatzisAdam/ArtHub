using ArtHub.Models;
using ArtHub.ViewModels;
using Microsoft.AspNet.Identity;
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
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres=context.Genres.ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = context.Genres.ToList();
                return View("Create", viewModel);
            }

            //var artistId = User.Identity.GetUserId();

            // var artist = context.Users.Single(u => u.Id == artistId);

           // var genre = context.Genres.Single(u => u.Id == viewModel.Genre);

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime=viewModel.GetDateTime(),
                GenreId=viewModel.GenreId,
                Venue=viewModel.Venue
            };

            context.Gigs.Add(gig);
            context.SaveChanges();

            return RedirectToAction("Index","Home");
        }
    }
}