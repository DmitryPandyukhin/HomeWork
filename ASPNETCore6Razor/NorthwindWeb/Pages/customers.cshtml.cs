using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Packt.Shared;
using Microsoft.AspNetCore.Mvc;

namespace NorthwindWeb.Pages
{
    public class CustomersModel : PageModel
    {
        // ILookup - список 
        public ILookup<string?, Customer?> CustomersByCountry { get; set; }
        private Northwind db;
        public CustomersModel(Northwind injectedContext)
        {
            db = injectedContext;
        }
        public void OnGet()
        {
            ViewData["Title"] = "Сайт Northwind - Клиенты";

            CustomersByCountry = db.Customers.ToLookup(c => c.Country)!;
        }
    }
}