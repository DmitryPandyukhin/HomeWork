using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        // При создании контроллера MVC создаст список,
        // тип которого задан в классе Startup
        // (сейчас это FakeRepository. Потом можно заменить.).
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }
        // Выполняем метод действия по умолчанию
        public ViewResult List() => View(repository.Products);
    }
}
