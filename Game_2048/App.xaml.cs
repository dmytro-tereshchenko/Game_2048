using Game_2048.Models;
using Game_2048.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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

            var gameScoreManager = new GameScoreManager();
            var gameField = new GameField(gameScoreManager, 4, 4);
            var view = new MainView();
            view.Show();
        }
    }
}
