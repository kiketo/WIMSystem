using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models.Abstract
{
    public abstract class AssignableWorkItem : WorkItem, IAssignableWorkItem, IWorkItem
    {
        public AssignableWorkItem(string title, string description, PriorityType priority, IBoard board, IPerson assignee = null)
            :base (title,description,board)
        {
            this.Priority = priority;
            this.Assignee = assignee;
        }

        public PriorityType Priority { get; private set; }

        public IPerson Assignee { get; private set; }

        public void AssignMember (IPerson member)
        {
            if(member==null)
            {
                throw new ArgumentNullException("member", "Member cannot be null or empty!");
            }
            this.Assignee = member;
        }

        public void UnassignMember()
        {
            this.Assignee = null;
        }

        public void ChangePriority(string priority)
        {
            PriorityType priorityEnum = (PriorityType)Enum.Parse(typeof(PriorityType), priority, true);
            this.Priority = priorityEnum;
        }

    }
}
