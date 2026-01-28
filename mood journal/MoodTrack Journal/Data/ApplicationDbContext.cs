using Microsoft.EntityFrameworkCore;
using MoodTrack_Journal.Models;

namespace MoodTrack_Journal.Data;

/// <summary>
/// Application database context for SQLite database.
/// Manages all database entities and relationships.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public DbSet<JournalEntry> JournalEntries { get; set; }
    public DbSet<Mood> Moods { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<JournalEntryMood> JournalEntryMoods { get; set; }
    public DbSet<JournalEntryTag> JournalEntryTags { get; set; }
    public DbSet<UserSettings> UserSettings { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure JournalEntry
        modelBuilder.Entity<JournalEntry>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Date).IsUnique(); // Ensure one entry per day
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
            
            // Relationships
            entity.HasOne(e => e.PrimaryMood)
                .WithMany()
                .HasForeignKey(e => e.PrimaryMoodId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.SecondaryMood1)
                .WithMany()
                .HasForeignKey(e => e.SecondaryMood1Id)
                .OnDelete(DeleteBehavior.SetNull);
            
            entity.HasOne(e => e.SecondaryMood2)
                .WithMany()
                .HasForeignKey(e => e.SecondaryMood2Id)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Configure Mood
        modelBuilder.Entity<Mood>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Configure Tag
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Configure JournalEntryMood
        modelBuilder.Entity<JournalEntryMood>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.JournalEntry)
                .WithMany()
                .HasForeignKey(e => e.JournalEntryId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Mood)
                .WithMany(m => m.JournalEntryMoods)
                .HasForeignKey(e => e.MoodId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure JournalEntryTag
        modelBuilder.Entity<JournalEntryTag>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.JournalEntry)
                .WithMany(e => e.JournalEntryTags)
                .HasForeignKey(e => e.JournalEntryId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Tag)
                .WithMany(t => t.JournalEntryTags)
                .HasForeignKey(e => e.TagId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure UserSettings
        modelBuilder.Entity<UserSettings>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Theme).HasMaxLength(20).HasDefaultValue("Light");
        });

        // Seed initial data
        SeedData(modelBuilder);
    }

    /// <summary>
    /// Seeds the database with initial moods and predefined tags.
    /// </summary>
    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Moods
        var moods = new List<Mood>
        {
            // Positive moods
            new Mood { Id = 1, Name = "Happy", Category = MoodCategory.Positive },
            new Mood { Id = 2, Name = "Excited", Category = MoodCategory.Positive },
            new Mood { Id = 3, Name = "Relaxed", Category = MoodCategory.Positive },
            new Mood { Id = 4, Name = "Grateful", Category = MoodCategory.Positive },
            new Mood { Id = 5, Name = "Confident", Category = MoodCategory.Positive },
            
            // Neutral moods
            new Mood { Id = 6, Name = "Calm", Category = MoodCategory.Neutral },
            new Mood { Id = 7, Name = "Thoughtful", Category = MoodCategory.Neutral },
            new Mood { Id = 8, Name = "Curious", Category = MoodCategory.Neutral },
            new Mood { Id = 9, Name = "Nostalgic", Category = MoodCategory.Neutral },
            new Mood { Id = 10, Name = "Bored", Category = MoodCategory.Neutral },
            
            // Negative moods
            new Mood { Id = 11, Name = "Sad", Category = MoodCategory.Negative },
            new Mood { Id = 12, Name = "Angry", Category = MoodCategory.Negative },
            new Mood { Id = 13, Name = "Stressed", Category = MoodCategory.Negative },
            new Mood { Id = 14, Name = "Lonely", Category = MoodCategory.Negative },
            new Mood { Id = 15, Name = "Anxious", Category = MoodCategory.Negative }
        };

        modelBuilder.Entity<Mood>().HasData(moods);

        // Seed Predefined Tags
        var predefinedTags = new List<Tag>
        {
            new Tag { Id = 1, Name = "Work", IsPredefined = true },
            new Tag { Id = 2, Name = "Career", IsPredefined = true },
            new Tag { Id = 3, Name = "Studies", IsPredefined = true },
            new Tag { Id = 4, Name = "Family", IsPredefined = true },
            new Tag { Id = 5, Name = "Friends", IsPredefined = true },
            new Tag { Id = 6, Name = "Relationships", IsPredefined = true },
            new Tag { Id = 7, Name = "Health", IsPredefined = true },
            new Tag { Id = 8, Name = "Fitness", IsPredefined = true },
            new Tag { Id = 9, Name = "Personal Growth", IsPredefined = true },
            new Tag { Id = 10, Name = "Self-care", IsPredefined = true },
            new Tag { Id = 11, Name = "Hobbies", IsPredefined = true },
            new Tag { Id = 12, Name = "Travel", IsPredefined = true },
            new Tag { Id = 13, Name = "Nature", IsPredefined = true },
            new Tag { Id = 14, Name = "Finance", IsPredefined = true },
            new Tag { Id = 15, Name = "Spirituality", IsPredefined = true },
            new Tag { Id = 16, Name = "Birthday", IsPredefined = true },
            new Tag { Id = 17, Name = "Holiday", IsPredefined = true },
            new Tag { Id = 18, Name = "Vacation", IsPredefined = true },
            new Tag { Id = 19, Name = "Celebration", IsPredefined = true },
            new Tag { Id = 20, Name = "Exercise", IsPredefined = true },
            new Tag { Id = 21, Name = "Reading", IsPredefined = true },
            new Tag { Id = 22, Name = "Writing", IsPredefined = true },
            new Tag { Id = 23, Name = "Cooking", IsPredefined = true },
            new Tag { Id = 24, Name = "Meditation", IsPredefined = true },
            new Tag { Id = 25, Name = "Yoga", IsPredefined = true },
            new Tag { Id = 26, Name = "Music", IsPredefined = true },
            new Tag { Id = 27, Name = "Shopping", IsPredefined = true },
            new Tag { Id = 28, Name = "Parenting", IsPredefined = true },
            new Tag { Id = 29, Name = "Projects", IsPredefined = true },
            new Tag { Id = 30, Name = "Planning", IsPredefined = true },
            new Tag { Id = 31, Name = "Reflection", IsPredefined = true }
        };

        modelBuilder.Entity<Tag>().HasData(predefinedTags);
    }
}

