namespace _123Accounting.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using _123Accounting.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<_123Accounting.DAL.AccountingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(_123Accounting.DAL.AccountingContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var products = new List<Product>
            {
                   new Product{Name="PPOIL", Cost=200.00m},
                   new Product{Name="DENIL", Cost=100.00m},
                   new Product{Name="HMOIL", Cost=50.00m}
            };
            products.ForEach(s => context.Products.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var customers = new List<Customer>
            {
                new Customer { CustomerID=1001, FirstName = "Carson",   LastName = "Alexander", 
                    EnrollmentDate = DateTime.Parse("2010-09-01") },
                new Customer { CustomerID=1002, FirstName = "Meredith", LastName = "Alonso",    
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Customer { CustomerID=1003, FirstName = "Arturo",   LastName = "Anand",     
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Customer { CustomerID=1004, FirstName = "Gytis",    LastName = "Barzdukas", 
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Customer { CustomerID=1005, FirstName = "Yan",      LastName = "Li",        
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Customer { CustomerID=1006, FirstName = "Peggy",    LastName = "Justice",   
                    EnrollmentDate = DateTime.Parse("2011-09-01") },
                new Customer { CustomerID=1007, FirstName = "Laura",    LastName = "Norman",    
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Customer { CustomerID=1008, FirstName = "Nino",     LastName = "Olivetto",  
                    EnrollmentDate = DateTime.Parse("2005-08-11") }
            };

            customers.ForEach(s => context.Customers.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var orders = new List<Order>
            {
                new Order{
                            CustomerID = customers.Single(s=> s.LastName == "Li").CustomerID,
                            ProductID = products.Single(s=> s.Name == "PPOIL").ProductID,
                            OrderDate = customers.Single(s=> s.LastName == "Li").EnrollmentDate
                        },

                new Order{
                            CustomerID = customers.Single(s=> s.LastName == "Olivetto").CustomerID,
                            ProductID = products.Single(s=> s.Name == "HMOIL").ProductID,
                            OrderDate = customers.Single(s=> s.LastName == "Olivetto").EnrollmentDate
                        },

                new Order{
                            CustomerID = customers.Single(s=> s.LastName == "Anand").CustomerID,
                            ProductID = products.Single(s=> s.Name == "DENIL").ProductID,
                            OrderDate = customers.Single(s=> s.LastName == "Anand").EnrollmentDate
                        }
            };

            foreach (Order o in orders)
            {
                var ordersInDB = context.Orders.Where(
                    s =>
                       s.Product.ProductID == o.ProductID &&
                       s.Customer.CustomerID == o.CustomerID).SingleOrDefault();
                if (ordersInDB == null)
                    context.Orders.Add(o);
            }
            context.SaveChanges();
        }
    }
}
