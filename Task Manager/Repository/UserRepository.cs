using Task_Manager.Entities;

namespace Task_Manager.Repository;
using Microsoft.EntityFrameworkCore;

public class UserRepository
{
    private readonly MyAppDbContext _context;

    public UserRepository(MyAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
         _context.Remove(user);
         await _context.SaveChangesAsync();
    }

    public async Task<int?> GetUserByMailAsync(string mail)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.getMail() == mail);

        return user?.getIdUser();
    }
}