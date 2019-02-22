using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Utils;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Tests.Commands.Utils.FakeClasses
{
    public class FakeGetters : Getters
    {
        public FakeGetters(IPersonsCollection personList, IWIMTeams wimTeams) : base(personList, wimTeams)
        {
        }
        public IPersonsCollection PersonList
        {
            get
            {
                return base.personList;
            }
        }
        public IWIMTeams WIMTeams
        {
            get
            {
                return base.wimTeams;
            }
        }
    }
}
