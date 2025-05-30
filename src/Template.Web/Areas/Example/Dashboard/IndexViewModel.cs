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
    public class IndexViewModel
    {
        public int NumeroUtentiAttivi { get; set; }
        public int OrdiniRecenti { get; set; }
        public string MessaggioBenvenuto { get; set; }
        // Aggiungi altre proprietà necessarie
    }
}
