using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface IPerson
    {
        string MemberName { get; }
        IList<IWorkItem> MemberWorkItems { get; }
    }
}
