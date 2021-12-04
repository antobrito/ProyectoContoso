
using Microsoft.EntityFrameworkCore;
using System;

namespace CustomersAPI;


public class DataDBContext : DbContext
{

    public DataDBContext(DbContextOptions<DataDBContext> options) : base(options)
    {
       
    }

    public DbSet<Customer> Customers { get; set; }
 
}
