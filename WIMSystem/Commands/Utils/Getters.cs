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
            this.personList = personList ?? throw new ArgumentNullException(
                                                                   string.Format(
                                                             Consts.NULL_OBJECT,
                                                                nameof(personList)));
            this.wimTeams = wimTeams ?? throw new ArgumentNullException(
                                                                string.Format(
                                                                Consts.NULL_OBJECT,
                                                                nameof(wimTeams)));
        }

        public IPerson GetPerson(string memberAsString)
        {
            if (!personList.Contains(memberAsString))
            {
                throw new ArgumentNullException(
                            string.Format(
                            Consts.NoPersonFound,
                            memberAsString));

            }
            var member = this.personList[memberAsString];
            return member;

        }

        public IPerson GetMember(ITeam team, string memberAsString)
        {
            if (!this.wimTeams.TeamsList.ContainsKey(team.TeamName))
            {
                throw new ArgumentException(
                            string.Format(
                            Consts.NoTeamFound,
                            team.TeamName));
            }

            var person = this.GetPerson(memberAsString);//this.personList[memberAsString];

            //if (!team.MemberList.Contains(person))
            //{
            //    throw new ArgumentNullException(
            //                string.Format(
            //                Consts.NoPersonInTeamFound,
            //                memberAsString));
                
            //}

            return person;
        }

        public ITeam GetTeam(string teamAsString)
        {
            if (!this.wimTeams.TeamsList.ContainsKey(teamAsString))
            {
                throw new ArgumentException(
                            string.Format(
                            Consts.NoTeamFound,
                            teamAsString));

            }
            var team = this.wimTeams[teamAsString];
            return team;
        }

        public IBoard GetBoard(string teamName, string boardAsString)
        {
            //var teamResult = this.wimTeams.TeamsList
            //                .Select(team => team.Value)
            //                .Where(team => team.BoardList.Keys.Any(board => board == boardAsString))
            //                .Single();
            if (!this.wimTeams.TeamsList.ContainsKey(teamName))
            {
                throw new ArgumentException(
                            string.Format(
                            Consts.NoTeamFound,
                            teamName));
            }

            if (!this.wimTeams.TeamsList[teamName].BoardList.ContainsKey(boardAsString))
            {
                throw new ArgumentException(
                            string.Format(
                            Consts.NoBoardFound,
                            boardAsString));
            }

            var boardResult = this.wimTeams[teamName].BoardList[boardAsString];
            return boardResult;

        }

        public IWorkItem GetWorkItem(IBoard board, string workItemAsString)
        {
            if (!board.BoardWorkItems.ContainsKey(workItemAsString))
            {
                throw new ArgumentException(
                            string.Format(
                            Consts.NoWorkItemFound,
                            workItemAsString));
            }
            return board.BoardWorkItems[workItemAsString];
        }

        public IAssignableWorkItem GetAssignableWorkItem(IBoard board, string assignableWorkItemTitle)
        {
            if (board.BoardWorkItems[assignableWorkItemTitle] is IAssignableWorkItem)
            {
                return (IAssignableWorkItem)board.BoardWorkItems[assignableWorkItemTitle];
            }

            throw new ArgumentException(string.Format($"{board.BoardWorkItems[assignableWorkItemTitle].GetType().Name} is not assignable work item!"));
        }
    }
}
