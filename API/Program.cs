using DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<InventoryContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

var app = builder.Build();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

InitializeDatabase(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

static void InitializeDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<InventoryContext>();
    context.Database.Migrate();

    if (!context.Categories.Any())
    {
        var category = new Categories { Name = "Test Category" };
        context.Categories.Add(category);
        context.SaveChanges();

        var product = new Product { Name = "Test Product", Price = 9.99m, Categories = category };
        context.Products.Add(product);
        context.SaveChanges();
    }
}