using Template.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Template.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Template.Web.Areas.Example.Dashboard
{
    public class UserIndexViewModel
    {
        public string MessaggioBenvenuto { get; set; }

        // Aggiungi la lista di utenti
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }

    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    
}
