using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models.Contracts
{
    public interface IFeedback
    {
        int Rating { get; set; }
        FeedbackStatusType feedbackStatus { get; set; }
    }
}
