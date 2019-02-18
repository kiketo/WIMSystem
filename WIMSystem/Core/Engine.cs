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

        public void Start()
        {
            this.mainMenu.ShowLogo();
            //var inputType = StringToEnum<InputType>.Convert(this.mainMenu.ShowMenu(MainMenuItems.InputTypeItems));
            //switch (inputType)
            //{
            //    case InputType.MenuCommands:
            //        this.mainMenu.Run(menuReader);
            //        break;
            //    case InputType.BatchCommands:
            //        this.mainMenu.Run(consoleReader);
            //        break;
            //    default:
            //        break;
            //}
            this.Run(this.reader);
            this.mainMenu.ShowCredits();
        }

        //public virtual void Run()
        //{
        //    string input = Console.ReadLine();
        //    while (input != "end")
        //    {
        //        try
        //        {
        //            var lineParameters = input.Trim().Split(
        //                new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        //            var commandName = lineParameters[0];
        //            var parameters = lineParameters.Skip(1);

        //            var command = this.parser.ParseCommand(commandName);
        //            var output = command.Execute(parameters.ToList());

        //            this.outputWriter.WriteLine(output);
        //            this.outputWriter.WriteLine(Delimiter);

        //            input = Console.ReadLine();
        //        }
        //        catch (Exception ex)
        //        {
        //            while (ex.InnerException != null)
        //            {
        //                ex = ex.InnerException;
        //            }

        //            this.outputWriter.WriteLine($"ERROR: {ex.Message}");
        //        }
        //    }
        //}

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
                            this.printReports.Reports.Clear();
                            break;
                        }

                        var command = this.parser.Parse(inputString);
                        var engineCommand = this.commandsFactory.GetCommand(command.Name);
                        var report = engineCommand.ReadSingleCommand(command.Parameters);
                        this.printReports.Reports.Add(report);
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    this.printReports.Reports.Add("Invalid..."); //TODO
                }
                catch (Exception ex)
                {
                    this.printReports.Reports.Add(ex.Message);
                }
            }
        }

    }
}
