using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.CreateCommands;
using WIMSystem.Commands.Utils;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Tests.Commands.CreateCommands.CreateBugCommandTests
{
    [TestClass]
    public class Execute_Should
    {
        private string validTitle = "valid";
        private string validDescription = "description";
        private List<string> validSteps = new List<string>() { "1", "2" };
        private PriorityType validPriority = PriorityType.High;
        private BugSeverityType validSeverity = BugSeverityType.Critical;
        private string validTeamName = "any";
        private string validBoardName = "myBoard";
        private Mock<IBoard> boardMock = new Mock<IBoard>();
        private Mock<IBug> bugMock = new Mock<IBug>();
        private Mock<ITeam> teamMock = new Mock<ITeam>();
        private Mock<IHistoryEventWriter> historyEventWriterMock = new Mock<IHistoryEventWriter>();
        private Mock<IGetters> gettersMock = new Mock<IGetters>();
        private Mock<IComponentsFactory> componentsFactoryMock = new Mock<IComponentsFactory>();
        
        [TestMethod]
        public void ExecuteAllFourMethodsOnce_WhenValidParametersArePassed()
        {
            //Arrange

            this.gettersMock.Setup(x => x.GetBoard(this.validTeamName, this.validBoardName)).Returns(this.boardMock.Object);

            this.componentsFactoryMock.Setup(x => x.CreateBug(this.validTitle, this.validDescription, this.validSteps, this.validPriority, this.validSeverity, this.boardMock.Object, null)).Returns(this.bugMock.Object);

            var sut = new CreateBugCommand(this.historyEventWriterMock.Object, this.componentsFactoryMock.Object, this.gettersMock.Object);
            var parameters = new List<string>()
            {
                this.validTitle,
                this.validDescription,
                string.Join(",", this.validSteps),
                this.validPriority.ToString(),
                this.validSeverity.ToString(),
                this.validTeamName,
                this.validBoardName };

            //Act
            sut.Execute(parameters);

            //Assert
            this.gettersMock.Verify(x => x.GetBoard(this.validTeamName, this.validBoardName), Times.Once);
            this.componentsFactoryMock.Verify(x => x.CreateBug(this.validTitle, this.validDescription, this.validSteps, this.validPriority, this.validSeverity, this.boardMock.Object, null), Times.Once);
            this.boardMock.Verify(x => x.AddWorkItemToBoard(this.bugMock.Object), Times.Once);
            this.historyEventWriterMock.Verify(x => x.AddHistoryEvent(It.IsAny<string>(), null, this.boardMock.Object, It.IsAny<ITeam>(), this.bugMock.Object), Times.Once);
        }

        [TestMethod]
        public void ReturnCorrectMessage_WhenCorrectParametrsArePassed()
        {
            //Arrange
            
            gettersMock.Setup(x => x.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);

            componentsFactoryMock.Setup(x => x.CreateBug(validTitle, validDescription, validSteps, validPriority, validSeverity, boardMock.Object, null)).Returns(bugMock.Object);

            var sut = new CreateBugCommand(historyEventWriterMock.Object, componentsFactoryMock.Object, gettersMock.Object);
            var parameters = new List<string>()
            {
                validTitle,
                validDescription,
                string.Join(",", validSteps),
                validPriority.ToString(),
                validSeverity.ToString(),
                validTeamName,
                validBoardName
            };
            var expectedReturn = string.Format(CommandsConsts.ObjectCreated, nameof(Bug), validTitle);

            //Act
            var actualReturn = sut.Execute(parameters);

            //Assert
            Assert.AreEqual(expectedReturn, actualReturn);
        }

        [TestMethod]
        public void ThrowArgumentException_WhenNullBugIsReturned()
        {
            //Arrange
            
            gettersMock.Setup(x => x.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);

            var sut = new CreateBugCommand(historyEventWriterMock.Object, componentsFactoryMock.Object, gettersMock.Object);
            var parameters = new List<string>()
            {
                validTitle,
                validDescription,
                string.Join(",", validSteps),
                validPriority.ToString(),
                validSeverity.ToString(),
                validTeamName,
                validBoardName
            };

            //Act, Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));
        }
    }
}
