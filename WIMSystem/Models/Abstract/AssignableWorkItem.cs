using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models.Abstract
{
    public abstract class AssignableWorkItem : WorkItem
    {
        IMember assignee;

        public AssignableWorkItem(string title, string description, IBoard board, IMember assignee = null)
            :base (title,description,board)
        {
            this.Assignee = assignee;
        }

        public IMember Assignee { get; private set; }

        public void AssignMember (IMember member)
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

    }
}
