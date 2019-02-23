using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.CreateCommands;
using WIMSystem.Commands.Utils;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Tests.Commands.CreateCommands.CreateBoardCommandTest
{
    [TestClass]
    public class Execute_Should
    {
        [TestMethod]
        public void ExecuteAllFourMethodsOnce_WhenValidParametersArePassed()
        {
            //Arrange
            var validBoarName = "Valid";
            var validTeamname = "ValidTeam";

            var boardMock = new Mock<IBoard>();

            var teamMock = new Mock<ITeam>();

            var historyEventWriterMock = new Mock<IHistoryEventWriter>();

            var componentsFactoryMock = new Mock<IComponentsFactory>();
            componentsFactoryMock.Setup(x => x.CreateBoard(validBoarName, teamMock.Object)).Returns(boardMock.Object);

            var gettersMock = new Mock<IGetters>();
            gettersMock.Setup(x => x.GetTeam(validTeamname)).Returns(teamMock.Object);

            var sut = new CreateBoardCommand(historyEventWriterMock.Object, componentsFactoryMock.Object, gettersMock.Object);
            var parameters = new List<string>() { validBoarName, validTeamname };
            
            //Act
            sut.Execute(parameters);
            
            //Assert
            gettersMock.Verify(x => x.GetTeam(validTeamname), Times.Once);
            componentsFactoryMock.Verify(x => x.CreateBoard(validBoarName, teamMock.Object), Times.Once);
            teamMock.Verify(x => x.AddBoardToTeam(It.IsAny<IBoard>()), Times.Once);
            historyEventWriterMock.Verify(x => x.AddHistoryEvent(It.IsAny<string>(),null, boardMock.Object,teamMock.Object,null), Times.Once);
        }

        [TestMethod]
        public void CorrectMessage_WhenValidParametersArePassed()
        {
            //Arrange
            var validBoarName = "Valid";
            var validTeamname = "ValidTeam";

            var boardMock = new Mock<IBoard>();

            var teamMock = new Mock<ITeam>();
            // teamMock.Setup(x => x.AddBoardToTeam(It.IsAny<IBoard>()));

            var historyEventWriterMock = new Mock<IHistoryEventWriter>();

            var componentsFactoryMock = new Mock<IComponentsFactory>();
            componentsFactoryMock.Setup(x => x.CreateBoard(validBoarName, teamMock.Object)).Returns(boardMock.Object);

            var gettersMock = new Mock<IGetters>();
            gettersMock.Setup(x => x.GetTeam(validTeamname)).Returns(teamMock.Object);

            var sut = new CreateBoardCommand(historyEventWriterMock.Object, componentsFactoryMock.Object, gettersMock.Object);
            var parameters = new List<string>() { validBoarName, validTeamname };

            var expectedReturn = string.Format(CommandsConsts.ObjectCreated, nameof(Board), validBoarName);

            //Act
            var actualReturn = sut.Execute(parameters);

            //Assert
            Assert.AreEqual(expectedReturn, actualReturn);
        }

        [TestMethod]
        public void ThrowArgumentException_WhenNullBoardIsReturned()
        {
            //Arrange
            var validBoarName = "Valid";
            var validTeamname = "ValidTeam";

            var boardMock = new Mock<IBoard>();

            var teamMock = new Mock<ITeam>();

            var historyEventWriterMock = new Mock<IHistoryEventWriter>();

            var componentsFactoryMock = new Mock<IComponentsFactory>();

            var gettersMock = new Mock<IGetters>();
            gettersMock.Setup(x => x.GetTeam(validTeamname)).Returns(teamMock.Object);

            var sut = new CreateBoardCommand(historyEventWriterMock.Object, componentsFactoryMock.Object, gettersMock.Object);
            var parameters = new List<string>() { validBoarName, validTeamname };

            //Act, Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));
        }

    }
}
