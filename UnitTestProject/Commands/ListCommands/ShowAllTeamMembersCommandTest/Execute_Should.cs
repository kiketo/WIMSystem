using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.ListCommands;
using WIMSystem.Commands.Utils;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;
using WIMSystem.Utils;

namespace WIMSystem.Tests.Commands.ListCommands.ShowAllTeamMembersCommandTest
{
    [TestClass]
    public class Execute_Should
    {
        [TestMethod]
        public void ExecuteGetTeamMethod_WhenPassedDataIsValid()
        {
            //Arrange
            var validTeamName = "validTeam";
            var teamMock = new Mock<ITeam>();
            var gettersMock = new Mock<IGetters>();
            var fakeHistoryItemsCollection = new Mock<IHistoryItemsCollection>();
            var sut = new ShowAllTeamMembersCommand(gettersMock.Object, fakeHistoryItemsCollection.Object);
            var fakeList = new List<string>() { validTeamName };

            gettersMock.Setup(x => x.GetTeam(validTeamName)).Returns(teamMock.Object);

            //Act
            sut.Execute(fakeList);

            //Assert
            gettersMock.Verify(x => x.GetTeam(validTeamName), Times.Once);

        }

        [TestMethod]
        public void ExecuteShowAllTeamMembersMethod_WhenPassedDataIsValid()
        {
            //Arrange
            var validTeamName = "validTeam";
            var teamMock = new Mock<ITeam>();
            var gettersMock = new Mock<IGetters>();
            var fakeHistoryItemsCollection = new Mock<IHistoryItemsCollection>();
            var sut = new ShowAllTeamMembersCommand(gettersMock.Object, fakeHistoryItemsCollection.Object);
            var parameters = new List<string>() { validTeamName };

            gettersMock.Setup(x => x.GetTeam(validTeamName)).Returns(teamMock.Object);
            teamMock.Setup(x => x.ShowAllTeamMembers());

            //Act
            sut.Execute(parameters);

            //Assert
            teamMock.Verify(x => x.ShowAllTeamMembers(), Times.Once);
        }

        [TestMethod]
        public void ThrowsArgumentException_WhenPassedTeamIsNull()
        {
            //Arrange
            var validTeamName = "validTeam";
            ITeam fakeTeam = null;
            var gettersMock = new Mock<IGetters>();
            var fakeHistoryItemsCollection = new Mock<IHistoryItemsCollection>();
            var sut = new ShowAllTeamMembersCommand(gettersMock.Object, fakeHistoryItemsCollection.Object);
            var parameters = new List<string>() { validTeamName };

            gettersMock.Setup(x => x.GetTeam(validTeamName)).Returns(fakeTeam);
            var expectedMessage = string.Format(CommandsConsts.NULL_OBJECT, nameof(Team));

            //Act
            var realMessage = Assert.ThrowsException<ArgumentException>(()=>sut.Execute(parameters));

            //Assert
            Assert.AreEqual(expectedMessage, realMessage.Message);
        }
    }
}
