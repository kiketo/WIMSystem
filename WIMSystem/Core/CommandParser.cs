using WIMSystem.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIMSystem.Core
{
    internal class CommandParser : ICommandParser
    {
        public IList<ICommand> ReadCommands(IReader reader)
        {
            var commands = new List<ICommand>();

            var currentLine = Console.ReadLine();
            // var currentLine = reader.Read();

            while (!string.IsNullOrEmpty(currentLine))
            {
                var currentCommand = Command.Parse(currentLine);
                commands.Add(currentCommand);

                currentLine = Console.ReadLine();
                // var currentLine = reader.Read();

            }

            return commands;
        }
    }
}
