using ArtHub.Dtos;
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

        // /api/attendances
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            //edge case
            var exists = context.Attendances.Any(a => a.AttendeeId==userId && a.GigId==dto.GigId);

            if (exists)
                return BadRequest("The attendance already exists");

            // add 1 Attendance
            var attendance = new Attendance
            {
                GigId=dto.GigId,
                AttendeeId=User.Identity.GetUserId()
            };

            context.Attendances.Add(attendance);
            context.SaveChanges();

            return Ok();
        }
    }
}
