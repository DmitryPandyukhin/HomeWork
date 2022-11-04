using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Packt.Shared;

namespace NorthwindWeb.Pages
{
    public class SuppliersModel : PageModel
    {
        public IEnumerable<string> Suppliers { get; set; }
        private Northwind db;
        public SuppliersModel(Northwind injectedContext)
        {
            db = injectedContext;
        }
        public void OnGet()
        {
            ViewData["Title"] = "Northwind Web Site - Поставщики";

            /*Suppliers = new[] {
                "Alpha Co", "Beta Limited", "Gamma Corp"
            };*/
            Suppliers = db.Suppliers.Select(s => s.CompanyName);
        }
    }
}