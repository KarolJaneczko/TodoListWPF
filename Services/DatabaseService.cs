using Microsoft.Data.Sqlite;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using TodoListWPF.Classes.Database;
using TodoListWPF.Classes.Database.Entities;
using TodoListWPF.Classes.Enums;
using TodoListWPF.Services.Interfaces;

namespace TodoListWPF.Services {
    public class DatabaseService : IDatabaseService {
        private const string DatabaseFile = "LittleDatabase.db";
        private DatabaseContext DatabaseContext { get; }

        public DatabaseService() {
            DatabaseContext = new DatabaseContext();
        }

        #region Initializing database
        public async Task CreateDatabase() {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var localPath = Path.Combine(assemblyPath, "Database");

            if (!Directory.Exists(localPath)) {
                Directory.CreateDirectory(localPath);
            }

            var filepath = Path.Combine(localPath, DatabaseFile);

            await using var connection = new SqliteConnection(GetConnectionString(filepath));
            await connection.OpenAsync();

            await DatabaseContext.Database.EnsureCreatedAsync();
            await connection.CloseAsync();
        }

        public static string GetConnectionString() => GetConnectionString(DatabaseFile);

        private static string GetConnectionString(string path) => $"Data Source ={path}";
        #endregion

        public async Task AddUserTasks(IEnumerable<UserTask> userTasks, CancellationToken cancellationToken) {
            await DatabaseContext.UserTasks.AddRangeAsync(userTasks, cancellationToken);
            await DatabaseContext.SaveChangesAsync();
        }

        public Task<IEnumerable<UserTask>> GetUserTasks(UserTaskRequest request, CancellationToken cancellationToken) {
            var result = DatabaseContext.UserTasks as IEnumerable<UserTask>;

            if (request.TaskType is not null) {
                result = result.Where(x => x.TaskType == request.TaskType);
            }

            if (request.TargetDate is not null) {
                result = result.Where(x => x.TargetDate == request.TargetDate);
            }

            return Task.FromResult(result);
        }

        public async Task EditTasks(IEnumerable<UserTask> userTasks, CancellationToken cancellationToken) {
            foreach (var task in userTasks) {
                var find = DatabaseContext.UserTasks.FirstOrDefault(x => x.TaskId == task.TaskId);
                if (find is not null) {
                    find.Name = task.Name;
                    find.TaskType = task.TaskType;
                    find.Description = task.Description;
                    find.TargetDate = task.TargetDate;
                }
            }
            await DatabaseContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveTasks(IEnumerable<int> ids, CancellationToken cancellationToken) {
            if (ids?.Any() ?? false) {
                var result = DatabaseContext.UserTasks as IEnumerable<UserTask>;
                var filtered = result.Where(x => ids.Any(y => y == x.TaskId));
                DatabaseContext.RemoveRange(filtered);
            }
            await DatabaseContext.SaveChangesAsync(cancellationToken);
        }
    }
}
