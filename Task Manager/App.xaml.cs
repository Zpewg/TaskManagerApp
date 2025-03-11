using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Task_Manager.Repository;


namespace Task_Manager
{
    public partial class App : Application
    {

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyAppDbContext>();
            services.AddSingleton<MainWindow>();
            var connection = new MyAppDbContext();
            if (connection.TestConnection())
            {
                MessageBox.Show("Connection established");
            }

        }

     
    }
}