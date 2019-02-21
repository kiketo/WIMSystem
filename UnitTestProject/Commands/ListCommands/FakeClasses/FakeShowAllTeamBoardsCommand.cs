using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.ListCommands;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Tests.CommandsTest.ListCommandsTest.FakeClasses
{
    public class FakeShowAllTeamBoardsCommand : ShowAllTeamBoardsCommand
    {
        public FakeShowAllTeamBoardsCommand(IGetters getters, IHistoryItemsCollection historyItemsCollection)
            : base(getters, historyItemsCollection)
        {

        }

        public IGetters Getters
        {
            get
            {
                return base.getters;
            }
        }

        public IHistoryItemsCollection HistoryItemsCollection
        {
            get
            {
                return base.historyItemsCollection;
            }
        }
    }
}

