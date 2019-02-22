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

namespace WIMSystem.Tests.Commands.CreateCommands.CreatePersonCommandTests
{
    [TestClass]
    public class Execute_Should
    {
        string validPersonName = "ValidName";
        private Mock<IPerson> personMock = new Mock<IPerson>();
        private Mock<IHistoryEventWriter> historyEventWriterMock = new Mock<IHistoryEventWriter>();
        private Mock<IPersonsCollection> personsCollectionMock = new Mock<IPersonsCollection>();
        private Mock<IComponentsFactory> componentsFactoryMock = new Mock<IComponentsFactory>();

        [TestMethod]
        public void ExecuteAllThreeMethodsOnce_WhenValidParametersArePassed()
        {
            //Arrange
            this.componentsFactoryMock
                .Setup(x => x.CreatePerson(this.validPersonName))
                .Returns(this.personMock.Object);

            var sut = new CreatePersonCommand(this.historyEventWriterMock.Object, personsCollectionMock.Object,this.componentsFactoryMock.Object);
            var parameters = new List<string>()
            {
                this.validPersonName
            };

            //Act
            var returnMessage = sut.Execute(parameters);

            //Assert
            this.componentsFactoryMock.Verify(x => x.CreatePerson(this.validPersonName), Times.Once);
            this.personsCollectionMock.Verify(x => x.AddPerson(personMock.Object), Times.Once);
            this.historyEventWriterMock.
                Verify(x => x.AddHistoryEvent(
                    returnMessage,
                    personMock.Object,
                    null,
                    null,
                    null
                    ), Times.Once);
        }

        [TestMethod]
        public void ReturnCorrectMessage_WhenCorrectParametrsArePassed()
        {
            //Arrange
            this.componentsFactoryMock
                .Setup(x => x.CreatePerson(this.validPersonName))
                .Returns(this.personMock.Object);

            var sut = new CreatePersonCommand(this.historyEventWriterMock.Object, personsCollectionMock.Object, this.componentsFactoryMock.Object);
            var parameters = new List<string>()
            {
                this.validPersonName
            };

            var expectedReturn = string.Format(CommandsConsts.ObjectCreated, nameof(Person), validPersonName);

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
                .Setup(x => x.CreatePerson(this.validPersonName));

            var sut = new CreatePersonCommand(this.historyEventWriterMock.Object, personsCollectionMock.Object, this.componentsFactoryMock.Object);
            var parameters = new List<string>()
            {
                this.validPersonName
            };

            var expectedReturn = string.Format(CommandsConsts.ObjectCreated, nameof(Person), validPersonName);

            //Act, Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Execute(parameters));
        }
    }
}
