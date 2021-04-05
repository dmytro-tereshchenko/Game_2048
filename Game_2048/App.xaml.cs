using System.Windows;

namespace Game_2048
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DirectorGameBuilder director = new DirectorGameBuilder();
            var view = director.Construct(new GameBuilder());
            view.Show();
        }
    }
}
