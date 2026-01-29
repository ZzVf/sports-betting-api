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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Club>()
            .HasOne(c => c.Ligue)
            .WithMany(l => l.Clubs)
            .HasForeignKey(c => c.LigueId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.Club1)
            .WithMany(c => c.MatchesClub1)
            .HasForeignKey(m => m.ClubId1)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.Club2)
            .WithMany(c => c.MatchesClub2)
            .HasForeignKey(m => m.ClubId2)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Bet>()
            .HasOne(b => b.Client)
            .WithMany(c => c.Bets)
            .HasForeignKey(b => b.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
        base.OnModelCreating(modelBuilder);
    }


}
