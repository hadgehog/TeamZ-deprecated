using System.Threading.Tasks;

namespace GameSaving
{
    public interface IGameController
    {
        Task LoadSavedGameAsync(string slotName);
        Task SaveAsync(string slotName);
    }
}