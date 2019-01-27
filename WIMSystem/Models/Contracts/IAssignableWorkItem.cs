using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface IAssignableWorkItem : IWorkItem
    {
        IMember Assignee { get; }
    }
}
