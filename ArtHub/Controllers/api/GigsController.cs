using ArtHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArtHub.Controllers.api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext context;
        public GigsController()
        {
            context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = context.Gigs
                .Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCanceled)
                return NotFound();

            gig.IsCanceled = true; //change state

            var notification = new Notification
            {
                DateTime=DateTime.Now,
                Gig=gig,
                Type=NotificationType.GigCanceled
            };

            var attendees = context.Attendances
                .Where(a => a.GigId == gig.Id)
                .Select(a => a.Attendee)
                .ToList();

            foreach(var attendee in attendees)
            {
                var userNotification = new UserNotification
                {
                    User = attendee,
                    Notification  =notification
                };
                context.UserNotifications.Add(userNotification);
            }

            context.SaveChanges();

            return Ok();
        }
    }
}
