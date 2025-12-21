using MoodTrack_Journal.Models;

namespace MoodTrack_Journal.Services;

/// <summary>
/// Service interface for managing journal entries.
/// Provides CRUD operations for journal entries with one entry per day constraint.
/// </summary>
public interface IJournalService
{
    Task<JournalEntry?> GetEntryByDateAsync(DateTime date);
    Task<JournalEntry?> GetEntryByIdAsync(int id);
    Task<List<JournalEntry>> GetAllEntriesAsync();
    Task<JournalEntry> CreateEntryAsync(JournalEntry entry);
    Task<JournalEntry> UpdateEntryAsync(JournalEntry entry);
    Task<bool> DeleteEntryAsync(int id);
    Task<bool> EntryExistsForDateAsync(DateTime date);
}

