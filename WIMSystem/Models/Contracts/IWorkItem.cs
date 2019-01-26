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
        IList<Comment> ListOfComments { get; }

        void AddComment(Comment comment);

        void AddHistoryItem(HistoryItem history);
        
    }
}
