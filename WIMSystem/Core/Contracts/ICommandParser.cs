using System.Collections.Generic;
using WIMSystem.Core.Contracts;

namespace WIMSystem.Core.Contracts
{
    internal interface ICommandParser
    {
        IList<ICommand> ReadCommands();
    }
}