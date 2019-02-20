using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Commands.CreateCommands
{
    public class CreateCommentCommand : IEngineCommand
    {
        private readonly IHistoryEventWriter historyEventWriter;
        private readonly IComponentsFactory componentsFactory;
        private readonly IGetters getter;

        public CreateCommentCommand(IHistoryEventWriter historyEventWriter, IComponentsFactory componentsFactory, IGetters getter)
        {
            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(nameof(historyEventWriter));
            this.componentsFactory = componentsFactory ?? throw new ArgumentNullException(nameof(componentsFactory));
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
        }

        public string Execute(IList<string> parameters)
        {
            var teamName = this.getter.GetTeam(parameters[0]);
            var boardName = this.getter.GetBoard(teamName.TeamName, parameters[1]);
            var workItem = this.getter.GetWorkItem(boardName, parameters[2]);
            var message = parameters[3];
            var author = this.getter.GetMember(teamName, parameters[4]);

            var comment = this.componentsFactory.CreateComment(message, author);
            workItem.AddComment(comment);

            string returnMessage = string.Format(CommandsConsts.CommentAdded, comment.Message, comment.Author.PersonName, workItem.Title);

            this.historyEventWriter.AddHistoryEvent(returnMessage, author, workItem.Board, workItem.Board.Team, workItem);

            return returnMessage;
        }
    }
}
