using System.Threading.Tasks;

namespace GhostChess.Board.Abstractions.Commands
{
    public interface ICommand
    {
        Task Execute();
    }
}
