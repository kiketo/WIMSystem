using System.Collections.Generic;
using WIMSystem.Core.Contracts;

namespace WIMSystem.Core.Contracts
{
    public interface ICommandParser
    {
        IList<ICommand> ReadCommands();

        void SaveCommand(string commandString);

    }
}