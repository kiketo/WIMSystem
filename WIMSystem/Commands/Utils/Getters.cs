using System;
using WIMSystem.Commands.Contracts;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Commands.Utils
{
    public class Getters : IGetters
    {
        private readonly IPersonsCollection personList;
        private readonly IWIMTeams wimTeams;

        public Getters(IPersonsCollection personList, IWIMTeams wimTeams)
        {
            this.personList = personList;
            this.wimTeams = wimTeams;
        }

        public IPerson GetPerson(string memberAsString)
        {
            var member = this.personList[memberAsString];
            return member;

        }

        public IPerson GetMember(ITeam team, string memberAsString)
        {
            if (!this.wimTeams.TeamsList.ContainsKey(team.TeamName))
            {
                throw new ArgumentException($"No {team.TeamName} team found!");
            }

            //var person = this.wimTeams.TeamsList
            //            .Where(x => x.Value == teamName)
            //            .Select(team => team.Value)
            //            .SelectMany(team => team.MemberList)
            //            .FirstOrDefault(member => member.PersonName == memberAsString);

            var person = this.personList[memberAsString];

            if (!team.MemberList.Contains(person))
            {
                throw new ArgumentNullException("person", $"There is no person with name {memberAsString} in the team.");
            }

            return person;
        }

        public ITeam GetTeam(string teamAsString)
        {
            var team = this.wimTeams[teamAsString];
            return team;
        }

        public IBoard GetBoard(string teamName, string boardAsString)
        {
            //var teamResult = this.wimTeams.TeamsList
            //                .Select(team => team.Value)
            //                .Where(team => team.BoardList.Keys.Any(board => board == boardAsString))
            //                .Single();
            var boardResult = this.wimTeams[teamName].BoardList[boardAsString];
            return boardResult;

        }

        public IWorkItem GetWorkItem(IBoard board, string workItemAsString)
        {
            return board.BoardWorkItems[workItemAsString];
        }
    }
}
