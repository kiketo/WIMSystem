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
        private IPersonsCollection personsCollection;
        #endregion
        #region Ctor
        public Person(string personName,IPersonsCollection personsCollection)
        {
            this.PersonName = personName;
            memberWorkItem = new List<IWorkItem>();
            this.personsCollection = personsCollection;
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
                //if(membersCollection.Contains(value))
                //{
                //    Console.WriteLine("Trqbva da se prepravi!");
                //}
                //else
                //{
                //}
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
