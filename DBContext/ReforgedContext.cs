namespace ReforgedApi
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ReforgedContext : DbContext
    {
        public ReforgedContext()
            : base("name=ReforgedContext")
        {
        }

        public virtual DbSet<Clan> Clans { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Faction> Factions { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameMode> GameModes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ParticipatesGame> ParticipatesGames { get; set; }
        public virtual DbSet<ParticipatesTeam> ParticipatesTeams { get; set; }
        public virtual DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clan>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Clan)
                .HasForeignKey(e => e.clanId);

            modelBuilder.Entity<Faction>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Faction>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.favoriteFaction)
                .HasForeignKey(e => e.favoriteFactionId);

            modelBuilder.Entity<Faction>()
                .HasMany(e => e.Users1)
                .WithOptional(e => e.rpFaction)
                .HasForeignKey(e => e.rpFactionId);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Teams)
                .WithRequired(e => e.Game)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GameMode>()
                .HasMany(e => e.Games)
                .WithRequired(e => e.GameMode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GameMode>()
                .HasMany(e => e.Scores)
                .WithRequired(e => e.GameMode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.ParticipatesTeams)
                .WithRequired(e => e.Team)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasOptional(e => e.Team1)
                .WithRequired(e => e.Team2);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Clans)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.leaderId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ParticipatesTeams)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Scores)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
