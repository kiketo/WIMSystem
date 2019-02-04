using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class Person : IPerson
    {
        #region Fields
        private string memberName;
        private IList<IWorkItem> memberWorkItem;
        private IMembersCollection membersCollection;
        #endregion
        #region Ctor
        public Person(string memberName,IMembersCollection membersCollection)
        {
            this.MemberName = memberName;
            memberWorkItem = new List<IWorkItem>();
            this.membersCollection = membersCollection;
        }
        #endregion
        #region Prop

        public string MemberName
        {
            get
            {
                return this.memberName;
            }
            private set
            {
                if (value.Length<5||value.Length>15)
                {
                    throw new ArgumentOutOfRangeException("Members name should be between 5 and 15 symbols.");
                }
                //Name should be unique in the application
                //if(membersCollection.Contains(value))
                //{
                //    Console.WriteLine("Trqbva da se prepravi!");
                //}
                //else
                //{
                //}
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
