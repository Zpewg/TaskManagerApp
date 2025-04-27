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
    private readonly MyAppDbContext _dbContext;

    public TaskJournalValidationTest()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddDbContext<MyAppDbContext>(options =>
            options.UseInMemoryDatabase("TaskJournalValidationTestDB"));

        serviceCollection.AddScoped<UserRepository>();
        serviceCollection.AddScoped<UserService>();
        serviceCollection.AddScoped<TaskJournalRepository>();
        serviceCollection.AddScoped<TaskJournalService>();
        serviceCollection.AddScoped<TaskJournalValidation>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _dbContext = serviceProvider.GetRequiredService<MyAppDbContext>();

        // Populate with initial test data
        _dbContext.Users.Add(new User("TestUser", "ceva@gmail.com", "TestPass123!", "0712345678"));
        _dbContext.SaveChanges();

        _userRepository = serviceProvider.GetRequiredService<UserRepository>();
        _taskJournalValidation = serviceProvider.GetRequiredService<TaskJournalValidation>();
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