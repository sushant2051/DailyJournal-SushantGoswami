namespace MoodTrack_Journal.Models;

/// <summary>
/// Represents a journal entry. Only one entry per day is allowed.
/// Each entry contains rich text/markdown content, moods, tags, and timestamps.
/// </summary>
public class JournalEntry
{
    public int Id { get; set; }
    public DateTime Date { get; set; } // Date of the entry (one entry per day)
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty; // Rich text or Markdown content
    public DateTime CreatedAt { get; set; } // System-generated timestamp
    public DateTime UpdatedAt { get; set; } // System-generated timestamp
    
    // Primary mood (required)
    public int PrimaryMoodId { get; set; }
    public Mood PrimaryMood { get; set; } = null!;
    
    // Secondary moods (optional, up to 2)
    public int? SecondaryMood1Id { get; set; }
    public Mood? SecondaryMood1 { get; set; }
    
    public int? SecondaryMood2Id { get; set; }
    public Mood? SecondaryMood2 { get; set; }
    
    // Category
    public string? Category { get; set; }
    
    // Navigation properties
    public List<JournalEntryTag> JournalEntryTags { get; set; } = new();
}

