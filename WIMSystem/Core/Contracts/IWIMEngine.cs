using System.Collections.Generic;
using WIMSystem.Core.Contracts;

namespace WIMSystem.Core.Contracts
{
    public interface IWIMEngine
    {
        void ExecuteCommands(ICommandParser commandParser);
        void ExecuteCommands(IList<ICommand> commands);
    }
}