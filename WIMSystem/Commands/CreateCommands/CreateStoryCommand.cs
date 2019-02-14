using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Core.Utils;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Commands.CreateCommands
{
    class CreateStoryCommand : IEngineCommand
    {
        private readonly IHistoryEventWriter historyEventWriter;
        private readonly IComponentsFactory componentsFactory;
        private readonly IGetters getter;

        public CreateStoryCommand(IHistoryEventWriter historyEventWriter, IComponentsFactory componentsFactory, IGetters getter)
        {
            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(nameof(historyEventWriter));
            this.componentsFactory = componentsFactory ?? throw new ArgumentNullException(nameof(componentsFactory));
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
        }

        public string ReadSingleCommand(IList<string> parameters)
        {
            var storyTitle = parameters[0];
            var storyDescription = parameters[1];
            var storyPriority = StringToEnum<PriorityType>.Convert(parameters[2]);
            var storySize = StringToEnum<StorySizeType>.Convert(parameters[3]);
            var teamName = parameters[4];
            var board = this.getter.GetBoard(teamName, parameters[5]);

            return this.Execute(storyTitle, storyDescription, storyPriority, storySize, board);
        }

        private string Execute(string storyTitle, string storyDescription, PriorityType storyPriority, StorySizeType storySize, IBoard board, IPerson storyAssignee = null)
        {
            var story = this.componentsFactory.CreateStory(storyTitle, storyDescription, storyPriority, storySize, board, storyAssignee);
            if (story == null)
            {
                throw new ArgumentException(string.Format(Consts.ObjectExists, nameof(Story), story.Title));
            }

            board.AddWorkItemToBoard(story);

            string output = string.Format(Consts.ObjectCreated, nameof(Story), story.Title);

            this.historyEventWriter.AddHistoryEvent(output, storyAssignee, board, board.Team);

            return output;
        }


    }
}
