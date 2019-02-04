using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class Person : IPerson
    {
        #region Fields
        private string personName;
        private IList<IWorkItem> memberWorkItem;
        #endregion
        #region Ctor
        public Person(string personName)
        {
            this.PersonName = personName;
            memberWorkItem = new List<IWorkItem>();
        }
        #endregion
        #region Prop

        public string PersonName
        {
            get
            {
                return this.personName;
            }
            private set
            {
                if (value.Length<5||value.Length>15)
                {
                    throw new ArgumentOutOfRangeException("Members name should be between 5 and 15 symbols.");
                }
                //Name should be unique in the application
                this.personName = value;
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
