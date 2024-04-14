using TodoListWPF.Classes.Database.Entities;

namespace TodoListWPF.Services.Interfaces {
    public interface IDatabaseService {
        public Task CreateDatabase();
        public Task AddUserTasks(IEnumerable<UserTask> userTasks, CancellationToken cancellationToken);
        public Task<IEnumerable<UserTask>> GetUserTasks(UserTaskRequest request, CancellationToken cancellationToken);
        public Task EditTasks(IEnumerable<UserTask> userTasks, CancellationToken cancellationToken);
        public Task RemoveTasks(IEnumerable<int> ids, CancellationToken cancellationToken);
    }
}
