using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models
{
    public class Feedback : WorkItem, IFeedback
    {
        public Feedback(string title, string description, int rating, IBoard board) 
            :base (title,description,board)
        {
            this.Rating = rating;
            this.FeedbackStatus = FeedbackStatusType.Unscheduled;
        }

        public int Rating { get; set; } //what is rating and how do we check it?

        public FeedbackStatusType FeedbackStatus { get; set; }
    }
}
