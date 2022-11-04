﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared
{
    [Index("CategoryId", Name = "CategoriesProducts")]
    [Index("CategoryId", Name = "CategoryID")]
    [Index("ProductName", Name = "ProductName")]
    [Index("SupplierId", Name = "SupplierID")]
    [Index("SupplierId", Name = "SuppliersProducts")]
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        [Column("ProductID")]
        public long ProductId { get; set; }
        [Column(TypeName = "nvarchar (40)")]
        public string ProductName { get; set; } = null!;
        [Column("SupplierID", TypeName = "int")]
        public long? SupplierId { get; set; }
        [Column("CategoryID", TypeName = "int")]
        public long? CategoryId { get; set; }
        [Column(TypeName = "nvarchar (20)")]
        public string? QuantityPerUnit { get; set; }
        [Column(TypeName = "money")]
        public byte[]? UnitPrice { get; set; }
        [Column(TypeName = "smallint")]
        public long? UnitsInStock { get; set; }
        [Column(TypeName = "smallint")]
        public long? UnitsOnOrder { get; set; }
        [Column(TypeName = "smallint")]
        public long? ReorderLevel { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool Discontinued { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("Products")]
        public virtual Category? Category { get; set; }
        [ForeignKey("SupplierId")]
        [InverseProperty("Products")]
        public virtual Supplier? Supplier { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
