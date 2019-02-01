using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class MembersCollection : IMembersCollection
    {
        // The single instance
        private readonly IDictionary<string, IMember> membersList;
        private static IMembersCollection instance;

        static MembersCollection()
        {
            instance = new MembersCollection();
        }
        private MembersCollection()
        {
            this.membersList = new Dictionary<string, IMember>();
        }

        public static IMembersCollection Instance
        {
            get { return instance; }
        }

        public IDictionary<string, IMember> Members
        {
            get
            {
                return new Dictionary<string, IMember>(membersList);
            }
        }

        public void AddMember(IMember newMember)
        {
            if (this.membersList.ContainsKey(newMember.MemberName))
            {
                throw new ArgumentException($"{nameof(ITeam)} with that name exist!");
            }
            this.membersList.Add(newMember.MemberName, newMember);
        }

        public bool Contains(string name)
        {
            return membersList.ContainsKey(name);
        }

        public IMember this[string index]
        {
            get => this.membersList[index];
            private set
            {
                this.membersList[index] = value;
            }
        }
    }
}
