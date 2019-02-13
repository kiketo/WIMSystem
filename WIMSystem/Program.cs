using Autofac;
using System;
using WIMSystem.ContainerModules;
using WIMSystem.Core;
using WIMSystem.Core.Factories;
using WIMSystem.Menu;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem
{
    internal class Program
    {
        private static void Main()
        {
            //var factory = new Factory();

            //var teamList = WIMTeams.Instance;
            //var memberLits = PersonsCollection.Instance;
            //var historyItemsList = HistoryItemsCollection.Instance;

            //var batchParser = new ConsoleCommandParser();

            //var engine = new WIMEngine(factory,teamList,memberLits,historyItemsList);

            //var mainMenu = new MainMenu(
            //    engine,
            //    batchParser,
            //    MainMenuItems.mainMenuItems);

            //mainMenu.Start();

            var builder = new ContainerBuilder();

            builder.RegisterModule(new MainModule());
            builder.RegisterModule(new CommandsModule());


            var container = builder.Build();
            //builder.RegisterGeneric(typeof(ContainerResolver<>)).As(typeof(IContainerResolver<>)).WithParameter("container", container);

            using (var scope = container.BeginLifetimeScope())
            {
                var menu = scope.Resolve<MainMenu>();
                menu.Start();
            }
        }
    }
}
