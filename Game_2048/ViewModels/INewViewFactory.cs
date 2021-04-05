using Game_2048.Models;

namespace Game_2048.ViewModels
{
    internal interface INewViewFactory
    {
        void CreateNewView(IGameField gamefield, string message);
    }
}
