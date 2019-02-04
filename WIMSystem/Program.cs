using System;
using WIMSystem.Core;
using WIMSystem.Menu;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem
{
    class Program
    {
        static void Main()
        {

            var factory = new Factory();
            var teamList = WIMTeams.Instance;
            var memberLits = PersonsCollection.Instance;
            var commandParser = new MenuCommandParser();
            var engine = new WIMEngine(factory,teamList,memberLits);

            var mainMenu = new MainMenu(
                engine,
                commandParser,
                MainMenuItems.mainMenuItems,
                MainMenuLogo.logo);
            mainMenu.Start();

            //engine.ExecuteCommands(commandParser);

        }
    }
}
