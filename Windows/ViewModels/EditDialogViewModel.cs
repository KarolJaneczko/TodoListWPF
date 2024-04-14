using TodoListWPF.Classes.Database.Entities;

namespace TodoListWPF.Windows.ViewModels {
    public class EditDialogViewModel(UserTask userTask) : BaseViewModel {
        public UserTask EditUserTask { get; set; } = userTask;

        public string TaskName {
            get => EditUserTask.Name; set {
                EditUserTask.Name = value;
                OnPropertyChanged(nameof(TaskName));
                OnPropertyChanged(nameof(IsConfirmingEnabled));
            }
        }
        public string TaskDescription {
            get => EditUserTask.Description; set {
                EditUserTask.Description = value;
                OnPropertyChanged(nameof(TaskDescription));
                OnPropertyChanged(nameof(IsConfirmingEnabled));
            }
        }

        public DateOnly TaskDate {
            get => EditUserTask.TargetDate; set {
                EditUserTask.TargetDate = value;
                OnPropertyChanged(nameof(TaskDate));
                OnPropertyChanged(nameof(IsConfirmingEnabled));
            }
        }

        public bool IsConfirmingEnabled => !string.IsNullOrEmpty(TaskName) && !string.IsNullOrEmpty(TaskDescription) && TaskDate != default;
    }
}
