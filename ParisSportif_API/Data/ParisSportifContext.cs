using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParisSportif_API.Data.Converter;
using ProjectFootAPI.Model;

namespace ParisSportif_API.Data;

public class ParisSportifContext : DbContext
{
    public ParisSportifContext(DbContextOptions<ParisSportifContext> options) : base(options)
    {

    }

    // DbSet pour chaque entité du modèle
    public DbSet<Bet> Bets { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Ligue> Ligues { get; set; }
    public DbSet<Match> Matches { get; set; }


    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateTime>()
            .HaveConversion<UtcConverter>();
    }



}
