namespace MoodTrack_Journal.Models;

/// <summary>
/// Represents user settings including password/PIN protection and theme preferences.
/// </summary>
public class UserSettings
{
    public int Id { get; set; }
    public string? PasswordHash { get; set; } // For password protection
    public string? PinHash { get; set; } // For PIN protection
    public string Theme { get; set; } = "Light"; // Light, Dark, or Custom
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

