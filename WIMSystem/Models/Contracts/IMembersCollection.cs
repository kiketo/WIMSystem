using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface IMembersCollection
    {
        IDictionary<string, IPerson> Members { get; }
        void AddMember(IPerson newMember);
        bool Contains(string name);
        IPerson this[string index] { get; }
    }
}
