using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using WIMSystem.Commands.Utils;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;
using WIMSystem.Tests.Commands.Utils.FakeClasses;

namespace WIMSystem.Tests.Commands.Utils.GettersTest
{
    [TestClass]
    public class Constructor_Should
    {
        private IWIMTeams wIMTeams;

        [TestMethod]
        public void ThrowArgumentNullException_WhenNullPersonsCollectionIsPassed()
        {
            //Arrange
            IPersonsCollection personList = null;
            var wIMTeams = new Mock<IWIMTeams>();

            //Act&Assert
            var ex=Assert.ThrowsException<ArgumentNullException>(() => new Getters(personList, wIMTeams.Object));
            Assert.AreEqual(string.Format(CommandsConsts.NULL_OBJECT,nameof(personList)),ex.ParamName);
        }
        [TestMethod]
        public void ThrowArgumentNullException_WhenNullWIMTeamsIsPassed()
        {
            //Arrange
            var personList = new Mock<IPersonsCollection>();
            IWIMTeams wimTeams = null;

            //Act&Assert
            var ex = Assert.ThrowsException<ArgumentNullException>(() => new Getters(personList.Object, wIMTeams));
            Assert.AreEqual(string.Format(CommandsConsts.NULL_OBJECT, nameof(wimTeams)), ex.ParamName);
        }
        [TestMethod]
        public void CorrectrlyAssignPassedData()
        {
            //Arrange
            var personListMock = new Mock<IPersonsCollection>();
            var wIMTeamMock = new Mock<IWIMTeams>();
            //Act
            var sut = new FakeGetters(personListMock.Object, wIMTeamMock.Object);
            //Assert
            Assert.AreEqual(personListMock.Object, sut.PersonList);
            Assert.AreEqual(wIMTeamMock.Object, sut.WIMTeams);
        }
    }
}
