using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using WIMSystem.Commands.ChangeCommands;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Models;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Contracts;
using WIMSystem.Utils;

namespace WIMSystem.Tests.Commands.ChangeCommands.ChangeRatingOfFeedbackCommandTests
{
    [TestClass]
    public class Execute_Should
    {
        private string validTeamName = "ValidTeam";
        private string validBoardName = "ValidB";
        private string validWorkItemTitle = "ValidTitle";
        private string validRating = "6";
        private Mock<IPerson> personMock = new Mock<IPerson>();
        private Mock<IBoard> boardMock = new Mock<IBoard>();

        private Mock<IHistoryEventWriter> historyEventWriterMock = new Mock<IHistoryEventWriter>();
        private Mock<IGetters> gettersMock = new Mock<IGetters>();

        [TestMethod]
        public void ExecuteAllMethodsOnce_WhenValidParametersArePassed()//TODO: In debug-mode works fine!!!
        {
            //Arrange
            var workItemMock = new Mock<IFeedback>();
            var sut = new ChangeRatingOfFeedbackCommand(historyEventWriterMock.Object, gettersMock.Object);
            gettersMock.Setup(x => x.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            gettersMock.Setup(x => x.GetWorkItem(boardMock.Object, validWorkItemTitle))
                .Returns(workItemMock.Object);
            workItemMock.Setup(x => x.Rating).Returns(int.Parse(validRating));
            workItemMock.Setup(x => x.Title).Returns(validWorkItemTitle);
            workItemMock.Setup(x => x.Board).Returns(boardMock.Object);
            historyEventWriterMock.Setup(x => x.AddHistoryEvent(It.IsAny<string>(), It.IsAny<IPerson>(), It.IsAny<IBoard>(), It.IsAny<ITeam>(), It.IsAny<IWorkItem>()));

            var parameters = new List<string>()
            {
                validTeamName,
                validBoardName,
                validWorkItemTitle,
                validRating,
            };
            var expectedMessage = string.Format(CommandsConsts.FeedbackRatingChange, workItemMock.Object.Title, validRating);
            //Act
            var returnMessage = sut.Execute(parameters);

            //Assert
            gettersMock.Verify(x => x.GetWorkItem(boardMock.Object, validWorkItemTitle), Times.Once);
            gettersMock.Verify(x => x.GetBoard(validTeamName, validBoardName), Times.Once);
            workItemMock.Verify(x => x.Rating, Times.Once);
            historyEventWriterMock.Verify(x => x.AddHistoryEvent(It.IsAny<string>(), It.IsAny<IPerson>(), It.IsAny<IBoard>(), It.IsAny<ITeam>(), It.IsAny<IWorkItem>()), Times.Once);

        }
        [TestMethod]
        public void ReturnCorrectMessage_WhenCorrectParametrsArePassed()
        {
            //Arrange
            var workItemMock = new Mock<IFeedback>();
            var sut = new ChangeRatingOfFeedbackCommand(historyEventWriterMock.Object, gettersMock.Object);
            gettersMock.Setup(x => x.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            gettersMock.Setup(x => x.GetWorkItem(boardMock.Object, validWorkItemTitle))
                .Returns(workItemMock.Object);
            workItemMock.Setup(x => x.Title).Returns(validWorkItemTitle);
            workItemMock.Setup(x => x.Board).Returns(boardMock.Object);
            historyEventWriterMock.Setup(x => x.AddHistoryEvent(It.IsAny<string>(), It.IsAny<IPerson>(), It.IsAny<IBoard>(), It.IsAny<ITeam>(), It.IsAny<IWorkItem>()));
            var parameters = new List<string>()
            {
                validTeamName,
                validBoardName,
                validWorkItemTitle,
                validRating,
            };
            var expectedMessage = string.Format(CommandsConsts.FeedbackRatingChange, workItemMock.Object.Title, validRating);
            //Act
            var returnMessage = sut.Execute(parameters);
            //Assert
            Assert.AreEqual(returnMessage, expectedMessage);
        }
        [TestMethod]
        public void ThrowArgumentException_WhenNullWorkItemIsPassed()
        {
            //Arrange
            var workItemMock = new Mock<IFeedback>();
            var sut = new ChangeRatingOfFeedbackCommand(historyEventWriterMock.Object, gettersMock.Object);
            gettersMock.Setup(x => x.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            workItemMock.Setup(x => x.Title).Returns(validWorkItemTitle);
            workItemMock.Setup(x => x.Board).Returns(boardMock.Object);
            historyEventWriterMock.Setup(x => x.AddHistoryEvent(It.IsAny<string>(), It.IsAny<IPerson>(), It.IsAny<IBoard>(), It.IsAny<ITeam>(), It.IsAny<IWorkItem>()));
            var parameters = new List<string>()
            {
                validTeamName,
                validBoardName,
                validWorkItemTitle,
                validRating,
            };
            var expectedMessage = string.Format(Consts.NULL_OBJECT, nameof(WorkItem));
            //Act
            var exeption = Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));
            //Assert
            Assert.AreEqual(exeption.Message, expectedMessage);
        }
        [TestMethod]
        public void ThrowArgumentException_WhenWorkItemIsNotFeedback()
        {
            //Arrange
            var workItemMock = new Mock<IWorkItem>();
            var sut = new ChangeRatingOfFeedbackCommand(historyEventWriterMock.Object, gettersMock.Object);
            gettersMock.Setup(x => x.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            gettersMock.Setup(x => x.GetWorkItem(boardMock.Object, validWorkItemTitle))
               .Returns(workItemMock.Object);
            workItemMock.Setup(x => x.Title).Returns(validWorkItemTitle);
            workItemMock.Setup(x => x.Board).Returns(boardMock.Object);
            historyEventWriterMock.Setup(x => x.AddHistoryEvent(It.IsAny<string>(), It.IsAny<IPerson>(), It.IsAny<IBoard>(), It.IsAny<ITeam>(), It.IsAny<IWorkItem>()));
            var parameters = new List<string>()
            {
                validTeamName,
                validBoardName,
                validWorkItemTitle,
                validRating,
            };
            var expectedMessage = string.Format($"{workItemMock.Object.GetType().Name} is not a {nameof(Feedback)}!");
            //Act
            var exeption = Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));
            //Assert
            Assert.AreEqual(exeption.Message, expectedMessage);
        }
    }
}
