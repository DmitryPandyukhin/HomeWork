namespace SportsStore.Models
{
    /// <summary>
    /// Товар
    /// </summary>
    public class Product
    {
        public int ProductID { get; set; }
        /// <summary>
        /// Название товара
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Категория
        /// </summary>
        public string Category { get; set; }
    }
}
