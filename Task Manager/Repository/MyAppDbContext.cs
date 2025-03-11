using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System.IO;
using System.Windows;

namespace Task_Manager.Repository;

public class MyAppDbContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Citește connection string din appsettings.json
            var config = new ConfigurationBuilder()
                .AddJsonFile("C:\\Task Manager App\\Task Manager\\Task Manager\\appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }

    public bool TestConnection()
    {
        try
        {
            return Database.CanConnect();
        }
        catch (Exception ex)
        {
            Console.Write($"{ex.Message}\n{ex.StackTrace}");
            return false;
        }
    }
}