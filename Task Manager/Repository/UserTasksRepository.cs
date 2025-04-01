using Task_Manager.Entities;
using Microsoft.EntityFrameworkCore;

namespace Task_Manager.Repository;

public class UserTasksRepository
{
    private readonly MyAppDbContext _context;

    public UserTasksRepository(MyAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserTasks>> GetUserTasks()
    {
        return await _context.UserTasks.ToListAsync();
    }

    public async Task<List<UserTasks>> GetUserTasksByUserId(int userId)
    {
        return await _context.UserTasks.Where(t=> t.idUser == userId).ToListAsync();
    }

    public async Task AddUserTask(UserTasks userTasks)
    {
        await _context.UserTasks.AddAsync(userTasks);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserTask(UserTasks userTasks)
    {
        _context.Entry(userTasks).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserTask(UserTasks userTasks)
    {
        
         _context.UserTasks.Remove(userTasks);
        await _context.SaveChangesAsync();
    }

    public async Task<int?> FindUserByTaskName(string taskName)
    {
        return await _context.UserTasks
            .Where(u=> u.nameOfTask == taskName)
            .Select(u=> (int?)u.idUserTasks)
            .FirstOrDefaultAsync();
    } 
    
    
}