using System.Windows;
using TodoListWPF.Services.Interfaces;
using TodoListWPF.Windows;
using TodoListWPF.Windows.ViewModels;

namespace TodoListWPF {
    public partial class MainWindow : Window {
        private IDatabaseService DatabaseService { get; set; }
        private bool IsWindowInitialized { get; set; }

        public MainWindow(IDatabaseService databaseService) {
            DatabaseService = databaseService;
            DataContext = new MainWindowViewModel(databaseService);
            InitializeComponent();
        }

        protected override async void OnInitialized(EventArgs e) {
            base.OnInitialized(e);
            if (!IsWindowInitialized) {
                IsWindowInitialized = true;
                var dataContext = DataContext as MainWindowViewModel;
                await dataContext.GetUserTasks(new CancellationToken());
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e) {
            var dataContext = DataContext as MainWindowViewModel;
            dataContext.FilterDate = null;
            dataContext.SelectedTask = null;
        }

        private async void Add_Click(object sender, RoutedEventArgs e) {
            var dialog = new InputDialog();
            if (dialog.ShowDialog() ?? false) {
                await DatabaseService.AddUserTasks([dialog.NewUserTask], new CancellationToken());
                var dataContext = DataContext as MainWindowViewModel;
                await dataContext.GetUserTasks(new CancellationToken());
            }
        }

        private async void Edit_Click(object sender, RoutedEventArgs e) {
            var dataContext = DataContext as MainWindowViewModel;
            var dialog = new EditDialog(dataContext.SelectedTask);
            if (dialog.ShowDialog() ?? false) {
                if (dialog.EditResult == Classes.Enums.EditResult.Edit) {
                    await DatabaseService.EditTasks([dialog.EditUserTask], new CancellationToken());
                } else if (dialog.EditResult == Classes.Enums.EditResult.Delete) {
                    await DatabaseService.RemoveTasks([dialog.EditUserTask.TaskId], new CancellationToken());
                }
                await dataContext.GetUserTasks(new CancellationToken());
                dataContext.SelectedTask = null;
            }
        }
    }
}