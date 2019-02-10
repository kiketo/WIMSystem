using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIMSystem.Core;
using WIMSystem.Core.Contracts;
using WIMSystem.Menu.Contracts;

namespace WIMSystem.Menu
{
    public class MainMenu //: IMainMenu
    {
        private readonly IList<MenuItem> mainMenuItems;
        private readonly string logo;
        //private readonly ICommandParser commandParser;
        private readonly ICommandParser batchParser;
        private readonly IWIMEngine engine;

        public MainMenu(IWIMEngine engine, ICommandParser batchParser, IList<MenuItem> mainMenuItems, string logo)
        {
            this.engine = engine;
            // this.commandParser = commandParser;
            this.batchParser = batchParser;
            this.mainMenuItems = mainMenuItems ?? throw new ArgumentException("Main menu items can not be null!");
            this.logo = logo ?? throw new ArgumentException("Logo can not be null!");
        }

        public void Start()
        {
            this.ShowLogo();
            this.ShowMenu();
            this.ShowCredits();

        }

        public void ShowLogo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(MainMenuLogo.logo);
            Console.WriteLine("Press any key for main menu...");
            Console.ResetColor();

            Console.ReadKey();

        }

        public void ShowCredits()
        {
            Console.Clear();
            Console.WriteLine(this.logo);
            Console.WriteLine("                 Thank you!");
        }

        public void ShowMenu()
        {
            int curItem = 0;
            int c = 0;

            ConsoleKeyInfo key;

            string[] menuItems = this.mainMenuItems.Select(x => x.MenuText).ToArray();
            do
            {
                Console.Clear();

                Console.WriteLine("Work Item Management (WIM) Console Application");
                Console.WriteLine("Select command:");
                Console.WriteLine();

                for (c = 0; c < menuItems.Length; c++)
                {
                    if (curItem == c)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(menuItems[c]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(menuItems[c]);
                    }
                }

                Console.Write("Select your choice with the arrow keys.");
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.DownArrow)
                {
                    curItem++;
                    if (curItem > menuItems.Length - 1) curItem = menuItems.Length - 1;
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    curItem--;
                    if (curItem < 0) curItem = 0;
                }

            } while (key.Key != ConsoleKey.Enter);

            if (curItem != menuItems.Length - 1)
            {
                if (curItem == menuItems.Length - 2)
                {

                    this.ConsoleBatchCommands();
                }
                else
                {

                    this.ConsoleParameters(curItem);
                }

            }
            Console.WriteLine();
        }

        public void ConsoleBatchCommands()
        {
            Console.Clear();
            this.engine.ExecuteCommands(this.batchParser);
            Console.WriteLine();
            Console.Write("Press any key for main menu...");
            Console.ReadKey();
            this.ShowMenu();
        }


        public void ConsoleParameters(int indexOfItem)
        {
            Console.Clear();
            ICommand command;
            if (!string.IsNullOrEmpty(this.mainMenuItems[indexOfItem].ParamsText))
            {
                Console.WriteLine(this.mainMenuItems[indexOfItem].ParamsText);

                var parameters = Console.ReadLine();

                command = Command.Parse(string.Concat(
                    this.mainMenuItems[indexOfItem].CommandText,
                    "\" ",
                    parameters));
            }
            else
            {
                command = Command.Parse(this.mainMenuItems[indexOfItem].CommandText);
                //this.commandParser.SaveCommand(this.mainMenuItems[indexOfItem].CommandText); //TODO
            }

            this.engine.ExecuteCommands(new List<ICommand>() { command });

            Console.WriteLine();
            Console.Write("Press any key for main menu...");
            Console.ReadKey();
            this.ShowMenu();

        }

    }
}
