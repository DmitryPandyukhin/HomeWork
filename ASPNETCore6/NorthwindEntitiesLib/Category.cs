﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace packt.shared
{
    [Index("CategoryName", Name = "CategoryName")]
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public long CategoryID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar (15)")]
        [StringLength(15)]
        public string CategoryName { get; set; } = null!;
        [Column(TypeName = "ntext")]
        public string? Description { get; set; }
        [Column(TypeName = "image")]
        public byte[]? Picture { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<Product> Products { get; set; }
    }
}