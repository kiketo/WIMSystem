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

namespace WIMSystem.Tests.Commands.ChangeCommands.ChangePriorityCommandTests
{
    [TestClass]
    public class Execute_Should
    {
        [TestMethod]
        public void ExecuteAllMethodsOnce_WhenValidParametersArePassed()
        {
            //Arrange
            var validTeamName = "validTeam";
            var validBoardName = "validB";
            var validWorkItemTitle = "validW";
            var priority = "high";

            //Arrange
            var historyEventWriterMock = new Mock<IHistoryEventWriter>();
            var gettersMock = new Mock<IGetters>();
            var boardMock = new Mock<IBoard>();
            var workItemMock = new Mock<IAssignableWorkItem>();
            var mockAssignee = new Mock<IPerson>();
            var teamMock = new Mock<ITeam>();

            //Arrange
            gettersMock.Setup(b => b.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            gettersMock.Setup(w => w.GetAssignableWorkItem(boardMock.Object, validWorkItemTitle)).Returns(workItemMock.Object);

            //Arrange
            boardMock.Setup(n => n.BoardName).Returns(validBoardName);
            boardMock.Setup(n => n.Team).Returns(It.IsAny<ITeam>());

            //Arrange
            workItemMock.Setup(p => p.Priority).Returns(PriorityType.Low);
            workItemMock.Setup(p => p.Title).Returns(validWorkItemTitle);
            workItemMock.Setup(a => a.Assignee).Returns(mockAssignee.Object);
            workItemMock.Setup(a => a.Board).Returns(boardMock.Object);
            workItemMock.Setup(a => a.Board.Team).Returns(teamMock.Object);

            var parameters = new List<string>() { validTeamName, validBoardName, validWorkItemTitle, priority };

            var sut = new ChangePriorityCommand(historyEventWriterMock.Object, gettersMock.Object);

            //Act
            var returnMessage = sut.Execute(parameters);

            //Assert
            gettersMock.Verify(x => x.GetBoard(validTeamName, validBoardName), Times.Once);
            gettersMock.Verify(x => x.GetAssignableWorkItem(boardMock.Object, validWorkItemTitle), Times.Once);
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
        public void ReturnSuccessMessage_WhenValidDataIsPassed()
        {
            //Arrange
            var validTeamName = "validTeam";
            var validBoardName = "validB";
            var validWorkItemTitle = "validW";
            var priority = "High";

            //Arrange
            var historyEventWriterMock = new Mock<IHistoryEventWriter>();
            var gettersMock = new Mock<IGetters>();
            var boardMock = new Mock<IBoard>();
            var workItemMock = new Mock<IAssignableWorkItem>();
            var mockAssignee = new Mock<IPerson>();
            var teamMock = new Mock<ITeam>();

            //Arrange
            gettersMock.Setup(b => b.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            gettersMock.Setup(w => w.GetAssignableWorkItem(boardMock.Object, validWorkItemTitle)).Returns(workItemMock.Object);

            //Arrange
            boardMock.Setup(n => n.BoardName).Returns(validBoardName);
            boardMock.Setup(n => n.Team).Returns(It.IsAny<ITeam>());

            //Arrange
            workItemMock.Setup(p => p.Priority).Returns(PriorityType.Low);
            workItemMock.Setup(p => p.Title).Returns(validWorkItemTitle);
            workItemMock.Setup(a => a.Assignee).Returns(mockAssignee.Object);
            workItemMock.Setup(a => a.Board).Returns(boardMock.Object);
            workItemMock.Setup(a => a.Board.Team).Returns(teamMock.Object);

            var parameters = new List<string>() { validTeamName, validBoardName, validWorkItemTitle, priority };

            var sut = new ChangePriorityCommand(historyEventWriterMock.Object, gettersMock.Object);
            var expecterMessage = string.Format(CommandsConsts.WorkItemPriorityChange, workItemMock.Object.Title, priority);

            //Act
            var actualMessage = sut.Execute(parameters);


            //Assert
            Assert.AreEqual(expecterMessage, actualMessage);
        }

        [TestMethod]
        public void MemberIsAssignee_WhenWorkItemIsAssignable()
        {
            //Arrange
            var validTeamName = "validTeam";
            var validBoardName = "validB";
            var validWorkItemTitle = "validW";
            var priority = "High";

            //Arrange
            var historyEventWriterMock = new Mock<IHistoryEventWriter>();
            var gettersMock = new Mock<IGetters>();
            var boardMock = new Mock<IBoard>();
            var workItemMock = new Mock<IAssignableWorkItem>();
            var mockAssignee = new Mock<IPerson>();
            var teamMock = new Mock<ITeam>();

            //Arrange
            gettersMock.Setup(b => b.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            gettersMock.Setup(w => w.GetAssignableWorkItem(boardMock.Object, validWorkItemTitle)).Returns(workItemMock.Object);

            //Arrange
            boardMock.Setup(n => n.BoardName).Returns(validBoardName);
            boardMock.Setup(n => n.Team).Returns(It.IsAny<ITeam>());

            //Arrange
            workItemMock.Setup(p => p.Priority).Returns(PriorityType.Low);
            workItemMock.Setup(p => p.Title).Returns(validWorkItemTitle);
            workItemMock.Setup(a => a.Assignee).Returns(mockAssignee.Object);
            workItemMock.Setup(a => a.Board).Returns(boardMock.Object);
            workItemMock.Setup(a => a.Board.Team).Returns(teamMock.Object);

            var parameters = new List<string>() { validTeamName, validBoardName, validWorkItemTitle, priority };

            var sut = new ChangePriorityCommand(historyEventWriterMock.Object, gettersMock.Object);

            //Act
            sut.Execute(parameters);

            //Assert
            Assert.AreEqual(mockAssignee.Object, workItemMock.Object.Assignee);
        }

        [TestMethod]
        public void ThrowsArgumentException_WhenPassedWorkItemIsNull()
        {
            //Arrange
            var validTeamName = "validTeam";
            var validBoardName = "validB";
            var validWorkItemTitle = "validW";
            var priority = "High";

            //Arrange
            var historyEventWriterMock = new Mock<IHistoryEventWriter>();
            var gettersMock = new Mock<IGetters>();
            var boardMock = new Mock<IBoard>();
            IAssignableWorkItem fakeWorkItem = null;
            var teamMock = new Mock<ITeam>();

            //Arrange
            gettersMock.Setup(b => b.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            gettersMock.Setup(w => w.GetAssignableWorkItem(boardMock.Object, validWorkItemTitle)).Returns(fakeWorkItem);

            //Arrange
            var parameters = new List<string>() { validTeamName, validBoardName, validWorkItemTitle, priority };

            var sut = new ChangePriorityCommand(historyEventWriterMock.Object, gettersMock.Object);
            var expectedMessage = string.Format(Consts.NULL_OBJECT, nameof(WorkItem));


            //Act,Assert
            var realMessage = 
                    Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));

            //Assert
            Assert.AreEqual(expectedMessage, realMessage.Message);
        }
    }
}
