using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.ListCommands;
using WIMSystem.Models.Contracts;
using WIMSystem.Commands.Utils;

namespace UnitTestProject.CommandsTest.ListCommandsTest.ShowAllPeopleCommandTest
{
    [TestClass]
    public class ShowAllPeopleCommandConstructor_Should
    {
        [TestMethod]
        public void Throw_When_A_Passed_Value_Is_Null()
        {
            IPersonsCollection fakeList = null;
            var expectedMessage = string.Format(CommandsConsts.NULL_OBJECT,
                nameof(fakeList));

            var testMessage = Assert.ThrowsException<ArgumentNullException>(() => new ShowAllPeopleCommand(fakeList));

            Assert.AreEqual(expectedMessage, testMessage.ParamName);
        }
    }
}
