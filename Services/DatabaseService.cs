using Microsoft.Data.Sqlite;
using System.IO;
using System.Reflection;
using TodoListWPF.Services.Interfaces;

namespace TodoListWPF.Services {
    public class DatabaseService : IDatabaseService {
        private const string DatabaseFile = "LittleDatabase.db";

        public async Task CreateDatabase() {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var localPath = Path.Combine(assemblyPath, "Database");

            if (!Directory.Exists(localPath)) {
                Directory.CreateDirectory(localPath);
            }

            var filepath = Path.Combine(localPath, DatabaseFile);

            await using var connection = new SqliteConnection(GetConnectionString(filepath));
            await connection.OpenAsync();
            await connection.CloseAsync();
        }

        private static string GetConnectionString(string path) => $"Data Source ={path}";
    }
}
