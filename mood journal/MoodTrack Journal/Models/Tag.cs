namespace MoodTrack_Journal.Models;

/// <summary>
/// Represents a tag that can be associated with journal entries.
/// Tags can be pre-defined (e.g., Work, Health, Travel) or custom user-created tags.
/// </summary>
public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsPredefined { get; set; }
    
    // Navigation property
    public List<JournalEntryTag> JournalEntryTags { get; set; } = new();
}

