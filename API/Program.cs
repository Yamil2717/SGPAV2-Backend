using DB;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<InventoryContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

// Registros de repositorios
builder.Services.AddScoped<ProductsInterface, ProductsRepository>();
builder.Services.AddScoped<SalesInterface, SalesRepository>();
builder.Services.AddScoped<CategoryInterface, CategoryRepository>();

var app = builder.Build();

InitializeDatabase(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseAuthorization();
app.MapControllers();
app.Run();

static void InitializeDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<InventoryContext>();
    context.Database.Migrate();

    context.Products.RemoveRange(context.Products);
    context.Categories.RemoveRange(context.Categories);
    context.Sale.RemoveRange(context.Sale);
    context.SaleDetail.RemoveRange(context.SaleDetail);
    context.SaveChanges();

    if (!context.Categories.Any())
    {
        var categories = new List<Categories>
        {
            new Categories { Name = "Electrónicos" },
            new Categories { Name = "Ropa" },
            new Categories { Name = "Alimentos" },
            new Categories { Name = "Libros" },
            new Categories { Name = "Muebles" }
        };

        foreach (var category in categories)
        {
            context.Categories.Add(category);
        }

        context.SaveChanges();
    }

    if (!context.Products.Any())
    {
        var products = new List<Products>
        {
            new Products { Name = "Televisor", Price = 499.99m, Category = context.Categories.First(c => c.Name == "Electrónicos"), Stock = 7 },
            new Products { Name = "Camisa", Price = 29.99m, Category = context.Categories.First(c => c.Name == "Ropa"), Stock = 17 },
            new Products { Name = "Manzanas", Price = 1.99m, Category = context.Categories.First(c => c.Name == "Alimentos"), Stock = 77 },
            new Products { Name = "Libro de cocina", Price = 19.99m, Category = context.Categories.First(c => c.Name == "Libros"), Stock = 27 },
            new Products { Name = "Sofá", Price = 899.99m, Category = context.Categories.First(c => c.Name == "Muebles"), Stock = 37 }
        };

        foreach (var product in products)
        {
            context.Products.Add(product);
        }

        context.SaveChanges();
    }

    if (!context.Sale.Any())
    {
        var sales = new List<Sale>
    {
        new Sale
        {
            Date = DateTime.Now,
            Client = "Cliente 1",
            SaleDetails = new List<SaleDetail>
            {
                new SaleDetail { ProductID = context.Products.First().ID, Quantity = 1, UnitPrice = 19.99m }
            }
        },
        new Sale
        {
            Date = DateTime.Now,
            Client = "Cliente 2",
            SaleDetails = new List<SaleDetail>
            {
                new SaleDetail { ProductID = context.Products.Skip(1).First().ID, Quantity = 2, UnitPrice = 29.99m },
                new SaleDetail { ProductID = context.Products.Skip(2).First().ID, Quantity = 3, UnitPrice = 39.99m }
            }
        },
    };

        foreach (var sale in sales)
        {
            sale.Total = sale.SaleDetails.Sum(sd => sd.Quantity * sd.UnitPrice);

            context.Sale.Add(sale);
        }

        context.SaveChanges();
    }
}