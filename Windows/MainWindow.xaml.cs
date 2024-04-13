using System.Windows;
using TodoListWPF.Services.Interfaces;
using TodoListWPF.Windows.ViewModels;

namespace TodoListWPF {
    public partial class MainWindow : Window {
        private bool IsWindowInitialized {  get; set; }

        public MainWindow(IDatabaseService databaseService) {
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
    }
}