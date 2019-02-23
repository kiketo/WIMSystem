using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.ChangeCommands;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Models;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;
using WIMSystem.Utils;

namespace WIMSystem.Tests.Commands.ChangeCommands.ChangeSeverityOfBugCommandTests
{
    [TestClass]
    public class Execute_Should
    {
        [TestMethod]

        public void ExecuteAllMethods_WhenValidParametersArePassed()
        {
            //Arrange
            var validTeamName = "validTeam";
            var validBoardName = "validB";
            var validBugTitle = "validBug";
            var severity = "critical";

            //Arrange
            var historyEventWriterMock = new Mock<IHistoryEventWriter>();
            var gettersMock = new Mock<IGetters>();
            var boardMock = new Mock<IBoard>();
            var workItemMock = new Mock<IBug>();
            var mockAssignee = new Mock<IPerson>();
            var teamMock = new Mock<ITeam>();

            //Arrange
            gettersMock.Setup(b => b.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            gettersMock.Setup(w => w.GetWorkItem(boardMock.Object, validBugTitle)).Returns(workItemMock.Object);

            //Arrange
            boardMock.Setup(n => n.BoardName).Returns(validBoardName);
            boardMock.Setup(n => n.Team).Returns(It.IsAny<ITeam>());

            //Arrange
            workItemMock.Setup(p => p.Severity).Returns(BugSeverityType.Minor);
            workItemMock.Setup(p => p.Title).Returns(validBugTitle);
            workItemMock.Setup(a => a.Assignee).Returns(mockAssignee.Object);
            workItemMock.Setup(a => a.Board).Returns(boardMock.Object);
            workItemMock.Setup(a => a.Board.Team).Returns(teamMock.Object);

            //Arrange
            var parameters = new List<string>() { validTeamName, validBoardName, validBugTitle, severity };

            var sut = new ChangeSeverityOfBugCommand(historyEventWriterMock.Object, gettersMock.Object);

            //Act
            var returnMessage = sut.Execute(parameters);

            //Assert
            gettersMock.Verify(x => x.GetBoard(validTeamName, validBoardName), Times.Once);
            gettersMock.Verify(x => x.GetWorkItem(boardMock.Object, validBugTitle), Times.Once);
            workItemMock.VerifySet(x => x.Severity = BugSeverityType.Critical, Times.Once);
            historyEventWriterMock.
            Verify(x => x.AddHistoryEvent(
                returnMessage,
                mockAssignee.Object,
                It.IsAny<IBoard>(),
                It.IsAny<ITeam>(),
                workItemMock.Object
                ), Times.Once);
        }

        [TestMethod]
        public void ReturnSuccessMessage_WhenPassedDataIsValid()
        {
            //Arrange
            var validTeamName = "validTeam";
            var validBoardName = "validB";
            var validBugTitle = "validBug";
            var severity = "Critical";

            //Arrange
            var historyEventWriterMock = new Mock<IHistoryEventWriter>();
            var gettersMock = new Mock<IGetters>();
            var boardMock = new Mock<IBoard>();
            var workItemMock = new Mock<IBug>();
            var mockAssignee = new Mock<IPerson>();
            var teamMock = new Mock<ITeam>();

            //Arrange
            gettersMock.Setup(b => b.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            gettersMock.Setup(w => w.GetWorkItem(boardMock.Object, validBugTitle)).Returns(workItemMock.Object);

            //Arrange
            boardMock.Setup(n => n.BoardName).Returns(validBoardName);
            boardMock.Setup(n => n.Team).Returns(It.IsAny<ITeam>());

            //Arrange
            workItemMock.Setup(p => p.Severity).Returns(BugSeverityType.Minor);
            workItemMock.Setup(p => p.Title).Returns(validBugTitle);
            workItemMock.Setup(a => a.Assignee).Returns(mockAssignee.Object);
            workItemMock.Setup(a => a.Board).Returns(boardMock.Object);
            workItemMock.Setup(a => a.Board.Team).Returns(teamMock.Object);

            //Arrange
            var parameters = new List<string>() { validTeamName, validBoardName, validBugTitle, severity };

            var sut = new ChangeSeverityOfBugCommand(historyEventWriterMock.Object, gettersMock.Object);
            

            //Act
            var returnMessage = sut.Execute(parameters);
            var expectedMessage = string.Format(CommandsConsts.BugSeverityChange, validBugTitle, severity);

            //Assert
            Assert.AreEqual(expectedMessage, returnMessage);
        }

        [TestMethod]
        public void ThrowsArgumentException_WhenPassedWorkItemIsNull()
        {
            //Arrange
            var validTeamName = "validTeam";
            var validBoardName = "validB";
            var validBugTitle = "validBug";
            var severity = "Critical";

            //Arrange
            var historyEventWriterMock = new Mock<IHistoryEventWriter>();
            var gettersMock = new Mock<IGetters>();
            var boardMock = new Mock<IBoard>();
            IAssignableWorkItem fakeWorkItem = null;
            var teamMock = new Mock<ITeam>();

            //Arrange
            gettersMock.Setup(b => b.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            gettersMock.Setup(w => w.GetWorkItem(boardMock.Object, validBugTitle)).Returns(fakeWorkItem);

            //Arrange
            var parameters = new List<string>() { validTeamName, validBoardName, validBugTitle, severity };

            var sut = new ChangeSeverityOfBugCommand(historyEventWriterMock.Object, gettersMock.Object);
            var expectedMessage = string.Format(Consts.NULL_OBJECT, nameof(WorkItem));


            //Act,Assert
            var realMessage =
                    Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));

            //Assert
            Assert.AreEqual(expectedMessage, realMessage.Message);
        }

        [TestMethod]
        public void ThrowsArgumentException_WhenPassedWorkItemIsNotBug()
        {
            //Arrange
            var validTeamName = "validTeam";
            var validBoardName = "validB";
            var validTitle = "validItem";
            var severity = "Critical";

            //Arrange
            var historyEventWriterMock = new Mock<IHistoryEventWriter>();
            var gettersMock = new Mock<IGetters>();
            var boardMock = new Mock<IBoard>();
            var workItemMock = new Mock<IStory>();
            var teamMock = new Mock<ITeam>();

            //Arrange
            gettersMock.Setup(b => b.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            gettersMock.Setup(w => w.GetWorkItem(boardMock.Object, validTitle)).Returns(workItemMock.Object);

            //Arrange
            var parameters = new List<string>() { validTeamName, validBoardName, validTitle, severity };

            var sut = new ChangeSeverityOfBugCommand(historyEventWriterMock.Object, gettersMock.Object);
            var expectedMessage = string.Format($"{workItemMock.Object.GetType().Name} is not a {nameof(Bug)}!");


            //Act,Assert
            var realMessage =
                    Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));

            //Assert
            Assert.AreEqual(expectedMessage, realMessage.Message);
        }

    }
}
