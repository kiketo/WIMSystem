using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class Board : IBoard
    {
        private string boardName;
        private readonly IDictionary<string,IWorkItem> boardWorkItems;
        private ITeam team;

        public Board(string boardName, ITeam team)
        {
            this.BoardName = boardName;
            boardWorkItems = new Dictionary<string, IWorkItem>();
            this.Team = team;
        }

        public string BoardName
        {
            get
            {
                return this.boardName;
            }

            private set
            {
                if (value.Length<5||value.Length>10)
                {
                    throw new ArgumentOutOfRangeException("Board name should be between 5 and 10 symbols.");
                }

                //Board name should be unique in the team
                if (team.BoardList.ContainsKey(value))
                {
                    Console.WriteLine("Board with the same name already exists in the current Team.");
                }

                else
                {
                    this.boardName = value;
                }
            }
        }

        public IDictionary<string,IWorkItem> BoardWorkItems
        {
            get
            {
                return new Dictionary<string, IWorkItem>(this.boardWorkItems);
            }
        }

        public ITeam Team
        {
            get
            {
                return this.team;
            }

            private set
            {
                this.team = value ?? throw new ArgumentNullException("team", "Team cannot be null!");
            }
        }

        public void AddWorkItemToBoard(IWorkItem workItem)
        {
            throw new NotImplementedException();
        }

    }
}
