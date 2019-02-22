using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.ChangeCommands;
using WIMSystem.Commands.Contracts;
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
            var workItemCollection = new Dictionary<string, IWorkItem>();



            workItemCollection.Add(validWorkItemTitle, workItemMock.Object);

            gettersMock.Setup(b => b.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            gettersMock.Setup(w => w.GetAssignableWorkItem(boardMock.Object, validWorkItemTitle)).Returns(workItemMock.Object);

            historyEventWriterMock.Setup(r => r.AddHistoryEvent(
                It.IsAny<string>(),
                    It.IsAny<IPerson>(),
                    It.IsAny<IBoard>(),
                    It.IsAny<ITeam>(),
                    It.IsAny<IWorkItem>()));


            boardMock.Setup(n => n.BoardName).Returns(validBoardName);
            boardMock.Setup(n => n.Team).Returns(It.IsAny<ITeam>());
            boardMock.Setup(n => n.BoardWorkItems).Returns(workItemCollection);

            workItemMock.Setup(p => p.Priority).Returns(PriorityType.Low);
            workItemMock.Setup(p => p.Title).Returns(validWorkItemTitle);
            workItemMock.Setup(a => a.Assignee).Returns(It.IsAny<IPerson>());
            
            
            var parameters = new List<string>() { validTeamName, validBoardName, validWorkItemTitle, priority };

            var sut = new ChangePriorityCommand(historyEventWriterMock.Object, gettersMock.Object);

            sut.Execute(parameters);

            gettersMock.Verify(x => x.GetBoard(validTeamName, validBoardName), Times.Once);
            historyEventWriterMock.
                Verify(x => x.AddHistoryEvent(
                    It.IsAny<string>(),
                    It.IsAny<IPerson>(),
                    It.IsAny<IBoard>(),
                    It.IsAny<ITeam>(),
                    It.IsAny<IWorkItem>()
                    ), Times.Once);
        }
    }
}
