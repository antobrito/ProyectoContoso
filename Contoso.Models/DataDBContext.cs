
using Microsoft.EntityFrameworkCore;
using System;


namespace Contoso.Models
{





    public class DataDBContext : DbContext
    {

        public DataDBContext(DbContextOptions<DataDBContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        

    }


}