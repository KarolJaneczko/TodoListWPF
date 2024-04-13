using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TodoListWPF.Services;
using TodoListWPF.Services.Interfaces;

namespace TodoListWPF {
    public partial class App : Application {
        private ServiceProvider ServiceProvider { get; set; }
        private StartupHostedService StartupHostedService { get; set; }

        public App() {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services) {
            services.AddSingleton<IDatabaseService, DatabaseService>();
            services.AddHostedService<StartupHostedService>();
            services.AddTransient(typeof(MainWindow));
            //services.AddDbContext<EmployeeDbContext>(options =>
            //{
            //    options.UseSqlite("Data Source = Employee.db");
            //});
            //services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e) {
            var databaseService = ServiceProvider.GetService<IDatabaseService>();
            StartupHostedService = new StartupHostedService(databaseService);
            StartupHostedService.StartAsync(new CancellationToken());

            var mainWindow = ServiceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
