using System;
using System.IO;
using System.Windows;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Task_Manager.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Task_Manager.Repository;
using Task_Manager.Service;
using Task_Manager.Validations;



namespace Task_Manager
{
    public partial class App : Application
    {
        
        public static IServiceProvider ServiceProvider { get;  set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("C:\\Task Manager App\\Task Manager\\Task Manager\\appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            serviceCollection.AddDbContext<MyAppDbContext>(options => 
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));
            serviceCollection.AddScoped<UserRepository>();
            serviceCollection.AddScoped<UserTasksRepository>();
            serviceCollection.AddScoped<TaskJournalRepository>();
            serviceCollection.AddScoped<UserService>();
            serviceCollection.AddScoped<TaskJournalService>();
            serviceCollection.AddScoped<UserTasksService>();
            serviceCollection.AddScoped<UserValidation>();
            serviceCollection.AddScoped<SignUpViewModel>();
            ServiceProvider = serviceCollection.BuildServiceProvider();
            base.OnStartup(e);
            
            
            
        }


    }

}