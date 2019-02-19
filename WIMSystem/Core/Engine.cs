using System;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Core.Contracts;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Menu.Contracts;
using WIMSystem.Utils;

namespace WIMSystem.Core
{
    internal class Engine
    {
        private readonly ICommandsFactory commandsFactory;
        private readonly IPrintReports printReports;
        private readonly IMainMenu mainMenu;
        private readonly IReader reader;
        private readonly ICommandParser parser;

        public IReader ConsoleReader { get; }

        public Engine(ICommandsFactory commandsFactory,
            IPrintReports printReports,
            IMainMenu mainMenu,
            ICommandParser parser1,
            IReader reader)
        {
            this.commandsFactory = commandsFactory ?? throw new ArgumentException(string.Format(Consts.NULL_OBJECT, nameof(printReports)));
            this.printReports = printReports ?? throw new ArgumentException(string.Format(Consts.NULL_OBJECT, nameof(printReports)));
            this.mainMenu = mainMenu ?? throw new ArgumentException(string.Format(Consts.NULL_OBJECT, nameof(printReports)));
            this.reader = reader;
            this.parser = parser1;
        }

        public void Start(bool showLogo)
        {
            if (showLogo)
            {
                this.mainMenu.ShowLogo();
            }

            var myReader = this.mainMenu.InputTypeChooser();
            if (myReader != null)
            {
                this.Run(myReader);
            }
            this.mainMenu.ShowCredits();
        }

        public void Run(IReader reader)
        {
            //var inputString = reader.Read();

            while (true)
            {
                try
                {
                    var inputStringList = reader.Read();

                    foreach (var inputString in inputStringList)
                    {
                        if (inputString == CommandsConsts.TerminationAppCommand)
                        {
                            return;
                        }

                        if (inputString == CommandsConsts.TerminationCommand)
                        {
                            this.printReports.Print();
                            break;
                        }

                        if (inputString == CommandsConsts.ConsoleExitCommand)
                        {
                            this.printReports.Print();
                            this.Start(false);
                            return;
                        }

                        var command = this.parser.Parse(inputString);
                        var engineCommand = this.commandsFactory.GetCommand(command.Name);
                        var report = engineCommand.ReadSingleCommand(command.Parameters);
                        this.printReports.Reports.Add(report);
                    }
                }
                catch (Exception ex)
                {
                    this.printReports.Reports.Add(ex.Message);
                    Start(false);
                }
            }
        }

    }
}
