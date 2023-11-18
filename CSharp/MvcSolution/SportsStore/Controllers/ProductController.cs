using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;
        // При создании контроллера MVC создаст список,
        // тип которого задан в классе Startup
        // (сейчас это FakeRepository. Потом можно заменить.).
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }
        // Выполняем метод действия по умолчанию
        public ViewResult List(int productPage = 1) =>
            View(new ProductListViewModel { 
                Products = repository.Products
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                }
             });
    }
}