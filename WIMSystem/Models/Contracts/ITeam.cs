using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface ITeam
    {
        string TeamName { get;  }
        IList<IMember> MemberList { get; }
        IDictionary<string,IBoard> BoardList { get; }
    }
}
