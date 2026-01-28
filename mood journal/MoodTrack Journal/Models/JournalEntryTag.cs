namespace MoodTrack_Journal.Models;

/// <summary>
/// Junction table for many-to-many relationship between JournalEntry and Tag.
/// </summary>
public class JournalEntryTag
{
    public int Id { get; set; }
    public int JournalEntryId { get; set; }
    public JournalEntry JournalEntry { get; set; } = null!;
    public int TagId { get; set; }
    public Tag Tag { get; set; } = null!;
}

