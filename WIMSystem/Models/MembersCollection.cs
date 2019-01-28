using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class MembersCollection : IMembersCollection
    {
        // The single instance
        private static IDictionary<string, IMember> members;

        static MembersCollection()
        {
            members = new Dictionary<string, IMember>();

        }
        private MembersCollection() { }


        public IDictionary<string, IMember> Members
        {
            get
            {
                var membersToGet = members;
                return membersToGet;
            }
            private set
            {
                members = value;
            }
        }

        public void AddMember(IMember newMember)
        {
            try
            {
                this.Members.Add(newMember.MemberName, newMember);
            }
            catch (Exception)
            {
                //To be improved:
                throw new Exception("The name should be unique");
            }
        }

    }
}
