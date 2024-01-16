using CoffeeShop.PointOfSale.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoffeeShop.PointOfSale.EntityFramework;

internal class ProductsContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
        optionsBuilder
        .UseSqlite($"Data Source = products.db")
        .EnableSensitiveDataLogging()
        .UseLoggerFactory(GetLoggerFactory());

    private ILoggerFactory GetLoggerFactory()
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddFilter((category, level) =>
                category == DbLoggerCategory.Database.Command.Name && level == LogLevel.None);
        });

        return loggerFactory;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderProduct>()
            .HasKey(op => new {op.OrderId, op.ProductId});

        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderId);

        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Product)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.ProductId);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(o => o.Products)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<Category>()
            .HasData(new List<Category>
            {
                new Category
                {
                    CategoryId = 1,
                    Name = "Hot Coffee"
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Cold Coffee"
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "Snacks"
                }
            });

        modelBuilder.Entity<Product>()
            .HasData(new List<Product>
            {
                new Product
                {
                    ProductId = 1,
                    CategoryId = 1,
                    Name = "Espresso",
                    Price = 2.5m
                },
                new Product
                {
                    ProductId = 2,
                    CategoryId = 1,
                    Name = "Cappuccino",
                    Price = 2.5m
                },
                new Product
                {
                    ProductId = 3,
                    CategoryId = 2,
                    Name = "Chilled Latte",
                    Price = 4.2m
                },
                new Product
                {
                    ProductId = 4,
                    CategoryId = 3,
                    Name = "Mozzarella Sticks",
                    Price = 2.8m
                },
                new Product
                {
                    ProductId = 5,
                    CategoryId = 3,
                    Name = "Sandwich",
                    Price = 3.0m
                },
                new Product
                {
                    ProductId = 6,
                    CategoryId = 3,
                    Name = "Pizza",
                    Price = 3.5m
                }
            });

        modelBuilder.Entity<Order>()
            .HasData(new List<Order>
            {
                new Order
                {
                    OrderId = 1,
                    CreatedDate = DateTime.Now.AddMonths(-1),
                    TotalPrice = 13.5m
                },
                new Order
                {
                    OrderId = 2,
                    CreatedDate = DateTime.Now.AddMonths(-1),
                    TotalPrice = 68.6m
                },
                new Order
                {
                    OrderId = 3,
                    CreatedDate = DateTime.Now.AddMonths(-2),
                    TotalPrice = 5
                },
                new Order
                {
                    OrderId = 4,
                    CreatedDate = DateTime.Now.AddMonths(-2),
                    TotalPrice = 75
                },
                new Order
                {
                    OrderId = 5,
                    CreatedDate = DateTime.Now.AddMonths(-3),
                    TotalPrice = 11.2m
                },
                new Order
                {
                    OrderId = 6,
                    CreatedDate = DateTime.Now.AddMonths(-3),
                    TotalPrice = 17.5m
                },
                new Order
                {
                    OrderId = 7,
                    CreatedDate = DateTime.Now.AddMonths(-3),
                    TotalPrice = 66
                },
                new Order
                {
                    OrderId = 8,
                    CreatedDate = DateTime.Now.AddYears(-1),
                    TotalPrice = 30
                }
            });

        modelBuilder.Entity<OrderProduct>()
            .HasData(new List<OrderProduct>
            {
                new OrderProduct
                {
                    OrderId = 1,
                    ProductId = 2,
                    Quantity = 3
                },
                new OrderProduct
                {
                    OrderId = 1,
                    ProductId = 5,
                    Quantity = 2
                },
                new OrderProduct
                {
                    OrderId = 2,
                    ProductId = 6,
                    Quantity = 10
                },
                new OrderProduct
                {
                    OrderId = 2,
                    ProductId = 3,
                    Quantity = 8
                },
                new OrderProduct
                {
                    OrderId = 3,
                    ProductId = 1,
                    Quantity = 2
                },
                new OrderProduct
                {
                    OrderId = 4,
                    ProductId = 5,
                    Quantity = 15
                },
                new OrderProduct
                {
                    OrderId = 4,
                    ProductId = 1,
                    Quantity = 12
                },
                new OrderProduct
                {
                    OrderId = 5,
                    ProductId = 4,
                    Quantity = 4
                },
                new OrderProduct
                {
                    OrderId = 6,
                    ProductId = 2,
                    Quantity = 7
                },
                new OrderProduct
                {
                    OrderId = 7,
                    ProductId = 6,
                    Quantity = 11
                },
                new OrderProduct
                {
                    OrderId = 7,
                    ProductId = 2,
                    Quantity = 11
                },
                new OrderProduct
                {
                    OrderId = 8,
                    ProductId = 1,
                    Quantity = 12
                }
            });
    }

}
