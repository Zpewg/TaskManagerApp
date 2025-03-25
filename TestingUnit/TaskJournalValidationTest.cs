using System.DirectoryServices.ActiveDirectory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestingUnit;
using Task_Manager.Repository;
using Task_Manager.Entities;
using Task_Manager.Service;
using Task_Manager.Validations;

public class TaskJournalValidationTest
{
    private readonly TaskJournalValidation _taskJournalValidation;
    private readonly UserRepository _userRepository;
    
    private IServiceProvider ServiceProvider{get; set;}

    public TaskJournalValidationTest()
    {
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("C:\\Task Manager App\\Task Manager\\Task Manager\\appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        serviceCollection.AddDbContext<MyAppDbContext>(options => 
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));
        serviceCollection.AddScoped<TaskJournalRepository>();
        serviceCollection.AddScoped<UserRepository>();
        serviceCollection.AddScoped<UserService>();
        serviceCollection.AddScoped<TaskJournalService>();
        serviceCollection.AddScoped<TaskJournalValidation>();
        ServiceProvider = serviceCollection.BuildServiceProvider();
        _taskJournalValidation = ServiceProvider.GetService<TaskJournalValidation>();
        _userRepository = ServiceProvider.GetService<UserRepository>();
    }

    private async Task<int> returnInt()
    {
        int? id = await _userRepository.GetUserByMailAsync("ceva@gmail.com");
        int idComplete = (int)id;
        return idComplete;
        
    } 
    [Fact]
    public async Task ValidTaskJournalTest()
    {
        int idComplete = await returnInt();
        var journal = new TaskJournal(idComplete,"Name2", "Description");
        Assert.Empty(await _taskJournalValidation.JournalValidation(journal));
    }

    [Fact]
    public async Task InvalidTaskJournalName()
    {
        int idComplete = await returnInt();
        var journal = new TaskJournal(idComplete, "NameNameNameNameName", "Description");
        List<string> test =await _taskJournalValidation.JournalValidation(journal);
        Assert.Contains(test, x => x.Contains("Invalid journal name"));
    }
    
    
}