using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Packt.Shared;
using Microsoft.AspNetCore.Mvc;

namespace NorthwindWeb.Pages
{
    public class SuppliersModel : PageModel
    {
        public IEnumerable<Supplier> Suppliers { get; set; }
        private Northwind db;
        [BindProperty]
        public Supplier Supplier { get; set; }
        public SuppliersModel(Northwind injectedContext)
        {
            db = injectedContext;
        }
        public void OnGet()
        {
            ViewData["Title"] = "Сайт Northwind - Поставщики";

            Suppliers = 
                db.Suppliers.Select(s => new Supplier()
                {
                    CompanyName = s.CompanyName,
                    Country = s.Country,
                    Phone = s.Phone
                }
                );
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(Supplier);
                db.SaveChanges();
                return RedirectToPage("/suppliers");
            }
            return Page();
        }
    }
}