using System.Collections.Generic;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public interface IWIMTeams
    {
        ITeam this[string index] { get; }

        IDictionary<string, ITeam> TeamsList { get; }

        void AddTeam(ITeam newTeam);
        IEnumerator<ITeam> GetEnumerator();
        void RemoveTeam(ITeam removeTeam);
        void RemoveTeam(string teamName);
        bool Contains(string teamName);
        void ShowAllTeams();
    }
}