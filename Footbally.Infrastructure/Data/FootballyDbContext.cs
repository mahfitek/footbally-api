using Footbally.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Footbally.Infrastructure.Data;

public class FootballyDbContext : DbContext
{
    public FootballyDbContext(DbContextOptions<FootballyDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<PlayerProfile> PlayerProfiles => Set<PlayerProfile>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<TeamProfile> TeamProfiles => Set<TeamProfile>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<MatchApplication> MatchApplications => Set<MatchApplication>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();

    // AI Tables
    public DbSet<AiJob> AiJobs => Set<AiJob>();
    public DbSet<AiPlayerRating> AiPlayerRatings => Set<AiPlayerRating>();
    public DbSet<AiTrustScore> AiTrustScores => Set<AiTrustScore>();
    public DbSet<AiScoutReport> AiScoutReports => Set<AiScoutReport>();
    public DbSet<AiModerationResult> AiModerationResults => Set<AiModerationResult>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.FullName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.Role).HasMaxLength(20).HasDefaultValue("Player");
        });

        // PlayerProfile
        modelBuilder.Entity<PlayerProfile>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.User)
                .WithOne(u => u.PlayerProfile)
                .HasForeignKey<PlayerProfile>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.Position).HasMaxLength(10);
            entity.Property(e => e.City).HasMaxLength(100);
        });

        // Team
        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Owner)
                .WithMany(u => u.OwnedTeams)
                .HasForeignKey(e => e.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.City).HasMaxLength(100);
        });

        // TeamProfile
        modelBuilder.Entity<TeamProfile>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Team)
                .WithOne(t => t.TeamProfile)
                .HasForeignKey<TeamProfile>(e => e.TeamId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Match
        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(e => e.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(e => e.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(20).HasDefaultValue("Open");
        });

        // MatchApplication
        modelBuilder.Entity<MatchApplication>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Match)
                .WithMany(m => m.Applications)
                .HasForeignKey(e => e.MatchId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.User)
                .WithMany(u => u.MatchApplications)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.Property(e => e.Status).HasMaxLength(20).HasDefaultValue("Pending");
        });

        // Subscription
        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.User)
                .WithMany(u => u.Subscriptions)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.PlanType).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Status).HasMaxLength(20).HasDefaultValue("Active");
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.Status);
        });

        // AiJob
        modelBuilder.Entity<AiJob>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.JobType).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Status).HasMaxLength(30).HasDefaultValue("Pending");
            entity.Property(e => e.TargetEntityType).HasMaxLength(50);
            entity.Property(e => e.ModelUsed).HasMaxLength(50);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.JobType);
            entity.HasIndex(e => e.TargetEntityId);
        });

        // AiPlayerRating
        modelBuilder.Entity<AiPlayerRating>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.AiJob)
                .WithMany()
                .HasForeignKey(e => e.AiJobId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(e => e.PlayerId);
            entity.Property(e => e.CardTier).HasMaxLength(20);
        });

        // AiTrustScore
        modelBuilder.Entity<AiTrustScore>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.AiJob)
                .WithMany()
                .HasForeignKey(e => e.AiJobId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(e => e.PlayerId);
            entity.Property(e => e.ScoreLabel).HasMaxLength(20);
        });

        // AiScoutReport
        modelBuilder.Entity<AiScoutReport>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.AiJob)
                .WithMany()
                .HasForeignKey(e => e.AiJobId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(e => e.PlayerId);
            entity.HasIndex(e => e.ScoutId);
            entity.Property(e => e.Verdict).HasMaxLength(30);
        });

        // AiModerationResult
        modelBuilder.Entity<AiModerationResult>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.AiJob)
                .WithMany()
                .HasForeignKey(e => e.AiJobId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(e => e.TargetEntityId);
            entity.Property(e => e.TargetEntityType).HasMaxLength(50).IsRequired();
            entity.Property(e => e.RiskLevel).HasMaxLength(20).HasDefaultValue("low");
        });
    }
}