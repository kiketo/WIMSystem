using System;
using System.Collections;
using System.Collections.Generic;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class WIMTeams : IEnumerable<ITeam>, IWIMTeams
    {
        private IDictionary<string,ITeam> teamsList;

        private static IWIMTeams instance;

        static WIMTeams()
        {
            instance = new WIMTeams();
        }

        private WIMTeams()
        {
            this.teamsList = new Dictionary<string,ITeam>();
        }

        public static IWIMTeams Instance
        {
            get { return instance; }
        }

        public IDictionary<string,ITeam> TeamsList
        {
            get => new Dictionary<string,ITeam>(teamsList);
        }

        public IEnumerator<ITeam> GetEnumerator()
        {
            foreach (var item in this.teamsList)
            {
                yield return item.Value;
            }
        }

        public void AddTeam(ITeam newTeam)
        {
            this.teamsList.Add(newTeam.TeamName, newTeam);
        }

        public ITeam this[string index]
        {
            get => this.teamsList[index];
            private set
            {
                this.teamsList[index] = value;
            }
        }

        public void RemoveTeam(ITeam removeTeam)
        {
            RemoveTeam(removeTeam.TeamName);
        }


        public void RemoveTeam(string teamName)
        {
            if (teamsList.ContainsKey(teamName))
            {
                throw new ArgumentOutOfRangeException($"Team with {teamName} does not exists");
            }
            this.teamsList.Remove(teamName);
        }

        public bool Contains(string teamName)
        {
            return (teamsList.ContainsKey(teamName));
        
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
