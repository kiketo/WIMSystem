﻿using System;
using System.Collections.Generic;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Commands.AddCommands
{
    public class AddBoardToTeamCommand : IEngineCommand
    {
        private readonly IHistoryEventWriter historyEventWriter;
        private readonly IGetters getters;

        public AddBoardToTeamCommand(IHistoryEventWriter historyEventWriter, IGetters getters)
        {
            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(nameof(historyEventWriter));
            this.getters = getters;
        }

        public string ReadSingleCommand(IList<string> parameters)
        {
            throw new NotImplementedException();
        }

        private string Execute(ITeam teamToAddTo, IBoard boardForAdding)
        {
            teamToAddTo.AddBoardToTeam(boardForAdding);
            string output = string.Format(ObjectConsts.ObjectAddedToTeam, nameof(Board), boardForAdding.BoardName, teamToAddTo.TeamName);
            this.historyEventWriter.AddHistoryEvent(output, board: boardForAdding, team: teamToAddTo);

            return output;
        }
    }
}
