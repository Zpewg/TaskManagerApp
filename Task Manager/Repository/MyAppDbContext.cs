using Task_Manager.Entities;
using Microsoft.EntityFrameworkCore;

namespace Task_Manager.Repository;

public class MyAppDbContext : DbContext
{   
    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<TaskJournal> TaskJournals { get; set; }
    
    public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("Server=localhost;Database=task_management;User=root;Password=Abilitati23!;", 
            ServerVersion.AutoDetect("Server=localhost;Database=task_management;User=root;Password=Abilitati23!;"));
    }
    
}