using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models.Contracts
{
    public interface IFeedback : IWorkItem
    {
        int Rating { get; set; }
        FeedbackStatusType FeedbackStatus { get; set; }
    }
}
