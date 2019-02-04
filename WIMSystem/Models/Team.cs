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
        private IList<IPerson> memberList;
        private IDictionary<string, IBoard> boardList;
        private IWIMTeams teamsList;

        #endregion
        #region Ctor
        public Team(string teamName, IWIMTeams teamsList)
        {
            this.TeamName = teamName;
            this.teamsList = teamsList;
            this.memberList = new List<IPerson>();
            this.boardList = new Dictionary<string, IBoard>();
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
                if (value.Length < 3 || value.Length > 25)
                {
                    throw new ArgumentOutOfRangeException("Teams name should be between 3 and 25 symbols.");
                }
                //if (this.teamsList.Contains(value))
                //{
                //    Console.WriteLine("I Tova trqbva da se opravi");
                //}
                //else
                //{
                //}
                this.teamName = value;
            }
        }

        public IList<IPerson> MemberList
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
                return new Dictionary<string, IBoard>(this.boardList);
            }

        }
        #endregion
        #region Methods
        public void AddMemberToTeam(IPerson member)
        {
            if (member == null)
            {
                throw new ArgumentException("Member cannot be null!");
            }

            if (this.memberList.Contains(member))
            {
                throw new ArgumentException($"This team already has {member.MemberName}");
            }

            else
            {
                this.memberList.Add(member);
                //return $"{member.MemberName} is added to {this.TeamName} member list";
            }
        }

        public void RemoveMemberFromTeam(IPerson member)
        {
            if (member == null)
            {
                throw new ArgumentException($"Member can not be null.");

            }

            if (!this.memberList.Contains(member))
            {
                throw new ArgumentException($"This team does not has {member.MemberName} member.");
            }

            else
            {
                this.memberList.Remove(member);
                //return $"{member.MemberName} is removed from {this.TeamName} member list";
            }
        }

        public void AddBoardToTeam(IBoard board)
        {
            if (board == null)
            {
                throw new ArgumentException("Board cannot be null!");
            }
            if (this.boardList.ContainsKey(board.BoardName))
            {
                throw new ArgumentException($"This team already has {board.BoardName} board");
            }

            else
            {
                this.boardList.Add(board.BoardName, board);

                //$"{board.BoardName} is added to {this.TeamName} board list";
            }
        }

        public void RemoveBoardFromTeam(IBoard board)
        {
            if (board == null)
            {
                throw new ArgumentException("Board cannot be null!");
            }
            if (!this.boardList.ContainsKey(board.BoardName))
            {
                throw new ArgumentException($"This team has  {board.BoardName} board");
            }

            else
            {
                this.boardList.Remove(board.BoardName);
                //return $"{board.BoardName} is added to {this.TeamName} board list";
            }
        }

        #endregion
    }
}
