using System;
using System.Windows;
using Task_Manager.Entities;
using Microsoft.Extensions.DependencyInjection;
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
            serviceCollection.AddScoped<UserService>();
            serviceCollection.AddScoped<UserValidation>();
            ServiceProvider = serviceCollection.BuildServiceProvider();
            base.OnStartup(e);
        }
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
 
 
           
            User user = new User( "Andrei", "Andreeei@gmail.com", "Andreiii1!", "0712345679");
            /*
            await userService.createUser(user); 
             
            string? message = await userService.deleteUser("Andreeei@gmail.com");
            MessageBox.Show(message);
            
            List<User> users = new List<User>();
            users = await userService.getUsers();
            foreach (User user in users)
            {
                Console.WriteLine(user.ToString());
            }*/
            var userValidation = ServiceProvider.GetRequiredService<UserValidation>();
            await userValidation.Validate(user);    

            //userService.updateUser();

        }


    }

}