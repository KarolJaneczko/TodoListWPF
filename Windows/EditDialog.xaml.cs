using System.Windows;
using TodoListWPF.Classes.Database.Entities;
using TodoListWPF.Classes.Enums;
using TodoListWPF.Windows.ViewModels;

namespace TodoListWPF.Windows {
    public partial class EditDialog : Window {
        public UserTask EditUserTask { get; set; }
        public EditResult EditResult { get; set; }

        public EditDialog(UserTask userTask) {
            EditUserTask = userTask;
            DataContext = new EditDialogViewModel(userTask);
            InitializeComponent();
        }

        private void Edit_Click(object sender, RoutedEventArgs e) {
            var dataContext = DataContext as EditDialogViewModel;
            EditUserTask = dataContext.EditUserTask;
            EditResult = EditResult.Edit;
            DialogResult = true;
        }

        private void Delete_Click(object sender, RoutedEventArgs e) {
            var dataContext = DataContext as EditDialogViewModel;
            EditUserTask = dataContext.EditUserTask;
            EditResult = EditResult.Delete;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            DataContext = new EditDialogViewModel(null);
            EditResult = EditResult.Cancel;
            DialogResult = false;
        }
    }
}
