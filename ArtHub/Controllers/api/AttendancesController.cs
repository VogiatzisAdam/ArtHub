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
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext context;

        public AttendancesController()
        {
            context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(int gigId)
        {
            var attendance = new Attendance
            {
                GigId=gigId,
                AttendeeId=User.Identity.GetUserId()
            };

            context.Attendances.Add(attendance);
            context.SaveChanges();

            return Ok();
        }
    }
}
