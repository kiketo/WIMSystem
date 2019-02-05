using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    internal class HistoryItemsCollection : IEnumerable, IHistoryItemsCollection
    {
        private readonly ICollection<IHistoryItem> historyItemsList;

        private readonly static IHistoryItemsCollection instance;

        static HistoryItemsCollection()
        {
            instance = new HistoryItemsCollection();
        }

        private HistoryItemsCollection()
        {
            this.historyItemsList = new List<IHistoryItem>();
        }

        public static IHistoryItemsCollection Instance
        {
            get { return instance; }
        }

        public ICollection<IHistoryItem> HistoryItemsList
        {
            get => new List<IHistoryItem>(this.historyItemsList);
        }

        public IEnumerator<IHistoryItem> GetEnumerator()
        {
            foreach (var item in this.historyItemsList)
            {
                yield return item;
            }
        }

        public void AddHistoryItem(IHistoryItem newHistoryItem)
        {
            this.historyItemsList.Add(newHistoryItem);
        }

        public bool Contains(IHistoryItem historyItem)
        {
            return this.historyItemsList.Contains(historyItem);

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void ShowBoardActivity(IBoard board)
        {
            var filteredActivity = Instance.HistoryItemsList.Where(team=>team.Team.TeamName==board.Team.TeamName);
            filteredActivity = filteredActivity.Where(x => x.Board.BoardName == board.BoardName);

            Console.WriteLine($"Activity history for board: {board.BoardName} in team: {board.Team.TeamName}");
            foreach (var item in filteredActivity)
            {
                Console.WriteLine(item.FilteredByBoardToString());
            }
            
        }

        public void ShowTeamActivity(ITeam team)
        {
            var filteredActivity = Instance.HistoryItemsList.Where(teamm => teamm.Team.TeamName == team.TeamName);
            

            Console.WriteLine($"Activity history for team: {team.TeamName}");
            foreach (var item in filteredActivity)
            {
                Console.WriteLine(item.FilteredByTeamToString());
            }

        }
    }
}
