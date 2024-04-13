using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TodoListWPF.Classes.Enums;

namespace TodoListWPF.Classes.Database.Entities {
    public class UserTask {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }
        public string Name { get; set; }
        public TaskType TaskType { get; set; }
        public string Description { get; set; }
        public DateOnly TargetDate { get; set; }
    }
}
