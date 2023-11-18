namespace MyStore.Models
{
    public interface ITag
    {
        int TagId { get; set; }
        string? Name { get; set; }
        int SortNumber { get; set; }
    }
    public class Tag : ITag
    {
        public int TagId { get; set; }
        public string? Name { get; set; }
        // Порядок отображения тегов заказа.
        public int SortNumber { get; set; }

        // ссылка на заказ
        public int OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
