using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestingUnit;
using Task_Manager.Validations;
using Task_Manager.Repository;
using Task_Manager.Service;
using Task_Manager.Entities;

public class UserTasksValidationTest
{
    private readonly UserTasksValidation _userTasksValidation;
    private readonly UserRepository _userRepository;
    private readonly MyAppDbContext _dbContext;

    public UserTasksValidationTest()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddDbContext<MyAppDbContext>(options =>
            options.UseInMemoryDatabase("UserTasksValidationTestDB"));

        serviceCollection.AddScoped<UserRepository>();
        serviceCollection.AddScoped<UserService>();
        serviceCollection.AddScoped<UserTasksRepository>();
        serviceCollection.AddScoped<UserTasksService>();
        serviceCollection.AddScoped<UserTasksValidation>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        _dbContext = serviceProvider.GetRequiredService<MyAppDbContext>();

        // Populate with initial test data
        _dbContext.Users.Add(new User("TestUser", "ceva@gmail.com", "TestPass123!", "0712345678"));
        _dbContext.SaveChanges();

        _userRepository = serviceProvider.GetRequiredService<UserRepository>();
        _userTasksValidation = serviceProvider.GetRequiredService<UserTasksValidation>();
    }
    private async Task<int> returnInt()
    {
        int? id = await _userRepository.GetUserByMailAsync("ceva@gmail.com");
        int idComplete = (int)id;
        return idComplete;
        
    } 

    [Fact]
    public async Task ValidUserTaskTest()
    {
        int id = await returnInt();
        DateOnly taskDate = DateOnly.FromDateTime(DateTime.Now);
        TimeOnly taskTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1));
        var usertask = new UserTasks(id, "Scoala", "Asada", taskDate, taskTime);
        Assert.Empty(await _userTasksValidation.ValidateUserTask(usertask));
        
    }

    [Fact]
    public async Task InvalidDateTest()
    {
        int id = await returnInt();
        DateOnly taskDate = DateOnly.Parse("2025-03-20");
        TimeOnly taskTime = TimeOnly.Parse("15:00");
        var usertask = new UserTasks(id, "Scoala", "Asada", taskDate, taskTime);
        List<string> errors = await _userTasksValidation.ValidateUserTask(usertask);
        Assert.Contains(errors, x => x.Contains("Invalid Date Range"));
    }

    [Fact]
    public async Task InvalidTimeTest()
    {
        int id = await returnInt();
        DateOnly taskDate = DateOnly.Parse("2025-03-21");
        TimeOnly taskTime = TimeOnly.Parse("9:00");
        var usertask = new UserTasks(id, "Scoala", "Asada", taskDate, taskTime);
        List<string> errors = await _userTasksValidation.ValidateUserTask(usertask);
        Assert.Contains(errors, x => x.Contains("You cannot add a time in the past"));
    }

    [Fact]
    public async Task InvalidUserTaskTest()
    {
        int id = await returnInt();
        DateOnly taskDate = DateOnly.Parse("2025-03-24");
        TimeOnly taskTime = TimeOnly.Parse("15:00");
        var usertask = new UserTasks(id, "ScoalaNameNameNameName", "Asada", taskDate, taskTime);
        List<string> errors = await _userTasksValidation.ValidateUserTask(usertask);
        Assert.Contains(errors, x => x.Contains("Invalid Task Name"));
    }
    
}