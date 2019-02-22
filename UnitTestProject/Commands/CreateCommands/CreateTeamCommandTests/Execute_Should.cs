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

namespace WIMSystem.Tests.Commands.CreateCommands.CreateTeamCommandTests
{
    [TestClass]
    public class Execute_Should
    {
        string validTeamName = "ValidName";
        private Mock<ITeam> teamMock = new Mock<ITeam>();
        private Mock<IHistoryEventWriter> historyEventWriterMock = new Mock<IHistoryEventWriter>();
        private Mock<IWIMTeams> personsCollectionMock = new Mock<IWIMTeams>();
        private Mock<IComponentsFactory> componentsFactoryMock = new Mock<IComponentsFactory>();

        [TestMethod]
        public void ExecuteAllThreeMethodsOnce_WhenValidParametersArePassed()
        {
            //Arrange
            this.componentsFactoryMock
                .Setup(x => x.CreateTeam(this.validTeamName))
                .Returns(this.teamMock.Object);

            var sut = new CreateTeamCommand(this.historyEventWriterMock.Object, personsCollectionMock.Object, this.componentsFactoryMock.Object);
            var parameters = new List<string>()
            {
                this.validTeamName
            };

            //Act
            var returnMessage = sut.Execute(parameters);

            //Assert
            this.componentsFactoryMock.Verify(x => x.CreateTeam(this.validTeamName), Times.Once);
            this.personsCollectionMock.Verify(x => x.AddTeam(teamMock.Object), Times.Once);
            this.historyEventWriterMock.
                Verify(x => x.AddHistoryEvent(
                    returnMessage,
                    null,
                    null,
                    teamMock.Object,
                    null
                    ), Times.Once);

        }

        [TestMethod]
        public void ReturnCorrectMessage_WhenCorrectParametrsArePassed()
        {
            //Arrange
            this.componentsFactoryMock
                .Setup(x => x.CreateTeam(this.validTeamName))
                .Returns(this.teamMock.Object);

            var sut = new CreateTeamCommand(this.historyEventWriterMock.Object, personsCollectionMock.Object, this.componentsFactoryMock.Object);
            var parameters = new List<string>()
            {
                this.validTeamName
            };

            var expectedReturn = string.Format(CommandsConsts.ObjectCreated, nameof(Team), validTeamName);

            //Act
            var actualReturn = sut.Execute(parameters);

            //Assert
            Assert.AreEqual(expectedReturn, actualReturn);
        }

        [TestMethod]
        public void ThrowArgumentException_WhenNullPersonIsReturned()
        {
            //Arrange
            this.componentsFactoryMock
                .Setup(x => x.CreateTeam(this.validTeamName));

            var sut = new CreateTeamCommand(
                this.historyEventWriterMock.Object,
                personsCollectionMock.Object,
                this.componentsFactoryMock.Object);

            var parameters = new List<string>()
            {
                this.validTeamName
            };

            //Act, Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));
        }
    }
}
