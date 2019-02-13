using System;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Commands.AddCommands
{
    public class AddBoardToTeamCommand : IEngineCommand
    {
        private readonly IHistoryEventWriter historyEventWriter;

        public AddBoardToTeamCommand(IHistoryEventWriter historyEventWriter)
        {
            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(nameof(historyEventWriter));
        }

        public string ReadSingleCommand()
        {
            throw new NotImplementedException();
        }

        private string Execute(ITeam teamToAddTo, IBoard boardForAdding)
        {
            teamToAddTo.AddBoardToTeam(boardForAdding);
            string output = string.Format(Consts.ObjectAddedToTeam, nameof(Board), boardForAdding.BoardName, teamToAddTo.TeamName);
            this.historyEventWriter.AddHistoryEvent(output, board: boardForAdding, team: teamToAddTo);

            return output;
        }
    }
}
