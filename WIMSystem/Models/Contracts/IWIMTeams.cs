using System.Collections.Generic;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public interface IWIMTeams
    {
        T this[string index] { get; }

        IDictionary<string, T> TeamsList { get; }

        void AddTeam(T newTeam);
        IEnumerator<T> GetEnumerator();
        void RemoveTeam(T removeTeam);
        void RemoveTeam(string teamName);
    }
}