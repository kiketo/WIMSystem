using System;
using System.Collections.Generic;
using System.Text;
using Utils;
using WIMSystem.Commands.Contracts;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Commands.ListCommands
{
    public class ShowAllTeamBoardsCommand : IEngineCommand
    {
        private IGetters getters;
        private readonly IHistoryItemsCollection historyItemsCollection;

        public ShowAllTeamBoardsCommand(IGetters getters, IHistoryItemsCollection historyItemsCollection)
        {
            this.getters = getters ?? throw new ArgumentNullException(nameof(getters));
            this.historyItemsCollection = historyItemsCollection ?? throw new ArgumentNullException(nameof(historyItemsCollection));
        }

        public string ReadSingleCommand(IList<string> parameters)
        {
            var team = this.getters.GetTeam(parameters[0]);
            return this.Execute(team);
        }

        private string Execute(ITeam team)
        {
            if (Validators.IsNullValue(team))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
                    nameof(team)
                    ));
            }
            var returnMessage = team.ShowAllTeamBoards();
            return returnMessage;
        }
    }
}
