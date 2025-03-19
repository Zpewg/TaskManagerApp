﻿using Microsoft.Extensions.DependencyInjection;

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
        serviceCollection.AddDbContext<MyAppDbContext>();
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
        var journal = new TaskJournal(idComplete,"Name", "Description");
        Assert.Empty(await _taskJournalValidation.JournalValidation(journal));
    }
    
}