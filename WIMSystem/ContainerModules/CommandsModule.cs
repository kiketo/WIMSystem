using Autofac;
using WIMSystem.Commands.AddCommands;
using WIMSystem.Commands.ChangeCommands;
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
            builder.RegisterType<CreateTeamCommand>().Named<IEngineCommand>("CreateTeam");
            builder.RegisterType<CreateBoardCommand>().Named<IEngineCommand>("CreateBoard");
            builder.RegisterType<CreateBugCommand>().Named<IEngineCommand>("CreateBug");
            builder.RegisterType<CreateStoryCommand>().Named<IEngineCommand>("CreateStory");
            builder.RegisterType<CreateFeedbackCommand>().Named<IEngineCommand>("CreateFeedback");
            builder.RegisterType<CreateCommentCommand>().Named<IEngineCommand>("CreateComment");
            builder.RegisterType<ChangePriorityCommand>().Named<IEngineCommand>("ChangePriority");
            builder.RegisterType<ChangeRatingOfFeedbackCommand>().Named<IEngineCommand>("ChangeRatingOfFeedback");
            builder.RegisterType<ChangeSeverityOfBugCommand>().Named<IEngineCommand>("ChangeSeverityOfBug");
            builder.RegisterType<ChangeSizeOfStoryCommand>().Named<IEngineCommand>("ChangeSizeOfStory");
            builder.RegisterType<ChangeStatus>().Named<IEngineCommand>("ChangeStatus");
            builder.RegisterType<AssignWorkItemToMemberCommand>().Named<IEngineCommand>("AssignWorkItemToMember");
            builder.RegisterType<UnassignWorkItemCommand>().Named<IEngineCommand>("UnassignWorkItem");

        }
    }
}
