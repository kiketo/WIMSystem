using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class Person : IPerson
    {
        
        private string personName;
        private IList<IWorkItem> memberWorkItem;
        private bool isAssignedToTeam;
        
        
        public Person(string personName)
        {
            this.PersonName = personName;
            memberWorkItem = new List<IWorkItem>();
            this.IsAssignedToTeam = isAssignedToTeam;
        }

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

        public bool IsAssignedToTeam { get; set; }

    }
}
