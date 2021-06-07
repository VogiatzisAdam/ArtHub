using ArtHub.Controllers;
using ArtHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace ArtHub.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }
        public string Heading { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        [Display(Name ="Genre")]
        public byte GenreId { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public string Action
        {
            get
            {
                //return (Id != 0) ? "Update" : "Create";
                Expression<Func<GigController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<GigController, ActionResult>> create = (c => c.Create(this));

                var action= (Id != 0) ? update : create;
                var actionName = (action.Body as MethodCallExpression).Method.Name;

                return actionName;
            }
        }
        public DateTime GetDateTime()
        {
             return DateTime.Parse(string.Format("{0} {1}", Date, Time)); 
        }
    }
}