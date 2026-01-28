namespace MoodTrack_Journal.Models;

/// <summary>
/// Junction table for many-to-many relationship between JournalEntry and Mood.
/// This is used for tracking moods associated with entries.
/// </summary>
public class JournalEntryMood
{
    public int Id { get; set; }
    public int JournalEntryId { get; set; }
    public JournalEntry JournalEntry { get; set; } = null!;
    public int MoodId { get; set; }
    public Mood Mood { get; set; } = null!;
    public bool IsPrimary { get; set; } // Indicates if this is the primary mood
}

