using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.ListCommands;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Tests.CommandsTest.ListCommandsTest.ShowAllPeopleCommandTest
{
    [TestClass]
    public class Execute_Should
    {
        [TestMethod]
        public void Call_ShowAllPeople_Method_To_The_Persons_Collection()
        {
            var fakeList = new Mock<IPersonsCollection>();
            var sut = new ShowAllPeopleCommand(fakeList.Object);
            fakeList.Setup(x => x.ShowAllPeople());
            var fakeParameters = new List<string>();

            sut.Execute(fakeParameters);

            fakeList.Verify(x => x.ShowAllPeople(), Times.Once);
        }
    }
}
