using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.ChangeCommands;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Tests.Commands.ChangeCommands.ChangePriorityCommandTests
{
    [TestClass]
    public class Execute_Should
    {
        [TestMethod]
        public void ExecuteAllMethodsOnce_WhenValidParametersArePassed()
        {
            var validTeamName = "validTeam";
            var validBoardName = "validB";
            var validWorkItemTitle = "validW";
            var priority = "high";

            var historyEventWriterMock = new Mock<IHistoryEventWriter>();
            var gettersMock = new Mock<IGetters>();
            var boardMock = new Mock<IBoard>();
            var workItemMock = new Mock<IBug>();
            var mockAssignee = new Mock<IPerson>();
            var teamMock = new Mock<ITeam>();

            gettersMock.Setup(b => b.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            gettersMock.Setup(w => w.GetAssignableWorkItem(boardMock.Object, validWorkItemTitle)).Returns(workItemMock.Object);

            boardMock.Setup(n => n.BoardName).Returns(validBoardName);
            boardMock.Setup(n => n.Team).Returns(It.IsAny<ITeam>());

            workItemMock.Setup(p => p.Priority).Returns(PriorityType.Low);
            workItemMock.Setup(p => p.Title).Returns(validWorkItemTitle);
            workItemMock.Setup(a => a.Assignee).Returns(mockAssignee.Object);
            workItemMock.Setup(a => a.Board).Returns(boardMock.Object);
            workItemMock.Setup(a => a.Board.Team).Returns(teamMock.Object);

            var parameters = new List<string>() { validTeamName, validBoardName, validWorkItemTitle, priority };

            var sut = new ChangePriorityCommand(historyEventWriterMock.Object, gettersMock.Object);

            var returnMessage = sut.Execute(parameters);

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
            var validTeamName = "validTeam";
            var validBoardName = "validB";
            var validWorkItemTitle = "validW";
            var priority = "High";

            var historyEventWriterMock = new Mock<IHistoryEventWriter>();
            var gettersMock = new Mock<IGetters>();
            var boardMock = new Mock<IBoard>();
            var workItemMock = new Mock<IBug>();
            var mockAssignee = new Mock<IPerson>();
            var teamMock = new Mock<ITeam>();

            gettersMock.Setup(b => b.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            gettersMock.Setup(w => w.GetAssignableWorkItem(boardMock.Object, validWorkItemTitle)).Returns(workItemMock.Object);

            boardMock.Setup(n => n.BoardName).Returns(validBoardName);
            boardMock.Setup(n => n.Team).Returns(It.IsAny<ITeam>());

            workItemMock.Setup(p => p.Priority).Returns(PriorityType.Low);
            workItemMock.Setup(p => p.Title).Returns(validWorkItemTitle);
            workItemMock.Setup(a => a.Assignee).Returns(mockAssignee.Object);
            workItemMock.Setup(a => a.Board).Returns(boardMock.Object);
            workItemMock.Setup(a => a.Board.Team).Returns(teamMock.Object);

            var parameters = new List<string>() { validTeamName, validBoardName, validWorkItemTitle, priority };

            var sut = new ChangePriorityCommand(historyEventWriterMock.Object, gettersMock.Object);

            var actualMessage = sut.Execute(parameters);

            var expecterMessage = string.Format(CommandsConsts.WorkItemPriorityChange, workItemMock.Object.Title, priority);

            Assert.AreEqual(expecterMessage, actualMessage);
        }

        //TODO: Member is assignee if it is assignable work item

        //TODO: Member is null if it is not assignable work item

        //TODO: ExceptionThrowing
    }
}
