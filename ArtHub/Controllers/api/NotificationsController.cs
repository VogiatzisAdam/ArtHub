using ArtHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using ArtHub.Dtos;
using AutoMapper;

namespace ArtHub.Controllers.api
{
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext context;

        public NotificationsController()
        {
            context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNotifications()
        {
            var userId = User.Identity.GetUserId();

            var notifications = context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            return notifications.Select(Mapper.Map<Notification, NotificationDto>); 
            //return notifications.Select(n=> new NotificationDto() 
            //{
            //     DateTime = n.DateTime,
            //     Gig = new GigDto
            //     {
            //         Artist =new UserDto
            //         {
            //             Id= n.Gig.Artist.Id,
            //             Name= n.Gig.Artist.Name
            //         },
            //         DateTime = n.Gig.DateTime,
            //         Id = n.Gig.Id,
            //         IsCanceled = n.Gig.IsCanceled,
            //         Venue =n .Gig.Venue
            //     },
            //     OriginalDateTime = n.OriginalDateTime,
            //     OriginalVenue = n.OriginalVenue,
            //     Type = n.Type
            //});    
        }
    }
}
