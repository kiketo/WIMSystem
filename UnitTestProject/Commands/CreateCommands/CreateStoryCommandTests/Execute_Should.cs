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

namespace WIMSystem.Tests.Commands.CreateCommands.CreateStoryCommandTests
{
    [TestClass]
    public class Execute_Should
    {
        private string validTitle = "valid";
        private string validDescription = "description";
        private PriorityType validPriority = PriorityType.High;
        private StorySizeType validStorySize = StorySizeType.Large;
        private string validTeamName = "ValidTeam";
        private string validBoardName = "ValidB";
        private Mock<IBoard> boardMock = new Mock<IBoard>();
        private Mock<IStory> storyMock = new Mock<IStory>();
        private Mock<IHistoryEventWriter> historyEventWriterMock = new Mock<IHistoryEventWriter>();
        private Mock<IGetters> gettersMock = new Mock<IGetters>();
        private Mock<IComponentsFactory> componentsFactoryMock = new Mock<IComponentsFactory>();

        [TestMethod]
        public void ExecuteAllFourMethodsOnce_WhenValidParametersArePassed()
        {
            //Arrange
            this.gettersMock.Setup(x => x.GetBoard(this.validTeamName, this.validBoardName)).Returns(this.boardMock.Object);

            this.componentsFactoryMock
                .Setup(x => x.CreateStory(this.validTitle, this.validDescription, this.validPriority, this.validStorySize, boardMock.Object, null))
                .Returns(this.storyMock.Object);

            var sut = new CreateStoryCommand(this.historyEventWriterMock.Object, this.componentsFactoryMock.Object, this.gettersMock.Object);
            var parameters = new List<string>()
            {
                this.validTitle,
                this.validDescription,
                this.validPriority.ToString(),
                this.validStorySize.ToString(),
                this.validTeamName,
                this.validBoardName
            };

            //Act
            var returnMessage = sut.Execute(parameters);

            //Assert
            this.gettersMock.Verify(x => x.GetBoard(this.validTeamName, this.validBoardName), Times.Once);
            this.componentsFactoryMock
                .Verify(x => x.CreateStory(
                    this.validTitle,
                    this.validDescription,
                    this.validPriority,
                    this.validStorySize,
                    boardMock.Object,
                    null),
               Times.Once);
            this.boardMock.Verify(x => x.AddWorkItemToBoard(this.storyMock.Object), Times.Once);
            this.historyEventWriterMock.
                Verify(x => x.AddHistoryEvent(
                    returnMessage,
                    It.IsAny<IPerson>(),
                    boardMock.Object,
                    It.IsAny<ITeam>(),
                    null
                    ), Times.Once);
        }

        [TestMethod]
        public void ReturnCorrectMessage_WhenCorrectParametrsArePassed()
        {
            //Arrange
            this.gettersMock.Setup(x => x.GetBoard(this.validTeamName, this.validBoardName)).Returns(this.boardMock.Object);

            this.componentsFactoryMock
                .Setup(x => x.CreateStory(this.validTitle, this.validDescription, this.validPriority, this.validStorySize, boardMock.Object, null))
                .Returns(this.storyMock.Object);

            var sut = new CreateStoryCommand(this.historyEventWriterMock.Object, this.componentsFactoryMock.Object, this.gettersMock.Object);
            var parameters = new List<string>()
            {
                this.validTitle,
                this.validDescription,
                this.validPriority.ToString(),
                this.validStorySize.ToString(),
                this.validTeamName,
                this.validBoardName
            };

            var expectedReturn = string.Format(CommandsConsts.ObjectCreated, nameof(Story), this.validTitle);

            //Act
            var actualReturn = sut.Execute(parameters);

            //Assert
            Assert.AreEqual(expectedReturn, actualReturn);
        }

        [TestMethod]
        public void ThrowArgumentException_WhenNullStoryIsReturned()
        {
            //Arrange
            this.gettersMock.Setup(x => x.GetBoard(this.validTeamName, this.validBoardName)).Returns(this.boardMock.Object);

            this.componentsFactoryMock
                .Setup(x => x.CreateStory(this.validTitle, this.validDescription, this.validPriority, this.validStorySize, boardMock.Object, null));

            var sut = new CreateStoryCommand(this.historyEventWriterMock.Object, this.componentsFactoryMock.Object, this.gettersMock.Object);
            var parameters = new List<string>()
            {
                this.validTitle,
                this.validDescription,
                this.validPriority.ToString(),
                this.validStorySize.ToString(),
                this.validTeamName,
                this.validBoardName
            };

            //Act, Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));
        }
    }
}
