using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models.Contracts
{
    public interface IAssignableWorkItem : IWorkItem
    {
        IMember Assignee { get; }

        PriorityType Priority { get; }

        void AssignMember(IMember member);

        void UnassignMember();
    }
}
