using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class HistoryItemsCollection : IEnumerable, IHistoryItemsCollection
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

        public string ShowBoardActivity(IBoard board)
        {
            StringBuilder str = new StringBuilder();

            var filteredActivity = this.HistoryItemsList.Where(x => x.Team.TeamName == board.Team.TeamName);

            filteredActivity = filteredActivity.Where(x => x.Board.BoardName == board.BoardName);

            str.AppendLine($"Activity history for board: {board.BoardName} in team: {board.Team.TeamName}");
            if (filteredActivity.Count() == 0)
            {
                str.AppendLine("There is no history!");
            }
            else
            {
                foreach (var item in filteredActivity)
                {
                    str.AppendLine(item.FilteredByBoardToString());
                }

            }

            return str.ToString();
        }

        public string ShowTeamActivity(ITeam team)
        {
            StringBuilder str = new StringBuilder();
            var filteredActivity = this.HistoryItemsList.Where(x => x.Team.TeamName == team.TeamName);


            str.AppendLine($"Activity history for team: {team.TeamName}");
            if (filteredActivity.Count() == 0)
            {
                str.AppendLine("There is no history!");
            }
            else
            {
                foreach (var item in filteredActivity)
                {
                    str.AppendLine(item.FilteredByTeamToString());
                }
            }

            return str.ToString();
        }

        public string ShowPersonActivity(IPerson person)
        {
            var str = new StringBuilder();

            var filteredActivity = this.HistoryItemsList.Where(x => x.Member.PersonName == person.PersonName);


            str.AppendLine($"Activity history for person: {person.PersonName}");
            if (filteredActivity.Count() == 0)
            {
                str.AppendLine("There is no history!");
            }
            else
            {
                foreach (var activity in filteredActivity)
                {
                    str.AppendLine(activity.FilteredByPersonToString());
                }
            }

            return str.ToString();
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            if (this.HistoryItemsList.Count == 0)
            {
                str.AppendLine("There are no history items!");
            }
            else
            {

            }
            return str.ToString();
        }
    }
}
