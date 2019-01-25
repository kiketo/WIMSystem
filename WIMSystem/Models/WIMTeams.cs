using System.Collections;
using System.Collections.Generic;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class WIMTeams : IWIMTeams, IEnumerable<ITeam>
    {
        private IList<Team> teamList;

        public IList<ITeam> TeamsList
        {
            get => new List<ITeam>(teamList);
        }

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
