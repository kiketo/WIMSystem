using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIMSystem.Commands.ChangeCommands;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Models;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Tests.Commands.ChangeCommands.AssignWorkItemMemberCommandTests
{
    [TestClass]
    public class Execute_Should
    {
        private string validTeamName = "ValidTeam";
        private string validBoardName = "ValidB";
        private string validWorkItemTitle = "ValidTitle";
        private string validPersonName = "ValidName";
        private Mock<IPerson> personMock = new Mock<IPerson>();
        private Mock<IBoard> boardMock = new Mock<IBoard>();
        private Mock<IAssignableWorkItem> workItemMock = new Mock<IAssignableWorkItem>();
        private Mock<IHistoryEventWriter> historyEventWriterMock = new Mock<IHistoryEventWriter>();
        private Mock<IGetters> gettersMock = new Mock<IGetters>();

        [TestMethod]
        public void ExecuteAllFiveMethodsOnce_WhenValidParametersArePassed()
        {
            //Arrange
            this.gettersMock.Setup(x => x.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            this.gettersMock.Setup(x => x.GetAssignableWorkItem(boardMock.Object, this.validWorkItemTitle)).Returns(this.workItemMock.Object);
            this.gettersMock.Setup(x => x.GetPerson(this.validPersonName)).Returns(this.personMock.Object);

            this.personMock.SetupGet(x => x.PersonName).Returns(this.validPersonName);
            this.personMock.SetupGet(x => x.IsAssignedToTeam).Returns(true);
            this.personMock.SetupGet(x => x.MemberWorkItems).Returns(new List<IWorkItem>());

            var sut = new AssignWorkItemToMemberCommand(this.historyEventWriterMock.Object, this.gettersMock.Object);
            var parameters = new List<string>()
            {
                this.validTeamName,
                this.validBoardName,
                this.validWorkItemTitle,
                this.validPersonName,
            };

            //Act
            var returnMessage = sut.Execute(parameters);

            //Assert
            this.gettersMock.Verify(x => x.GetBoard(this.validTeamName, this.validBoardName), Times.Once);
            this.gettersMock.Verify(x => x.GetPerson(this.validPersonName), Times.Once);
            this.gettersMock.Verify(x => x.GetAssignableWorkItem(boardMock.Object, this.validWorkItemTitle), Times.Once);
            this.workItemMock.Verify(x => x.AssignMember(this.personMock.Object), Times.Once);
            this.historyEventWriterMock.
                Verify(x => x.AddHistoryEvent(
                    returnMessage,
                    personMock.Object,
                    It.IsAny<IBoard>(),
                    It.IsAny<ITeam>(),
                    workItemMock.Object
                    ), Times.Once);
        }

        [TestMethod]
        public void ReturnCorrectMessage_WhenCorrectParametrsArePassed()
        {
            //Arrange
            this.gettersMock.Setup(x => x.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            this.gettersMock.Setup(x => x.GetAssignableWorkItem(boardMock.Object, this.validWorkItemTitle)).Returns(this.workItemMock.Object);
            this.gettersMock.Setup(x => x.GetPerson(this.validPersonName)).Returns(this.personMock.Object);

            this.personMock.SetupGet(x => x.PersonName).Returns(this.validPersonName);
            this.personMock.SetupGet(x => x.IsAssignedToTeam).Returns(true);
            this.personMock.SetupGet(x => x.MemberWorkItems).Returns(new List<IWorkItem>());

            this.workItemMock.SetupGet(x => x.Title).Returns(validWorkItemTitle);

            var sut = new AssignWorkItemToMemberCommand(this.historyEventWriterMock.Object, this.gettersMock.Object);
            var parameters = new List<string>()
            {
                this.validTeamName,
                this.validBoardName,
                this.validWorkItemTitle,
                this.validPersonName,
            };

            var expectedReturn = string.Format(CommandsConsts.WorkItemAssigned, validWorkItemTitle, validPersonName);

            //Act
            var actualReturn = sut.Execute(parameters);

            //Assert
            Assert.AreEqual(expectedReturn, actualReturn);
        }

        [TestMethod]
        public void AddWorkItemToMember_WhenCorrectParametrsArePassed()
        {
            //Arrange
            this.gettersMock.Setup(x => x.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            this.gettersMock.Setup(x => x.GetAssignableWorkItem(boardMock.Object, this.validWorkItemTitle)).Returns(this.workItemMock.Object);
            this.gettersMock.Setup(x => x.GetPerson(this.validPersonName)).Returns(this.personMock.Object);

            this.personMock.SetupGet(x => x.PersonName).Returns(this.validPersonName);
            this.personMock.SetupGet(x => x.IsAssignedToTeam).Returns(true);
            this.personMock.SetupGet(x => x.MemberWorkItems).Returns(new List<IWorkItem>());

            this.workItemMock.SetupGet(x => x.Title).Returns(validWorkItemTitle);

            var sut = new AssignWorkItemToMemberCommand(this.historyEventWriterMock.Object, this.gettersMock.Object);
            var parameters = new List<string>()
            {
                this.validTeamName,
                this.validBoardName,
                this.validWorkItemTitle,
                this.validPersonName,
            };

            //Act
            sut.Execute(parameters);

            //Assert
            Assert.AreEqual(workItemMock.Object, personMock.Object.MemberWorkItems.Single());
        }

        [TestMethod]
        public void ThrowArgumentException_WhenNullPersonIsReturned()
        {
            //Arrange
            this.gettersMock.Setup(x => x.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            this.gettersMock.Setup(x => x.GetAssignableWorkItem(boardMock.Object, this.validWorkItemTitle)).Returns(this.workItemMock.Object);

            this.personMock.SetupGet(x => x.PersonName).Returns(this.validPersonName);
            this.personMock.SetupGet(x => x.IsAssignedToTeam).Returns(true);
            this.personMock.SetupGet(x => x.MemberWorkItems).Returns(new List<IWorkItem>());

            var sut = new AssignWorkItemToMemberCommand(this.historyEventWriterMock.Object, this.gettersMock.Object);
            var parameters = new List<string>()
            {
                this.validTeamName,
                this.validBoardName,
                this.validWorkItemTitle,
                this.validPersonName,
            };
            string expectedExceptionMessage = string.Format(CommandsConsts.NULL_OBJECT, nameof(Person));

            //Act, Assert
            Assert.AreEqual(expectedExceptionMessage,Assert.ThrowsException<ArgumentException>
                (() => sut.Execute(parameters)).Message);
        }

        [TestMethod]
        public void ThrowArgumentException_WhenNullWorkItemPersonIsReturned()
        {
            //Arrange

            this.gettersMock.Setup(x => x.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            this.gettersMock.Setup(x => x.GetPerson(this.validPersonName)).Returns(this.personMock.Object);

            this.personMock.SetupGet(x => x.PersonName).Returns(this.validPersonName);
            this.personMock.SetupGet(x => x.IsAssignedToTeam).Returns(true);
            this.personMock.SetupGet(x => x.MemberWorkItems).Returns(new List<IWorkItem>());

            var sut = new AssignWorkItemToMemberCommand(this.historyEventWriterMock.Object, this.gettersMock.Object);
            var parameters = new List<string>()
            {
                this.validTeamName,
                this.validBoardName,
                this.validWorkItemTitle,
                this.validPersonName,
            };
            string expectedExceptionMessage = string.Format(CommandsConsts.NULL_OBJECT, nameof(WorkItem));

            //Act, Assert
            Assert.AreEqual(
                expectedExceptionMessage,
                Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters)).Message
                );
        }

        [TestMethod]
        public void ThrowArgumentException_WhenPersonIsNotAssignedToTeam()
        {
            //Arrange

            this.gettersMock.Setup(x => x.GetBoard(validTeamName, validBoardName)).Returns(boardMock.Object);
            this.gettersMock.Setup(x => x.GetAssignableWorkItem(It.IsAny<IBoard>(), this.validWorkItemTitle)).Returns(this.workItemMock.Object);
            this.gettersMock.Setup(x => x.GetPerson(this.validPersonName)).Returns(this.personMock.Object);

            this.personMock.SetupGet(x => x.PersonName).Returns(this.validPersonName);
            this.personMock.SetupGet(x => x.IsAssignedToTeam).Returns(false);
            this.personMock.SetupGet(x => x.MemberWorkItems).Returns(new List<IWorkItem>());

            var sut = new AssignWorkItemToMemberCommand(this.historyEventWriterMock.Object, this.gettersMock.Object);
            var parameters = new List<string>()
            {
                this.validTeamName,
                this.validBoardName,
                this.validWorkItemTitle,
                this.validPersonName,
            };
            string expectedExceptionMessage = string.Format(CommandsConsts.NotMemberOfAnyTeam, validPersonName);

            //Act, Assert
            Assert.AreEqual(
                expectedExceptionMessage,
                Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters)).Message
                );
        }
    }
}
