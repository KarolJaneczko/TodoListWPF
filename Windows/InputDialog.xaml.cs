using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TodoListWPF.Classes.Database.Entities;
using TodoListWPF.Windows.ViewModels;

namespace TodoListWPF.Windows {
    public partial class InputDialog : Window {
        public UserTask NewUserTask { get; set; }

        public InputDialog() {
            DataContext = new InputDialogViewModel();
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e) {
            var dataContext = DataContext as InputDialogViewModel;
            NewUserTask = dataContext.NewUserTask;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            DataContext = new InputDialogViewModel();
            DialogResult = false;
        }
    }
}
