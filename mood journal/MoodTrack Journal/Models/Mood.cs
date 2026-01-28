namespace MoodTrack_Journal.Models;

/// <summary>
/// Represents a mood that can be associated with journal entries.
/// Moods are categorized as Positive, Neutral, or Negative.
/// </summary>
public class Mood
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public MoodCategory Category { get; set; }
    
    // Navigation property
    public List<JournalEntryMood> JournalEntryMoods { get; set; } = new();
}

/// <summary>
/// Enumeration for mood categories.
/// </summary>
public enum MoodCategory
{
    Positive,
    Neutral,
    Negative
}

