using Autofac;
using WIMSystem.Commands.AddCommands;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.CreateCommands;
using WIMSystem.Core.Factories;
using WIMSystem.Core.Factories.Contracts;

namespace WIMSystem.ContainerModules
{
    public class CommandsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandsFactory>().As<ICommandsFactory>();

            builder.RegisterType<AddBoardToTeamCommand>().Named<IEngineCommand>("AddBoardToTeam");
            builder.RegisterType<AddPersonToTeamCommand>().Named<IEngineCommand>("AddPersonToTeam");
            builder.RegisterType<CreatePersonCommand>().Named<IEngineCommand>("CreatePerson");


        }
    }
}
