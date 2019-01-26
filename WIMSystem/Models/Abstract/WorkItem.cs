using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Utils;

namespace WIMSystem.Models.Abstract
{
    public abstract class WorkItem : IWorkItem
    {
        private int id;
        private string title;
        private string description;
        private IList<Comment> listOfComments;
        private IList<HistoryItem> listOfHistoryItems;

        public WorkItem(string title, string description)
        {
            this.id = IDProvider.GenerateUniqueID();
            this.Title = title;
            this.Description = description;
            listOfComments = new List<Comment>();
            listOfHistoryItems = new List<HistoryItem>();
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

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Title", "Title cannot be empty!");
                }

                if (value.Length < 10 || value.Length > 50)
                {
                    throw new ArgumentOutOfRangeException("Title", "Title should be more than 10 and lesser than 50 characters long!");
                }

                this.title = value;
            }
        }


        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                if(string.IsNullOrEmpty(value))
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

        public IList<Comment> ListOfComments
        {
            get
            {
                return this.listOfComments;
            }
        }

        public void AddComment(Comment comment)
        {
            if(comment==null)
            {
                throw new ArgumentNullException("Comment", "Comment cannot be empty!");
            }

            listOfComments.Add(comment);
        }

        public void AddHistoryItem(HistoryItem history)
        {
            if(history == null)
            {
                throw new ArgumentNullException("History");
            }

            listOfHistoryItems.Add(history);
        }
    }
}
