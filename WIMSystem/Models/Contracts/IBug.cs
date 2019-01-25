using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models.Contracts
{
    public interface IBug
    {
        IList<string> stepsToReproduce { get; set; }
        PriorityType Priority { get; set; }
        SeverityType Severity { get; set; }
        BugStatusType bugStatus { get; set; }
        Member Assignee { get; set; }
    }

}
