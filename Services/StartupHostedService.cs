using Microsoft.Extensions.Hosting;
using TodoListWPF.Services.Interfaces;

namespace TodoListWPF.Services {
    public class StartupHostedService(IDatabaseService databaseService) : BackgroundService {
        private IDatabaseService DatabaseService { get; } = databaseService;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            try {
                await DatabaseService.CreateDatabase();
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
