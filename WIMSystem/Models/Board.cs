using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class Board : IBoard
    {
        private string boardName;
        private IList<IWorkItem> boardWorkItems;

        public Board(string boardName, IList<IWorkItem> boardWorkItems)
        {
            this.BoardName = boardName;
            this.BoardWorkItems = boardWorkItems;
        }

        public string BoardName
        {
            get
            {
                return this.boardName;
            }
            set
            {
                if (value.Length<5||value.Length>10)
                {
                    throw new ArgumentOutOfRangeException("Board name should be between 5 and 10 symbols.");
                }

                //Name should be unique in the team
                this.boardName = value;
            }
        }

        public IList<IWorkItem> BoardWorkItems
        {
            get
            {
                IList<IWorkItem> workItems = this.boardWorkItems;
                return workItems;
            }
            set
            {
                this.boardWorkItems = value;
            }
        }
    }
}
