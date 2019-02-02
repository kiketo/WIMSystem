using WIMSystem.Core.Contracts;

namespace WIMSystem.Core.Contracts
{
    public interface IWIMEngine
    {
        void ExecuteCommands(ICommandParser commandParser);
    }
}