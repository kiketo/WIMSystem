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

namespace WIMSystem.Tests.Commands.CreateCommands.CreateFeedbackCommandTests
{
    [TestClass]
    public class Execute_Should
    {
        private string validTitle = "Valid";
        private string validDescription = "description";
        private int validRating = 1;
        private string validTeamName = "ValidTeam";
        private string validBoardName = "ValidB";
        private Mock<IBoard> boardMock = new Mock<IBoard>();
        private Mock<IFeedback> feedbackMock = new Mock<IFeedback>();
        private Mock<IHistoryEventWriter> historyEventWriterMock = new Mock<IHistoryEventWriter>();
        private Mock<IGetters> gettersMock = new Mock<IGetters>();
        private Mock<IComponentsFactory> componentsFactoryMock = new Mock<IComponentsFactory>();

        [TestMethod]
        public void ExecuteAllFourMethodsOnce_WhenValidParametersArePassed()
        {
            //Arrange
            this.gettersMock.Setup(x => x.GetBoard(this.validTeamName, this.validBoardName)).Returns(this.boardMock.Object);

            this.componentsFactoryMock
                .Setup(x => x.CreateFeedback(this.validTitle, this.validDescription, this.validRating, It.IsAny<IBoard>()))
                .Returns(this.feedbackMock.Object);

            var sut = new CreateFeedbackCommand(this.historyEventWriterMock.Object, this.componentsFactoryMock.Object, this.gettersMock.Object);
            var parameters = new List<string>()
            {
                this.validTitle,
                this.validDescription,
                this.validRating.ToString(),
                this.validTeamName,
                this.validBoardName
            };

            //Act
            sut.Execute(parameters);

            //Assert
            this.gettersMock.Verify(x => x.GetBoard(this.validTeamName, this.validBoardName), Times.Once);
            this.componentsFactoryMock.Verify(x => x.CreateFeedback(this.validTitle, this.validDescription, this.validRating, It.IsAny<IBoard>()), Times.Once);
            this.boardMock.Verify(x => x.AddWorkItemToBoard(feedbackMock.Object), Times.Once);
            this.historyEventWriterMock.
                Verify(x => x.AddHistoryEvent(
                    It.IsAny<string>(),
                    It.IsAny<IPerson>(),
                    It.IsAny<IBoard>(),
                    It.IsAny<ITeam>(),
                    It.IsAny<IWorkItem>()
                    ), Times.Once);
        }

        [TestMethod]
        public void ReturnCorrectMessage_WhenCorrectParametrsArePassed()
        {
            //Arrange
            this.gettersMock.Setup(x => x.GetBoard(this.validTeamName, this.validBoardName)).Returns(this.boardMock.Object);

            this.componentsFactoryMock
                .Setup(x => x.CreateFeedback(this.validTitle, this.validDescription, this.validRating, It.IsAny<IBoard>()))
                .Returns(this.feedbackMock.Object);

            var sut = new CreateFeedbackCommand(this.historyEventWriterMock.Object, this.componentsFactoryMock.Object, this.gettersMock.Object);
            var parameters = new List<string>()
            {
                this.validTitle,
                this.validDescription,
                this.validRating.ToString(),
                this.validTeamName,
                this.validBoardName
            };


            var expectedReturn = string.Format(CommandsConsts.ObjectCreated, nameof(Feedback), validTitle);

            //Act
            var actualReturn = sut.Execute(parameters);

            //Assert
            Assert.AreEqual(expectedReturn, actualReturn);
        }

        [TestMethod]
        public void ThrowArgumentException_WhenNullFeedbackIsReturned()
        {
            //Arrange
            this.gettersMock.Setup(x => x.GetBoard(this.validTeamName, this.validBoardName)).Returns(this.boardMock.Object);

            this.componentsFactoryMock
                .Setup(x => x.CreateFeedback(this.validTitle, this.validDescription, this.validRating, It.IsAny<IBoard>()));

            var sut = new CreateFeedbackCommand(this.historyEventWriterMock.Object, this.componentsFactoryMock.Object, this.gettersMock.Object);
            var parameters = new List<string>()
            {
                this.validTitle,
                this.validDescription,
                this.validRating.ToString(),
                this.validTeamName,
                this.validBoardName
            };

            //Act, Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));
        }

    }
}

