using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;

namespace WIMSystem.Commands.ChangeCommands
{
    class ChangePriorityCommand : IEngineCommand
    {
        private readonly IHistoryEventWriter historyEventWriter;
        private readonly IGetters getter;

        public string ReadSingleCommand(IList<string> parameters)
        {
            
        }
    }
}
