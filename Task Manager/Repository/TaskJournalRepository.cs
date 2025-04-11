using Task_Manager.Entities;
using Microsoft.EntityFrameworkCore;

namespace Task_Manager.Repository;

public class TaskJournalRepository
{
    private readonly MyAppDbContext _context;

    public TaskJournalRepository(MyAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskJournal>> GetTaskJournalsByUserIdAsync(int userId)
    {
        return await _context.TaskJournal.Where(t => t.iduser == userId).ToListAsync();
    }

    public async Task AddTaskJournalAsync(TaskJournal taskJournal)
    {
        await _context.TaskJournal.AddAsync(taskJournal);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTaskJournalAsync(TaskJournal taskJournal)
    {
        _context.Entry(taskJournal).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskJournalAsync(TaskJournal taskJournal)
    {
        _context.TaskJournal.Remove(taskJournal);
        await _context.SaveChangesAsync();
    }
    
    
}