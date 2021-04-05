using Game_2048.ViewModels;
using System.Windows;

namespace Game_2048.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainView : Window
    {
        public MainView(IViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
