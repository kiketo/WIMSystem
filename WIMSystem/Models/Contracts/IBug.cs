using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models.Contracts
{
    public interface IBug : IAssignableWorkItem, IWorkItem
    {
        IList<string> StepsToReproduce { get; }
        BugSeverityType Severity { get; }
        BugStatusType BugStatus { get; }

        void ChangeSeverity(string severity);
        void ChangeStatus(string status);
    }
}
