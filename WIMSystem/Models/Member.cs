using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class Member : IMember
    {
        #region Fields
        private string memberName;
        private IList<IWorkItem> memberWorkItem;
        #endregion
        #region Ctor
        public Member(string memberName, IList<IWorkItem> memberWorkItem)
        {
            this.MemberName = memberName;
            this.MemberWorkItems = memberWorkItem;
        }
        #endregion
        #region Prop

        public string MemberName
        {
            get
            {
                return this.memberName;
            }
            set
            {
                if (value.Length<5||value.Length>15)
                {
                    throw new ArgumentOutOfRangeException("Members name should be between 5 and 15 symbols.");
                }
                //Name should be unique in the application
                this.memberName = value;
            }
        }

        public IList<IWorkItem> MemberWorkItems
        {
            get
            {
                return this.memberWorkItem;
            }
            set
            {
                this.memberWorkItem = value;
            }
        }
        #endregion
        #region Methods

        #endregion
    }
}
