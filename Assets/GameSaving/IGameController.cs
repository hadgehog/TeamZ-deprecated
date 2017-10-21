using System.Threading.Tasks;

namespace GameSaving
{
    public interface IGameController
    {
        Task LoadAsync(string slotName);
        Task SaveAsync(string slotName);
    }
}