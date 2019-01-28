﻿using System;
using System.Collections;
using System.Collections.Generic;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class WIMTeams : IEnumerable<T>, IWIMTeams
    {
        private IDictionary<string,T> teamsList;

        public IDictionary<string,T> TeamsList
        {
            get => new Dictionary<string,T>(teamsList);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in this.teamsList)
            {
                yield return item.Value;
            }
        }

        public void AddTeam(T newTeam)
        {
            this.teamsList.Add(newTeam.TeamName, newTeam);
        }

        public T this[string index]
        {
            get => this.teamsList[index];
            private set
            {
                this.teamsList[index] = value;
            }
        }

        public void RemoveTeam(T removeTeam)
        {
            RemoveTeam(removeTeam.TeamName);
        }


        public void RemoveTeam(string teamName)
        {
            if (teamsList.ContainsKey(teamName))
            {
                throw new ArgumentOutOfRangeException("There is not such team");
            }
            this.teamsList.Remove(teamName);
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
