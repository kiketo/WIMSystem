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
        private IDictionary<string,IBoard> boardList;
        #endregion
        #region Ctor
        public Team(string teamName)//, IList<IMember> memberList, IDictionary<string, IBoard> boardList)
        {
            this.TeamName = teamName;
            //this.MemberList = new List<IMember>();
            //this.MemberList = memberList;
            //this.BoardList = new Dictionary<string, IBoard>();
            //this.BoardList = boardList;
        }
        #endregion
        #region Prop

        public string TeamName
        {
            get
            {
                return this.teamName;
            }
            set
            {
                if (value.Length < 5 || value.Length > 15)
                {
                    throw new ArgumentOutOfRangeException("Teams name should be between 5 and 15 symbols.");
                }

                this.teamName = value;

            }
        }

        public IList<IMember> MemberList
        {
            get
            {
                return this.memberList;
            }
            set
            {
                this.MemberList = value;
            }
        }

        public IDictionary<string,IBoard> BoardList
        {
            get
            {
                return this.boardList;
            }
            set
            {
                this.boardList = value;
            }
        }
        #endregion
        #region Methods
        public void AddMember ()
        {

        }

        public void AddBoard()
        {

        }

        #endregion
    }
}
