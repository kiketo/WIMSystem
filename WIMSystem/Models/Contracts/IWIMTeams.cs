using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface IWIMTeams
    {
        IList<ITeam> TeamsList { get; }
    }
}
