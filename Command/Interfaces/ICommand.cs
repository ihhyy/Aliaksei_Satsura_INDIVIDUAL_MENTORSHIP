using System.Threading.Tasks;

namespace Command.Interfaces
{
    public interface ICommand
    {
        string Text { get; }

        Task Execute();
    }
}
