using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


using Task_Manager.Entities;

namespace Task_Manager.Repository;

public class MyAppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<TaskJournal> TaskJournal { get; set; }
    public DbSet<UserTasks> UserTasks { get; set; }
    public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            
            var config = new ConfigurationBuilder()
                .AddJsonFile("C:\\Task Manager App\\Task Manager\\Task Manager\\appsettings.json", optional: false, reloadOnChange: true)
                .Build();
 
            var connectionString = config.GetConnectionString("DefaultConnection");
 
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));
             
        }
    }
}