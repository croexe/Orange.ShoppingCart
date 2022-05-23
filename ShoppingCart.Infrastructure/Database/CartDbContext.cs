using Microsoft.EntityFrameworkCore;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Infrastructure.Database
{
    public class CartDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { //should recive connection string from constructor just hard coded for demo purposes
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-FC6TTT7;Initial Catalog=OrangeTest;Integrated Security=True");
        }
    }
}
