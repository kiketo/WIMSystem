using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.ListCommands;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;
using WIMSystem.Utils;

namespace WIMSystem.Tests.CommandsTest.ListCommandsTest.ShowAllTeamBoardsCommandTest
{
    [TestClass]
    public class Execute_Should
    {
        [TestMethod]
        public void Throw_When_A_Passed_Team_Is_Null()
        {
            //Arrange
            var validTeamName = "validTeam";
            ITeam team = null;
            var fakeGetters = new Mock<IGetters>();
            var fakeHistoryItemsCollection = new Mock<IHistoryItemsCollection>();
            var sut = new ShowAllTeamBoardsCommand(fakeGetters.Object, fakeHistoryItemsCollection.Object);
            var fakeList = new List<string>() { validTeamName };

            //Arrange
            fakeGetters.Setup(x => x.GetTeam(validTeamName)).Returns(team);
            var expectedMessage = string.Format(Consts.NULL_OBJECT, nameof(team));

            //Act,Assert
            var realMessage = Assert.ThrowsException<ArgumentException>(() => sut.Execute(fakeList));

            //Assert
            Assert.AreEqual(expectedMessage, realMessage.Message);

        }
    }
}
