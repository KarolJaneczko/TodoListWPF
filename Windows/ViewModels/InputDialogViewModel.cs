using TodoListWPF.Classes.Database.Entities;

namespace TodoListWPF.Windows.ViewModels {
    public class InputDialogViewModel : BaseViewModel {
        public UserTask NewUserTask { get; set; }

        public string TaskName {
            get => NewUserTask.Name; set {
                NewUserTask.Name = value;
                OnPropertyChanged(nameof(TaskName));
                OnPropertyChanged(nameof(IsAddingEnabled));
            }
        }
        public string TaskDescription {
            get => NewUserTask.Description; set {
                NewUserTask.Description = value;
                OnPropertyChanged(nameof(TaskDescription));
                OnPropertyChanged(nameof(IsAddingEnabled));
            }
        }

        public DateOnly TaskDate {
            get => NewUserTask.TargetDate; set {
                NewUserTask.TargetDate = value;
                OnPropertyChanged(nameof(TaskDate));
                OnPropertyChanged(nameof(IsAddingEnabled));
            }
        }

        public bool IsAddingEnabled => !string.IsNullOrEmpty(TaskName) && !string.IsNullOrEmpty(TaskDescription) && TaskDate != default;

        public InputDialogViewModel() {
            NewUserTask = new UserTask() {
                TargetDate = DateOnly.FromDateTime(DateTime.Now)
            };
        }
    }
}
