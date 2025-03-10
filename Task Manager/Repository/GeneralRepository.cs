namespace Task_Manager.Repository;

public abstract class GeneralRepository<T> where T : class
{
    protected readonly MyAppDbContext _context;

    public GeneralRepository(MyAppDbContext context)
    {
        _context = context;
    }
    
    public abstract Task<T> GetByIdAsync(int id);
    public abstract Task<List<T>> GetAll();
    public abstract Task AddAsync(T entity);
    public abstract Task UpdateAsync(T entity);
    public abstract Task DeleteAsync(T entity);
    
}