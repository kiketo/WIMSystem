using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class Team : ITeam
    {
        #region Fields
        private string teamName;
        private IList<IMember> memberList;
        private IDictionary<string, IBoard> boardList;
        private IWIMTeams teamsList;

        #endregion
        #region Ctor
        public Team(string teamName, IWIMTeams teamsList)
        {
            this.TeamName = teamName;
            this.teamsList = teamsList;
            memberList = new List<IMember>();
        }
        #endregion
        #region Prop

        public string TeamName
        {
            get
            {
                return this.teamName;
            }

            private set
            {
                if (value.Length < 5 || value.Length > 15)
                {
                    throw new ArgumentOutOfRangeException("Teams name should be between 5 and 15 symbols.");
                }
                if (teamsList.Contains(value))
                {
                    Console.WriteLine("I Tova trqbva da se opravi");
                }
                else
                {
                    this.teamName = value;
                }
            }
        }

        public IList<IMember> MemberList
        {
            get
            {
                return this.memberList;
            }
        }

        public IDictionary<string, IBoard> BoardList
        {
            get
            {
                return new Dictionary<string,IBoard>(this.boardList);
            }

        }
        #endregion
        #region Methods
        public void AddMemberToTeam(IMember member)
        {
            if (member == null)
            {
                throw new ArgumentNullException("member", "Member cannot be null!");
            }

            if (memberList.Contains(member))
            {
                Console.WriteLine($"This team already has {member.MemberName}");
            }

            else
            {
                memberList.Add(member);
            }
        }

        public void AddBoard(IBoard board)
        {
            if (board == null)
            {
                throw new ArgumentNullException("board", "Board cannot be null!");
            }

            this.boardList.Add(board.BoardName,board);
        }

        #endregion
    }
}
