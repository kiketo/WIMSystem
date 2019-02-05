using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class Team : ITeam
    {

        private string teamName;
        private readonly IList<IPerson> memberList;
        private readonly IDictionary<string, IBoard> boardList;


        public Team(string teamName)
        {
            this.TeamName = teamName;

            this.memberList = new List<IPerson>();
            this.boardList = new Dictionary<string, IBoard>();
        }

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

        public void AddMemberToTeam(IPerson member)
        {
            if (member == null)
            {
                throw new ArgumentException("Member cannot be null!");
            }

            if (this.memberList.Contains(member))
            {
                throw new ArgumentException($"This team already has {member.PersonName}");
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
                throw new ArgumentException($"This team does not has {member.PersonName} member.");
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

        public void ShowAllTeamMembers()
        {
            if (this.MemberList.Count == 0)
            {
                Console.WriteLine($"There are no members in team {this.TeamName}!");
            }
            else
            {
                Console.WriteLine($"Members in team {this.TeamName}:");
                foreach (var member in this.MemberList)
                {
                    Console.WriteLine(member.PersonName);
                }
            }
        }

        public void ShowAllTeamBoards()
        {
            if (this.BoardList.Count == 0)
            {
                Console.WriteLine($"There are no boards in team {this.TeamName}!");
            }
            else
            {
                Console.WriteLine($"Boards in team {this.TeamName}:");
                foreach (var board in this.BoardList)
                {
                    Console.WriteLine(board.Value.BoardName);
                }
            }
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();


            return
        }

    }
}
