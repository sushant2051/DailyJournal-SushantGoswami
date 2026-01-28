using Microsoft.EntityFrameworkCore;
using MoodTrack_Journal.Data;
using MoodTrack_Journal.Models;

namespace MoodTrack_Journal.Services;

/// <summary>
/// Service implementation for managing moods.
/// </summary>
public class MoodService : IMoodService
{
    private readonly ApplicationDbContext _context;

    public MoodService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Mood>> GetAllMoodsAsync()
    {
        return await _context.Moods
            .OrderBy(m => m.Category)
            .ThenBy(m => m.Name)
            .ToListAsync();
    }

    public async Task<List<Mood>> GetMoodsByCategoryAsync(MoodCategory category)
    {
        return await _context.Moods
            .Where(m => m.Category == category)
            .OrderBy(m => m.Name)
            .ToListAsync();
    }

    public async Task<Mood?> GetMoodByIdAsync(int id)
    {
        return await _context.Moods.FindAsync(id);
    }
}

