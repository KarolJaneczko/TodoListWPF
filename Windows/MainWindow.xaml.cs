using System.Windows;
using TodoListWPF.Services.Interfaces;
using TodoListWPF.Windows.ViewModels;

namespace TodoListWPF {
    public partial class MainWindow : Window {
        public MainWindow(IDatabaseService databaseService) {
            InitializeComponent();
            DataContext = new MainWindowViewModel(databaseService);
        }

        protected override async void OnActivated(EventArgs e) {
            var dataContext = DataContext as MainWindowViewModel;
            await dataContext.GetUserTasks(new CancellationToken());
        }
    }
}