using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models
{
    public class Feedback : WorkItem, IFeedback, IWorkItem
    {
        public Feedback(string title, string description, int rating, IBoard board) 
            :base (title,description,board)
        {
            this.Rating = rating;
            this.FeedbackStatus = FeedbackStatusType.Unscheduled;
        }

        public int Rating { get; private set; } //what is rating and how do we check it?

        public FeedbackStatusType FeedbackStatus { get; private set; }

        public void ChangeRating(int rating)
        {
            if(rating<0)
            {
                throw new ArgumentOutOfRangeException("rating", "Rating cannot be negative!");
            }

            else
            {
                this.Rating = rating;
            }
        }

        public void ChangeStatus(string status)
        {
            if (status == null)
            {
                throw new ArgumentNullException("status", "Status cannot be null or empty!");
            }

            else
            {
                FeedbackStatusType statusEnum = (FeedbackStatusType)Enum.Parse(typeof(FeedbackStatusType), status, true);
                this.FeedbackStatus = statusEnum;
            }
        }
    }
}
