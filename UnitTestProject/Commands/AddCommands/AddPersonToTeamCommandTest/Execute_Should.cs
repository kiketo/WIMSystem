using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using WIMSystem.Commands.AddCommands;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Tests.Commands.AddCommands.AddPersonToTeamCommandTest
{
    [TestClass]
    public class Execute_Should
    {
        [TestMethod]
        public void CorrectlyPassData()
        {
            //Arrange
            var getterMock = new Mock<IGetters>();
            var historyEventWriterMock = new Mock<IHistoryEventWriter>();
            var personMock = new Mock<IPerson>();
            var teamMock = new Mock<ITeam>();
            teamMock.Setup(x => x.AddMemberToTeam(personMock.Object));
            teamMock.Setup(x => x.TeamName).Returns("TeamName");
            IList<string> parameters = new List<string>{
                { "Person" },
                {"TeamName" }
            };
            getterMock.Setup(x => x.GetPerson("Person")).Returns(personMock.Object);
            getterMock.Setup(x => x.GetTeam("TeamName")).Returns(teamMock.Object);
            var sut = new AddPersonToTeamCommand(getterMock.Object, historyEventWriterMock.Object);
            var expectedMessage = string.Format(CommandsConsts.ObjectAddedToTeam, nameof(personMock.Object), "Person", "TeamName");
            var boardMock = new Mock<IBoard>();
            historyEventWriterMock.Setup(x => x.AddHistoryEvent(It.IsAny<string>(), It.IsAny<IPerson>(), It.IsAny<IBoard>(), It.IsAny<ITeam>(), It.IsAny<IWorkItem>()));
            //Act
            var returnMessage = sut.Execute(parameters);
            //Assert
            teamMock.Verify(x => x.AddMemberToTeam(personMock.Object), Times.Once);
            historyEventWriterMock.Verify(x => x.AddHistoryEvent(It.IsAny<string>(), It.IsAny<IPerson>(), It.IsAny<IBoard>(), It.IsAny<ITeam>(), It.IsAny<IWorkItem>()), Times.Once);
            StringAssert.Equals(expectedMessage, returnMessage);
        }
    }
}
