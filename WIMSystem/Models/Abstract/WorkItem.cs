using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Utils;

namespace WIMSystem.Models.Abstract
{
    public abstract class WorkItem : IWorkItem
    {
        private readonly int id;
        private string title;
        private string description;
        private IBoard board;
        private IList<IComment> listOfComments;
        private IList<IHistoryItem> listOfHistoryItems;

        public WorkItem(string title, string description, IBoard board)
        {
            this.Board = board;
            this.id = IDProvider.GenerateUniqueID();
            this.Title = title;
            this.Description = description;
            listOfComments = new List<IComment>();
            listOfHistoryItems = new List<IHistoryItem>();
        }

        public int ID
        {
            get
            {
                return this.id;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Title", "Title cannot be empty!");
                }

                if (value.Length < 10 || value.Length > 50)
                {
                    throw new ArgumentOutOfRangeException("Title", "Title should be more than 10 and lesser than 50 characters long!");
                }
                if (board.BoardWorkItems.ContainsKey(value)) //
                {
                    Console.WriteLine($"This board already contains Work Item with name {value}!");
                }
                else
                {
                    this.title = value;
                }
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Description", "Description cannot be empty!");
                }

                if (value.Length < 10 || value.Length > 500)
                {
                    throw new ArgumentOutOfRangeException("Description", "Description should be more than 10 and lesser than 500 characters long!");
                }

                this.description = value;
            }
        }

        public IList<IComment> ListOfComments { get; }

        public IList<IHistoryItem> ListOfHistoryItems { get; }

        public IBoard Board
        {
            get
            {
                return this.board;
            }
            private set
            {
                this.board = value ?? throw new ArgumentNullException("board", "Board cannot be null!");
            }
        }

        public void AddComment(IComment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("Comment", "Comment cannot be empty!");
            }

            listOfComments.Add(comment);
        }

        public void AddHistoryItem(IHistoryItem history)
        {
            if (history == null)
            {
                throw new ArgumentNullException("History");
            }

            listOfHistoryItems.Add(history);
        }

        public abstract void ChangeStatus(string newStatus);

        public abstract Enum GetStatus();

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine($"Team: {this.Board.Team.TeamName}");
            str.AppendLine($"Board: {this.Board.BoardName}");
            str.AppendLine($"Work Item ID: {this.ID}");
            str.AppendLine($"Title: {this.Title}");
            str.AppendLine($"Description: {this.Description}");
            if (this.ListOfComments.Count > 0)
            {
                str.AppendLine("Comments:");
                foreach (var comment in this.ListOfComments)
                {
                    str.AppendLine($"Author: {comment.Author}");
                    str.AppendLine($"Comment: {comment.Message}");
                }
            }

            return str.ToString();
        }
    }
}
