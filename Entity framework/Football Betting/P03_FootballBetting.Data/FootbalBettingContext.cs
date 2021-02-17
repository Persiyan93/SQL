using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data.Models;
using System;

namespace P03_FootballBetting.Data
{
    public class FootbalBettingContext : DbContext
    {
        public FootbalBettingContext()
        {

        }
        public FootbalBettingContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<User> Users { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=FootballBetting;Integrated Security=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Team>(entity => {
                entity.HasKey(t => t.TeamId);

                entity.Property(t => t.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

                entity.
                    Property(t => t.LogoUrl)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity
                    .Property(t => t.Initials)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(3);
                entity
                    .HasOne(t => t.PrimaryKitColor)
                    .WithMany(c => c.PrimaryKitTeams)
                    .HasForeignKey(t => t.PrimaryKitColorId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity
                    .HasOne(t => t.SecondaryKitColor)
                    .WithMany(c => c.SecondaryKitTeams)
                    .HasForeignKey(t => t.SecondaryKitColorId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity
                    .HasOne(t => t.Town)
                    .WithMany(t => t.Teams)
                    .HasForeignKey(t => t.TownId);

            });
            modelBuilder.Entity<Color>(entity => {
                entity.HasKey(c => c.ColorId);
                entity
                    .Property(c => c.Name)
                    .IsRequired(true)
                    .HasMaxLength(50);
            });
            modelBuilder.Entity<Town>(entity => {
                entity
                    .HasKey(t => t.TownId);

                entity
                    .Property(t => t.Name)
                    .IsRequired(true)
                    .HasMaxLength(50);

                entity
                    .HasOne(t => t.Country)
                    .WithMany(c => c.Towns)
                    .HasForeignKey(t => t.CountryId);

            });
            modelBuilder.Entity<Country>(entity => {
                entity.HasKey(c => c.CountryId);

                entity
                    .Property(c => c.Name)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .IsRequired(true);

            });
            modelBuilder.Entity<Player>(entity => {
                entity.HasKey(p => p.PlayerId);


                entity
                    .HasOne(p => p.Team)
                    .WithMany(t => t.Players)
                    .HasForeignKey(p => p.TeamId);
                entity
                    .HasOne(p => p.Position)
                    .WithMany(p => p.Players)
                    .HasForeignKey(p => p.PositionId);


            });
            modelBuilder.Entity<Position>(enitity => {
                enitity.HasKey(p => p.PositionId);

                enitity
                    .Property(p => p.Name)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(200);
            });
            modelBuilder.Entity<PlayerStatistic>(entity=> {
                entity
                    .HasKey(x => new { x.GameId ,x.PlayerId});

                entity
                    .HasOne(x => x.Player)
                    .WithMany(x => x.PlayerStatistic)
                    .HasForeignKey(x => x.PlayerId);

                entity
                    .HasOne(x => x.Game)
                    .WithMany(x => x.PlayerStatistic)
                    .HasForeignKey(x => x.GameId);

                    


            });
            modelBuilder.Entity<Game>(entity => {

                entity.HasKey(x => x.GameId);

                entity
                    .Property(x => x.Result)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsRequired(true);

                entity
                    .HasOne(x => x.AwayTeam)
                    .WithMany(x => x.AwayGames)
                    .HasForeignKey(x => x.AwayTeamId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity
                    .HasOne(x => x.Hometeam)
                    .WithMany(x => x.HomeGames)
                    .HasForeignKey(x => x.HomeTeamId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
            });
            modelBuilder.Entity<Bet>(entity =>
            {
                entity.HasKey(e => e.BetId);

                entity
                    .HasOne(x => x.Game)
                    .WithMany(x => x.Bets)
                    .HasForeignKey(x => x.GameId);
                entity
                    .HasOne(x => x.User)
                    .WithMany(x => x.Bets)
                    .HasForeignKey(x => x.UserId);

                
              
            });
            modelBuilder.Entity<User>(entity => {

                entity
                    .Property(x => x.Password)
                    .IsRequired(true)
                    .IsUnicode(false)
                    .HasMaxLength(256);
                entity
                    .Property(x => x.UserName)
                    .IsRequired(true)
                    .IsUnicode(false)
                    .HasMaxLength(256);
                entity
                    .Property(x => x.Email)
                    .IsRequired(true)
                    .IsUnicode(false)
                    .HasMaxLength(100);
                    
            });
        }

    }
}
