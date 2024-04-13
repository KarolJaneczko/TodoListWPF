using TodoListWPF.Classes.Enums;

namespace TodoListWPF.Classes.Database.Entities {
    public class UserTaskRequest {
        public TaskType? TaskType { get; set; }
        public DateOnly? TargetDate { get; set; }
    }
}
