using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models.Contracts
{
    public interface IStory
    {
        PriorityType Priority { get; set; }
        BugSeverityType Severity { get; set; }
        StoryStatusType storyStatus { get; set; }
        IMember Assignee { get; set; }
    }
}
