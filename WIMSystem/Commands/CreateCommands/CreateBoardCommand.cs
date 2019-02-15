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
    public class CreateBoardCommand : IEngineCommand
    {
        private readonly IHistoryEventWriter historyEventWriter;
        private readonly IWIMTeams wimTeams;
        private readonly IComponentsFactory componentsFactory;
        private readonly IGetters getter;

        public CreateBoardCommand(IHistoryEventWriter historyEventWriter, IWIMTeams wimTeams, IComponentsFactory componentsFactory, IGetters getter)
        {
            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(nameof(historyEventWriter));
            this.wimTeams = wimTeams ?? throw new ArgumentNullException(nameof(wimTeams));
            this.componentsFactory = componentsFactory ?? throw new ArgumentNullException(nameof(componentsFactory));
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
        }

        public string ReadSingleCommand(IList<string> parameters)
        {
            var boardName = parameters[0];
            var teamToAddTo = this.getter.GetTeam(parameters[1]);
            return this.Execute(boardName, teamToAddTo);
        }

        private string Execute(string boardName, ITeam teamToAddTo)
        {
            var board = this.componentsFactory.CreateBoard(boardName, teamToAddTo);

            if (board == null)
            {
                throw new ArgumentException(string.Format(ObjectConsts.ObjectExists, nameof(Board), board));
            }

            teamToAddTo.AddBoardToTeam(board);
            var returnMessage = string.Format(ObjectConsts.ObjectCreated, nameof(Board), boardName);
            this.historyEventWriter.AddHistoryEvent(returnMessage, board: board, team: teamToAddTo);

            return returnMessage;
        }
    }
}
