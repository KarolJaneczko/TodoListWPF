using Microsoft.EntityFrameworkCore;
using TodoListWPF.Classes.Database.Entities;
using TodoListWPF.Services;

namespace TodoListWPF.Classes.Database {
    public class DatabaseContext : DbContext {
        public DbSet<UserTask> UserTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite(DatabaseService.GetConnectionString());
        }
    }
}
