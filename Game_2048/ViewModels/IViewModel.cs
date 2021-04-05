using System.ComponentModel;

namespace Game_2048.ViewModels
{
    internal interface IViewModel
    {
        event PropertyChangedEventHandler PropertyChanged;
    }
}
