using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.ListCommands;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Tests.FakeClasses
{
    public class FakeShowAllPeopleCommand : ShowAllPeopleCommand
    {
        public FakeShowAllPeopleCommand(IPersonsCollection personList)
            : base(personList) { }


        public IPersonsCollection PersonList
        {
            get
            {
                return base.personList;
            }
        }
    }
}
