using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        // Для определения класса, используемого в списке, 
        // MVC Обращается
        // к конфигурации в классе Startup. Затем MVC создает список
        // (сейчас это FakeRepository. Потом можно заменить.)
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }
        // Вызываем представление по умолчанию 
        // и передаем ему список.
        public ViewResult List() => View(repository.Products);
    }
}
