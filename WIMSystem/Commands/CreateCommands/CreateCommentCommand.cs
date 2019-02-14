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
    class CreateCommentCommand : IEngineCommand
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

        public string ReadSingleCommand(IList<string> parameters)
        {
            var teamName = this.getter.GetTeam(parameters[0]);
            var boardName = this.getter.GetBoard(teamName.TeamName, parameters[1]);
            var workItem = this.getter.GetWorkItem(boardName, parameters[2]);
            var comment = parameters[3];
            var author = this.getter.GetMember(teamName, parameters[4]);

            return this.Execute(workItem, comment, author);
        }

        private string Execute(IWorkItem workitem, string message, IPerson author)
        {
            var comment = this.componentsFactory.CreateComment(message, author);
            workitem.AddComment(comment);

            string output = string.Format(Consts.CommentAdded, comment.Message, comment.Author.PersonName, workitem.Title);

            this.historyEventWriter.AddHistoryEvent(output, author, workitem.Board, workitem.Board.Team, workitem);

            return output;
        }
    }
}
