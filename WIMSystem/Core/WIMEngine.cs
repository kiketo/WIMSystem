using System;
using System.Collections.Generic;
using WIMSystem.Commands.Contracts;
using WIMSystem.Core.Contracts;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Menu.Contracts;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;
using WIMSystem.Utils;

namespace WIMSystem.Core
{
    internal class WIMEngine : IWIMEngine
    {
        private readonly ICommandsFactory commandsFactory;
        private readonly IPrintReports printReports;
        private readonly IMainMenu mainMenu;

        public WIMEngine(ICommandsFactory commandsFactory, IPrintReports printReports, IMainMenu mainMenu)
        {
            this.commandsFactory = commandsFactory ?? throw new ArgumentException(string.Format(Consts.NULL_OBJECT, nameof(printReports)));
            this.printReports = printReports ?? throw new ArgumentException(string.Format(Consts.NULL_OBJECT, nameof(printReports)));
            this.mainMenu = mainMenu ?? throw new ArgumentException(string.Format(Consts.NULL_OBJECT, nameof(printReports)));
        }

        public void Start()
        {
            ///
        }

        public void ExecuteCommands(ICommandParser commandParser)
        {
            var commands = commandParser.ReadCommands();
            var commandResult = this.ProcessCommands(commands);
            this.printReports.Print(commandResult);
        }

        public void ExecuteCommands(ICommand command)
        {
            var commandResult = this.ProcessCommands(new List<ICommand>() { command });
            this.printReports.Print(commandResult);
        }

        private IList<string> ProcessCommands(IList<ICommand> commands)
        {
            var reports = new List<string>();

            foreach (var command in commands)
            {
                try
                {
                    var engineCommand = this.commandsFactory.GetCommand(command.Name);
                    var report = engineCommand.ReadSingleCommand(command.Parameters);
                    reports.Add(report);
                }
                catch (Exception ex)
                {
                    reports.Add(ex.Message);
                }
            }

            return reports;
        }


    }
}
