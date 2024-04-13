using System.Collections.ObjectModel;
using System.ComponentModel;
using TodoListWPF.Classes.Database.Entities;
using TodoListWPF.Services.Interfaces;

namespace TodoListWPF.Windows.ViewModels {
    public class MainWindowViewModel(IDatabaseService databaseService) : INotifyPropertyChanged {
        private IDatabaseService DatabaseService { get; } = databaseService;
        private ObservableCollection<UserTask> userTasks;

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<UserTask> UserTasks {
            get => userTasks; set {
                userTasks = value;
                OnPropertyChanged(nameof(UserTasks));
            }
        }

        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task GetUserTasks(CancellationToken cancellationToken) {
            UserTasks = new ObservableCollection<UserTask>(await DatabaseService.GetUserTasks(new UserTaskRequest(), cancellationToken));
        }
    }
}
