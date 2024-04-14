using System.Collections.ObjectModel;
using TodoListWPF.Classes.Database.Entities;
using TodoListWPF.Services.Interfaces;

namespace TodoListWPF.Windows.ViewModels {
    public class MainWindowViewModel(IDatabaseService databaseService) : BaseViewModel {
        private IDatabaseService DatabaseService { get; } = databaseService;
        private ObservableCollection<UserTask> userTasks;
        private UserTask selectedTask;
        private DateOnly? filterDate;

        public ObservableCollection<UserTask> UserTasks {
            get {
                if (FilterDate is not null) {
                    return new ObservableCollection<UserTask>(userTasks.Where(x => x.TargetDate == FilterDate));
                } else {
                    return userTasks;
                }
            }
            set {
                userTasks = value;
                OnPropertyChanged(nameof(UserTasks));
            }
        }

        public UserTask SelectedTask {
            get => selectedTask; set {
                selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
                OnPropertyChanged(nameof(IsEditEnabled));
            }
        }

        public DateOnly? FilterDate {
            get => filterDate; set {
                filterDate = value;
                OnPropertyChanged(nameof(FilterDate));
                OnPropertyChanged(nameof(UserTasks));
            }
        }

        public bool IsEditEnabled => SelectedTask is not null;

        public async Task GetUserTasks(CancellationToken cancellationToken) {
            UserTasks = new ObservableCollection<UserTask>(await DatabaseService.GetUserTasks(new UserTaskRequest(), cancellationToken));
        }
    }
}
