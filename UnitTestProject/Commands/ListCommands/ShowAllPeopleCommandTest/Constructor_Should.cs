using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WIMSystem.Commands.ListCommands;
using WIMSystem.Models.Contracts;
using WIMSystem.Commands.Utils;
using Moq;
using WIMSystem.Tests.Commands.ListCommans.FakeClasses;

namespace UnitTestProject.Commands.ListCommands.ShowAllPeopleCommandTest
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_When_A_Passed_Value_Is_Null()
        {
            IPersonsCollection personList = null;
            var expectedMessage = string.Format(CommandsConsts.NULL_OBJECT,
                nameof(personList));

            var testMessage = Assert.ThrowsException<ArgumentNullException>(() => new ShowAllPeopleCommand(personList));

            Assert.AreEqual(expectedMessage, testMessage.ParamName);
        }

        [TestMethod]
        public void Correctly_Assign_PersonList_Value()
        {
            var fakeList = new Mock<IPersonsCollection>();

            var sut = new FakeShowAllPeopleCommand(fakeList.Object);

            Assert.AreEqual(fakeList.Object, sut.PersonList);
        }
    }
}
