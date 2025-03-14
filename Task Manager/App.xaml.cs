using System;
using System.Windows;
using Task_Manager.Entities;
using Microsoft.Extensions.DependencyInjection;
using Task_Manager.Repository;
using Task_Manager.Service;


namespace Task_Manager
{
    public partial class App : Application
    {

        public App()
        {

            var services = new ServiceCollection();
            ConfigureServices(services);
            StartApp();

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
        public async void StartApp()
        {
            var services = new ServiceCollection();

           
            services.AddDbContext<MyAppDbContext>();

          
            services.AddScoped<UserRepository>();
            services.AddScoped<UserService>();

            var serviceProvider = services.BuildServiceProvider();
            var userService = serviceProvider.GetRequiredService<UserService>();

            User user = new User( "Andrei", "Andreeei@gmail.com", "Andrei23", "2384234");
            await userService.createUser(user); 
        }


    }

}