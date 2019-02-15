using System;
using System.Collections.Generic;
using Utils;
using WIMSystem.Commands.Contracts;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Commands.ListCommands
{
    public class ShowBoardActivityCommand : IEngineCommand
    {
        private IGetters getters;
        private readonly IHistoryItemsCollection historyItemsCollection;

        public ShowBoardActivityCommand(IGetters getters, IHistoryItemsCollection historyItemsCollection)
        {
            this.getters = getters;
            this.historyItemsCollection = historyItemsCollection;
        }


        public string ReadSingleCommand(IList<string> parameters)
        {
            var teamName = parameters[0];
            var boardName = parameters[1];
            var board = this.getters.GetBoard(teamName, boardName);
            return this.Execute(board);
        }

        private string Execute(IBoard board)
        {
            if (Validators.IsNullValue(board))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
                    nameof(board)
                    ));
            }
            var returnMessage = this.historyItemsCollection.ShowBoardActivity(board);
            return returnMessage;
        }
    }
}
