using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface IWorkItem
    {
        int ID { get; }
        string Title { get; set; }
        string Description { get; set; }
        IBoard Board { get; }
        IList<Comment> ListOfComments { get; }
        IList<HistoryItem> ListOfHistoryItems { get; }

        void AddComment(Comment comment);

        void AddHistoryItem(HistoryItem history);
        
    }
}
