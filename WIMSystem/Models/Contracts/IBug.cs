using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models.Contracts
{
    public interface IBug: IAssignableWorkItem
    {
        IList<string> StepsToReproduce { get;}
        PriorityType Priority { get; set; }
        BugSeverityType Severity { get; set; }
        BugStatusType BugStatus { get; set; }
    }
}
