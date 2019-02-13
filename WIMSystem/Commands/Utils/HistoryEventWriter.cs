using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Core.Contracts;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Commands.Utils
{
    public class HistoryEventWriter : IHistoryEventWriter
    {
        private readonly IHistoryItemsCollection historyItemsList;
        private readonly IFactory factory;

        public HistoryEventWriter(IHistoryItemsCollection historyItemsList, IFactory factory)
        {
            this.historyItemsList = historyItemsList ?? throw new ArgumentNullException(nameof(historyItemsList));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        //SingleInstance();

        public void AddHistoryEvent(string description, IPerson member = null, IBoard board = null, ITeam team = null, IWorkItem workItem = null)
        {
            var historyItem = this.factory.CreateHistoryItem(description, member, board, team, workItem);
            this.historyItemsList.AddHistoryItem(historyItem);
        }
    }
}
