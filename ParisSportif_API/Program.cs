using Microsoft.EntityFrameworkCore;
using ParisSportif_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ParisSportifContext>(options => options.UseNpgsql("Host=localhost;Port=5432;Database=ParisSportifAPI;Username=postgres;"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<ParisSportifContext>();
        var env = services.GetRequiredService<IWebHostEnvironment>();

        DbInitializer.Initialize(context, env);

        Console.WriteLine("✔ Base de données seedée avec succès !");
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Erreur lors du seeding de la base de données :");
        Console.WriteLine(ex.Message);

        if (ex.InnerException != null)
            Console.WriteLine("➡ InnerException : " + ex.InnerException.Message);
    }
}

app.MapControllers();

app.Run();
