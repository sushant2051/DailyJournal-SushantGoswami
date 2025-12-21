using Microsoft.EntityFrameworkCore;
using MoodTrack_Journal.Data;
using MoodTrack_Journal.Models;

namespace MoodTrack_Journal.Services;

/// <summary>
/// Service implementation for managing journal entries.
/// Ensures only one entry per day and handles system-generated timestamps.
/// </summary>
public class JournalService : IJournalService
{
    private readonly ApplicationDbContext _context;

    public JournalService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<JournalEntry?> GetEntryByDateAsync(DateTime date)
    {
        var dateOnly = date.Date;
        return await _context.JournalEntries
            .Include(e => e.PrimaryMood)
            .Include(e => e.SecondaryMood1)
            .Include(e => e.SecondaryMood2)
            .Include(e => e.JournalEntryTags)
                .ThenInclude(jet => jet.Tag)
            .FirstOrDefaultAsync(e => e.Date.Date == dateOnly);
    }

    public async Task<JournalEntry?> GetEntryByIdAsync(int id)
    {
        return await _context.JournalEntries
            .Include(e => e.PrimaryMood)
            .Include(e => e.SecondaryMood1)
            .Include(e => e.SecondaryMood2)
            .Include(e => e.JournalEntryTags)
                .ThenInclude(jet => jet.Tag)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<JournalEntry>> GetAllEntriesAsync()
    {
        return await _context.JournalEntries
            .Include(e => e.PrimaryMood)
            .Include(e => e.SecondaryMood1)
            .Include(e => e.SecondaryMood2)
            .Include(e => e.JournalEntryTags)
                .ThenInclude(jet => jet.Tag)
            .OrderByDescending(e => e.Date)
            .ToListAsync();
    }

    public async Task<JournalEntry> CreateEntryAsync(JournalEntry entry)
    {
        // Ensure only one entry per day
        var dateOnly = entry.Date.Date;
        var existingEntry = await GetEntryByDateAsync(dateOnly);
        if (existingEntry != null)
        {
            throw new InvalidOperationException($"An entry already exists for {dateOnly:yyyy-MM-dd}. Only one entry per day is allowed.");
        }

        // Set system-generated timestamps
        entry.CreatedAt = DateTime.Now;
        entry.UpdatedAt = DateTime.Now;
        entry.Date = dateOnly; // Ensure date is normalized

        _context.JournalEntries.Add(entry);
        await _context.SaveChangesAsync();
        return entry;
    }

    public async Task<JournalEntry> UpdateEntryAsync(JournalEntry entry)
    {
        var existingEntry = await GetEntryByIdAsync(entry.Id);
        if (existingEntry == null)
        {
            throw new InvalidOperationException($"Entry with ID {entry.Id} not found.");
        }

        // Update properties
        existingEntry.Title = entry.Title;
        existingEntry.Content = entry.Content;
        existingEntry.PrimaryMoodId = entry.PrimaryMoodId;
        existingEntry.SecondaryMood1Id = entry.SecondaryMood1Id;
        existingEntry.SecondaryMood2Id = entry.SecondaryMood2Id;
        existingEntry.Category = entry.Category;
        existingEntry.UpdatedAt = DateTime.Now; // Update timestamp

        // Update tags
        _context.JournalEntryTags.RemoveRange(existingEntry.JournalEntryTags);
        foreach (var tag in entry.JournalEntryTags)
        {
            existingEntry.JournalEntryTags.Add(new JournalEntryTag
            {
                JournalEntryId = existingEntry.Id,
                TagId = tag.TagId
            });
        }

        await _context.SaveChangesAsync();
        return existingEntry;
    }

    public async Task<bool> DeleteEntryAsync(int id)
    {
        var entry = await GetEntryByIdAsync(id);
        if (entry == null)
        {
            return false;
        }

        _context.JournalEntries.Remove(entry);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EntryExistsForDateAsync(DateTime date)
    {
        var dateOnly = date.Date;
        return await _context.JournalEntries
            .AnyAsync(e => e.Date.Date == dateOnly);
    }
}

