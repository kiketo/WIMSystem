using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface IMember
    {
        string MemberName { get; set; }
        IList<IWorkItem> MemberWorkItems { get; }
    }
}
