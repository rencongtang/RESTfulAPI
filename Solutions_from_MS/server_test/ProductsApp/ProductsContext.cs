﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ProductStore.Models;

namespace ProductsApp
{
    public class ProductsContext : DbContext
        
    {
        public ProductsContext() : base("name = ProductsContext")
        {
        }
        public DbSet<Product> Products { get; set; }
    }


}