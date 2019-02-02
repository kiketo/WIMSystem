using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Core.Contracts;

namespace WIMSystem.Core
{
    internal class MenuCommandParser : ICommandParser
    {
        private string commandString;
        
        public IList<ICommand> ReadCommands()
        {
            var commands = new List<ICommand>();

            var currentCommandString = this.commandString;

            var currentCommand = Command.Parse(currentCommandString);

            commands.Add(currentCommand);
            
            return commands;
        }

        public void SaveCommand(string commandString)
        {
            this.commandString = commandString; 
        }
    }
}
