using System;
using System.Collections;
using System.Collections.Generic;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class DictionaryContainer<T> : IEnumerable<T>, IWIMTeams
    {
        private IDictionary<string, T> containerList;

        public IDictionary<string, T> ContainerList
        {
            get => new Dictionary<string, T>(containerList);
        }

        public IEnumerator<Contracts.T> GetEnumerator()
        {
            foreach (var item in this.containerList)
            {
                yield return item.Value;
            }
        }

        public void AddTeam(Contracts.T newTeam)
        {
            this.containerList.Add(newTeam.TeamName, newTeam);
        }

        public T this[string index]
        {
            get => this.containerList[index];
            private set
            {
                this.containerList[index] = value;
            }
        }

        public void RemoveTeam(Contracts.T removeTeam)
        {
            RemoveTeam(removeTeam.TeamName);
        }


        public void RemoveTeam(string teamName)
        {
            if (containerList.ContainsKey(teamName))
            {
                throw new ArgumentOutOfRangeException("There is not such team");
            }
            this.containerList.Remove(teamName);
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
