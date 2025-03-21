using System;
using System.Windows;
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
            serviceCollection.AddDbContext<MyAppDbContext>();
            serviceCollection.AddScoped<UserRepository>();
            serviceCollection.AddScoped<UserTasksRepository>();
            serviceCollection.AddScoped<TaskJournalRepository>();
            serviceCollection.AddScoped<UserService>();
            serviceCollection.AddScoped<TaskJournalService>();
            serviceCollection.AddScoped<UserTasksService>();
            ServiceProvider = serviceCollection.BuildServiceProvider();
            base.OnStartup(e);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

        }

        private void ConfigureServices(IServiceCollection services)
        {
            
            services.AddSingleton<MainWindow>();
            var connection = new MyAppDbContext();
            if (connection.TestConnection())
            {
                MessageBox.Show("Connection established");
            }

        }


    }

}