using System.Collections.Generic;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models.Contracts
{
    internal interface IHistoryItemsCollection
    {
        ICollection<IHistoryItem> HistoryItemsList { get; }

        void AddHistoryItem(IHistoryItem newHistoryItem);
        bool Contains(IHistoryItem historyItem);
        IEnumerator<IHistoryItem> GetEnumerator();
        void ShowTeamActivity(ITeam team);
        void ShowBoardActivity(IBoard board);
    }
}