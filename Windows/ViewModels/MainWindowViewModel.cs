using System.ComponentModel;
using TodoListWPF.Classes.Database.Entities;
using TodoListWPF.Services.Interfaces;

namespace TodoListWPF.Windows.ViewModels {
    public class MainWindowViewModel(IDatabaseService databaseService) : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private IDatabaseService DatabaseService { get; } = databaseService;

        private List<UserTask> UserTasks { get; set; }

        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task GetUserTasks(CancellationToken cancellationToken) {
            UserTasks = (await DatabaseService.GetUserTasks(new UserTaskRequest(), cancellationToken)).ToList();
        }
    }
}
