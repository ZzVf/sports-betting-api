using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ProjectFootAPI.Model;

namespace ParisSportif_API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ParisSportifContext context, ILogger logger)
        {
            context.Database.Migrate();

            ReadFakeLigues(context, logger);
            ReadFakeClubs(context, logger);
            ReadFakeClients(context, logger);
            ReadFakeMatchs(context, logger);
            ReadFakeBets(context, logger);
        }

        // ===================== LIGUES =====================
        private static void ReadFakeLigues(ParisSportifContext context, ILogger logger)
        {
            if (context.Ligues.Any())
            {
                logger.LogInformation("La base de donnée contient déjà des ligues...");
                return;
            }

            string jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Data/Json", "ligues.json");

            if (!File.Exists(jsonFilePath))
            {
                logger.LogError("Le fichier ligues.json est introuvable...");
                return;
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);

                List<Ligue>? ligues = JsonSerializer.Deserialize<List<Ligue>>(
                    jsonData,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (ligues != null && ligues.Count > 0)
                {
                    context.Ligues.AddRange(ligues);
                    context.SaveChanges();
                    logger.LogInformation($"{ligues.Count} ligues ont été ajoutées dans la table Ligues");
                }
                else
                {
                    logger.LogWarning("Aucune ligue détectée dans ligues.json");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Erreur lors de la lecture de ligues.json : " + ex.Message);
            }
        }

        // ===================== CLUBS =====================
        private static void ReadFakeClubs(ParisSportifContext context, ILogger logger)
        {
            if (context.Clubs.Any())
            {
                logger.LogInformation("La base de donnée contient déjà des clubs...");
                return;
            }

            string jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Data/Json", "clubs.json");

            if (!File.Exists(jsonFilePath))
            {
                logger.LogError("Le fichier clubs.json est introuvable...");
                return;
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);

                List<Club>? clubs = JsonSerializer.Deserialize<List<Club>>(
                    jsonData,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (clubs != null && clubs.Count > 0)
                {
                    context.Clubs.AddRange(clubs);
                    context.SaveChanges();
                    logger.LogInformation($"{clubs.Count} clubs ont été ajoutés dans la table Clubs");
                }
                else
                {
                    logger.LogWarning("Aucun club détecté dans clubs.json");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Erreur lors de la lecture de clubs.json : " + ex.Message);
            }
        }

        // ===================== CLIENTS =====================
        private static void ReadFakeClients(ParisSportifContext context, ILogger logger)
        {
            if (context.Clients.Any())
            {
                logger.LogInformation("La base de donnée contient déjà des clients...");
                return;
            }

            string jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Data/Json", "clients.json");

            if (!File.Exists(jsonFilePath))
            {
                logger.LogError("Le fichier clients.json est introuvable...");
                return;
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);

                List<Client>? clients = JsonSerializer.Deserialize<List<Client>>(
                    jsonData,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (clients != null && clients.Count > 0)
                {
                    context.Clients.AddRange(clients);
                    context.SaveChanges();
                    logger.LogInformation($"{clients.Count} clients ont été ajoutés dans la table Clients");
                }
                else
                {
                    logger.LogWarning("Aucun client détecté dans clients.json");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Erreur lors de la lecture de clients.json : " + ex.Message);
            }
        }

        // ===================== MATCHS =====================
        private static void ReadFakeMatchs(ParisSportifContext context, ILogger logger)
        {
            if (context.Matches.Any())
            {
                logger.LogInformation("La base de donnée contient déjà des matchs...");
                return;
            }

            string jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Data/Json", "matchs.json");

            if (!File.Exists(jsonFilePath))
            {
                logger.LogError("Le fichier matchs.json est introuvable...");
                return;
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);

                List<Match>? matchs = JsonSerializer.Deserialize<List<Match>>(
                    jsonData,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (matchs != null && matchs.Count > 0)
                {
                    context.Matches.AddRange(matchs);
                    context.SaveChanges();
                    logger.LogInformation($"{matchs.Count} matchs ont été ajoutés dans la table Matches");
                }
                else
                {
                    logger.LogWarning("Aucun match détecté dans matchs.json");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Erreur lors de la lecture de matchs.json : " + ex.Message);
            }
        }

        // ===================== BETS =====================
        private static void ReadFakeBets(ParisSportifContext context, ILogger logger)
        {
            if (context.Bets.Any())
            {
                logger.LogInformation("La base de donnée contient déjà des bets...");
                return;
            }

            string jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Data/Json", "bets.json");

            if (!File.Exists(jsonFilePath))
            {
                logger.LogError("Le fichier bets.json est introuvable...");
                return;
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);

                List<Bet>? bets = JsonSerializer.Deserialize<List<Bet>>(
                    jsonData,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (bets != null && bets.Count > 0)
                {
                    context.Bets.AddRange(bets);
                    context.SaveChanges();
                    logger.LogInformation($"{bets.Count} bets ont été ajoutés dans la table Bets");
                }
                else
                {
                    logger.LogWarning("Aucun bet détecté dans bets.json");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Erreur lors de la lecture de bets.json : " + ex.Message);
            }
        }
    }
}
