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

namespace WIMSystem.Tests.Commands.CreateCommands.CreateCommentCommandTests
{
    [TestClass]
    public class Execute_Should
    {
        string validTeamName = "ValidTeam";
        string validBoardName = "ValidB";
        string validWorkItemTitle = "ValidTitle";
        string validMessage = "ValidMessage";
        string validAuthorName = "ValidName";
        private Mock<IComment> commentMock = new Mock<IComment>();
        private Mock<IHistoryEventWriter> historyEventWriterMock = new Mock<IHistoryEventWriter>();
        private Mock<IGetters> gettersMock = new Mock<IGetters>();
        private Mock<IComponentsFactory> componentsFactoryMock = new Mock<IComponentsFactory>();
        private Mock<IWorkItem> workItemMock = new Mock<IWorkItem>();

        [TestMethod]
        public void ExecuteAllSixMethodsOnce_WhenValidParametersArePassed()
        {
            //Arrange
            gettersMock.Setup(x => x.GetWorkItem(It.IsAny<IBoard>(), validWorkItemTitle)).Returns(workItemMock.Object);

            this.componentsFactoryMock.Setup(x => x.CreateComment(this.validMessage, It.IsAny<IPerson>())).Returns(this.commentMock.Object);

            var sut = new CreateCommentCommand(this.historyEventWriterMock.Object, this.componentsFactoryMock.Object, this.gettersMock.Object);
            var parameters = new List<string>()
            {
                this.validTeamName,
                this.validBoardName,
                this.validWorkItemTitle,
                this.validMessage,
                this.validAuthorName,
            };

            //Act
            var returnMessage = sut.Execute(parameters);

            //Assert
            this.gettersMock.Verify(x => x.GetBoard(this.validTeamName, this.validBoardName), Times.Once);
            this.gettersMock.Verify(x => x.GetMember(It.IsAny<ITeam>(), this.validAuthorName), Times.Once);
            this.gettersMock.Verify(x => x.GetWorkItem(It.IsAny<IBoard>(), this.validWorkItemTitle), Times.Once);
            this.gettersMock.Verify(x => x.GetTeam(this.validTeamName), Times.Once);
            this.componentsFactoryMock.Verify(x => x.CreateComment(validMessage,It.IsAny<IPerson>()), Times.Once);
            this.workItemMock.Verify(x => x.AddComment(commentMock.Object), Times.Once);
            this.historyEventWriterMock.
                Verify(x => x.AddHistoryEvent(
                    returnMessage,
                    It.IsAny<IPerson>(),
                    It.IsAny<IBoard>(),
                    It.IsAny<ITeam>(),
                    workItemMock.Object
                    ), Times.Once);
        }

        [TestMethod]
        public void ReturnCorrectMessage_WhenCorrectParametrsArePassed()
        {
            //Arrange
            gettersMock.Setup(x => x.GetWorkItem(It.IsAny<IBoard>(), validWorkItemTitle)).Returns(workItemMock.Object);

            this.componentsFactoryMock.Setup(x => x.CreateComment(this.validMessage, It.IsAny<IPerson>())).Returns(this.commentMock.Object);

            var sut = new CreateCommentCommand(this.historyEventWriterMock.Object, this.componentsFactoryMock.Object, this.gettersMock.Object);
            var parameters = new List<string>()
            {
                this.validTeamName,
                this.validBoardName,
                this.validWorkItemTitle,
                this.validMessage,
                this.validAuthorName,
            };

            var expectedReturn = string.Format(CommandsConsts.CommentAdded, validMessage, validAuthorName, validWorkItemTitle);

            //Act
            var actualReturn = sut.Execute(parameters);

            //Assert
            Assert.AreEqual(expectedReturn, actualReturn);
        }

        [TestMethod]
        public void ThrowArgumentException_WhenNullCommentIsReturned()
        {
            //Arrange
            gettersMock.Setup(x => x.GetWorkItem(It.IsAny<IBoard>(), validWorkItemTitle)).Returns(workItemMock.Object);

            this.componentsFactoryMock.Setup(x => x.CreateComment(this.validMessage, It.IsAny<IPerson>()));

            var sut = new CreateCommentCommand(this.historyEventWriterMock.Object, this.componentsFactoryMock.Object, this.gettersMock.Object);
            var parameters = new List<string>()
            {
                this.validTeamName,
                this.validBoardName,
                this.validWorkItemTitle,
                this.validMessage,
                this.validAuthorName,
            };

            //Act, Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));
        }

    }
}
