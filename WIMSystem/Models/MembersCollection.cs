using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class MembersCollection : IMembersCollection
    {
        // The single instance
        private readonly IDictionary<string, IPerson> membersList;
        private static IMembersCollection instance;

        static MembersCollection()
        {
            instance = new MembersCollection();
        }
        private MembersCollection()
        {
            this.membersList = new Dictionary<string, IPerson>();
        }

        public static IMembersCollection Instance
        {
            get { return instance; }
        }

        public IDictionary<string, IPerson> Members
        {
            get
            {
                return new Dictionary<string, IPerson>(membersList);
            }
        }

        public void AddMember(IPerson newMember)
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

        public IPerson this[string index]
        {
            get => this.membersList[index];
            private set
            {
                this.membersList[index] = value;
            }
        }
    }
}
