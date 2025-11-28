using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ProjectFootAPI.Model;

namespace ParisSportif_API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ParisSportifContext context, IWebHostEnvironment env)
        {
            context.Database.EnsureCreated();

            // Si déjà seedé -> on ne fait rien
            if (context.Ligues.Any())
                return;

            var jsonFolder = Path.Combine(env.ContentRootPath, "Data", "Json");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // 1) Ligues
            var ligues = LoadJson<Ligue[]>(jsonFolder, "Ligue.json", options);
            context.Ligues.AddRange(ligues);
            context.SaveChanges();

            // 2) Clubs
            var clubs = LoadJson<Club[]>(jsonFolder, "Club.json", options);
            context.Clubs.AddRange(clubs);
            context.SaveChanges();

            // 3) Clients
            var clients = LoadJson<Client[]>(jsonFolder, "Client.json", options);
            context.Clients.AddRange(clients);
            context.SaveChanges();

            // 4) Matchs
            var matchs = LoadJson<Match[]>(jsonFolder, "Match.json", options);
            context.Matches.AddRange(matchs);
            context.SaveChanges();

            // 5) Bets
            var bets = LoadJson<Bet[]>(jsonFolder, "Bet.json", options);
            context.Bets.AddRange(bets);
            context.SaveChanges();
        }

        private static T LoadJson<T>(string folder, string fileName, JsonSerializerOptions options)
        {
            var fullPath = Path.Combine(folder, fileName);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"Fichier JSON introuvable : {fullPath}");

            var text = File.ReadAllText(fullPath);

            var result = JsonSerializer.Deserialize<T>(text, options);

            if (result == null)
                throw new Exception($"Impossible de charger '{fileName}'.");

            return result;
        }
    }
}
