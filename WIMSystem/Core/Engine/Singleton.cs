using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;


namespace WIMSystem.Core.Engine
{
    public class WIMTeams : IWIMTeams, IEnumerable<ITeam>
    {
        // The single instance 
        private static List<Team> teamList;

        // Initialize the single instance 
        static WIMTeams()
        {
            teamList = new List<Team>();
        }

        // Private constructor – protects against direct instantiation 
        private WIMTeams() { }

        // The property for retrieving the single instance 
        public List<Team> TeamList
        {
            get {
                
                return teamList; }
            private set { this.teamList = value; }
        }


        public void AddTeam(Team newTeam)
        {
          this.TeamList.
        }

        //Надолу дали е ОК??? и какво става с другия WINTeams.cs ?


       // public IList<ITeam> TeamsList
       // {
       //     get => new List<ITeam>(teamList);
       // }

        public IEnumerator<ITeam> GetEnumerator()
        {
            foreach (var item in this.teamList)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
