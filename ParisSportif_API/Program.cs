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

app.MapControllers();

// =====================
// INITIALISATION DB
// =====================

// On cr�e un "scope" de services.
// Cela permet de r�soudre les d�pendances de type "scoped" (comme le DbContext) 
// en dehors d'une requ�te HTTP, ici au d�marrage de l'application.
using (var scope = app.Services.CreateScope())
{
    // On r�cup�re le conteneur de services du scope.
    var services = scope.ServiceProvider;

    try
    {
        // On demande une instance de notre DbContext (PokISPOBowlContext),
        // qui servira � appliquer les migrations et ins�rer les donn�es.
        var context = services.GetRequiredService<ParisSportifContext>();

        // On r�cup�re la fabrique de loggers.
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();

        // On cr�e un logger sp�cifique pour la cat�gorie "DbInitializer".
        // Cela nous permettra de suivre les messages g�n�r�s pendant l'initialisation.
        var logger = loggerFactory.CreateLogger("DbInitializer");

        // On lance l'initialisation de la base de donn�es :
        // - Application des migrations
        // - V�rification si la table Clients est vide
        // - Lecture du fichier clients.json et insertion des donn�es si n�cessaire
        DbInitializer.Initialize(context, logger);
    }
    catch (Exception ex)
    {
        // Si une erreur se produit (connexion DB, JSON introuvable, etc.),
        // on r�cup�re un logger pour la cat�gorie "Program" afin de tracer l'erreur.
        var logger = services.GetRequiredService<ILogger<Program>>();

        // On enregistre l'erreur avec un message explicite et la stacktrace.
        logger.LogError(ex, "Erreur lors de l'initialisation de la base de donn�es.");
    }
}
// =====================

app.Run();
