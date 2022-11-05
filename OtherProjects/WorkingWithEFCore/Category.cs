using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Packt.Shared
{
    public class Category
    {
        // столбцы БД
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        [Column(TypeName ="ntext")]
        public string Description { get; set; }
        // навигационное свойство для связанных строк
        public virtual ICollection<Product> Products { get; set; }
        public Category()
        {
            // чтоюы позволить разработчикам добавлять товары в Category,
            // нужно инициировать навигационное свойство пустым списком.
            this.Products = new HashSet<Product>();
        }
    }
}
