using MoodTrack_Journal.Models;

namespace MoodTrack_Journal.Services;

/// <summary>
/// Service interface for managing moods.
/// Provides operations to retrieve moods by category.
/// </summary>
public interface IMoodService
{
    Task<List<Mood>> GetAllMoodsAsync();
    Task<List<Mood>> GetMoodsByCategoryAsync(MoodCategory category);
    Task<Mood?> GetMoodByIdAsync(int id);
}

