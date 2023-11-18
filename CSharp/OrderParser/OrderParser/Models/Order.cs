using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersParser.Models
{
    [Index("No", "Reg_date", "Sum", IsUnique = true, Name = "IX1_Order")]	
    internal class Order
    {
        public int OrderId { get; set; }
        public int No { get; set; }
        public DateTime Reg_date { get; set; }
        public decimal Sum { get; set; }
        public int UserId { get; set; }
        // Ссылка на пользователя.
        public User User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
    [Index("Name", "Price", IsUnique = true, Name = "IX1_Name")]
    internal class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Product p = (Product)obj;
                return (Name == p.Name) && (Price == p.Price);
            }
        }

        public override int GetHashCode()
        {
            return Convert.ToInt32(Price)+Name.GetHashCode();
        }
    }
    internal class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        // Ссылка на заказ.
        public Order Order { get; set; }
        public int ProductId { get; set; }
        // Ссылка на товар
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal ResultPrice { get; set; }
    }
    [Index("FIO", IsUnique = true, Name = "IX1_FIO")]
    internal class User
    {
        public int UserId { get; set; }
        public string FIO { get; set; }
        public string Email { get; set; }
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                User p = (User)obj;
                return (FIO == p.FIO);
            }
        }

        public override int GetHashCode()
        {
            return FIO.GetHashCode();
        }
    }
}
