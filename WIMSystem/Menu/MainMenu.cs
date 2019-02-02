using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIMSystem.Core.Contracts;
using WIMSystem.Menu.Contracts;

namespace WIMSystem.Menu
{
    public class MainMenu //: IMainMenu
    {
        private readonly IList<MenuItem> mainMenuItems;
        private readonly string logo;
        private ICommandParser commandParser;
        private IWIMEngine engine;

        public MainMenu(IWIMEngine engine, ICommandParser commandParser, IList<MenuItem> mainMenuItems, string logo)
        {
            this.engine = engine;
            this.commandParser = commandParser;
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

            Console.WriteLine(this.logo);
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
                        // Console.Write("-->");
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

            } while (key.KeyChar != 13);

            if (curItem != menuItems.Length - 1)
            {

                this.ConsoleParameters(curItem);
            }
            Console.WriteLine();
        }


        public void ConsoleParameters(int indexOfItem)
        {
            Console.Clear();

            Console.WriteLine(this.mainMenuItems[indexOfItem].ParamsText);

            var parameters = Console.ReadLine();

            //var key = Console.ReadKey();

           // if (key.Key == ConsoleKey.Enter)
            {
                this.commandParser.SaveCommand(string.Concat(
                    this.mainMenuItems[indexOfItem].CommandText,
                    " ",
                    parameters));
                //Console.WriteLine(string.Concat(
                //    this.mainMenuItems[indexOfItem].CommandText,
                //    " ",
                //    parameters));
                this.engine.ExecuteCommands(this.commandParser);
            }
            Console.WriteLine();
            Console.Write("Press any key for main menu...");
            Console.ReadKey();
            this.ShowMenu();

        }

    }
}
