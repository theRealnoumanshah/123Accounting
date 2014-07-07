using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using System.Data.Entity;
using _123Accounting.Models;

namespace _123Accounting.DAL
{
    public class AccountingContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders {get; set;}
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}