using Command.Interfaces;
using System;
using System.Threading.Tasks;   

namespace Command.Commands
{
    public class ExitCommand : ICommand
    {
        public string Text => ": Exit";

        public Task Execute()
        {
            Environment.Exit(0);
            return Task.CompletedTask;
        }
    }
}
