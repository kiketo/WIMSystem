using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Utils;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;
using WIMSystem.Tests.Commands.Utils.FakeClasses;

namespace WIMSystem.Tests.Commands.Utils.GettersTest
{
    [TestClass]
    public class GetPerson_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_PersonDontExist()
        {
            //Arrange
            var personListMock = new Mock<IPersonsCollection>();
            var wIMTeamMock = new Mock<IWIMTeams>();
            var sut = new Getters(personListMock.Object, wIMTeamMock.Object);
            //Act&&Assert
            var ex =Assert.ThrowsException< ArgumentNullException >(()=> sut.GetPerson("Some Member"));
            Assert.AreEqual(string.Format(CommandsConsts.NoPersonFound,"Some Member"),ex.ParamName);
        }
        [TestMethod]
        public void CorrectlyPassData()
        {            
            //Arrange 
            var wIMTeamMock = new Mock<IWIMTeams>();
            var personListMock = new Mock<IPersonsCollection>();
            personListMock.Setup(x => x.Contains("Pesho")).Returns(true);
            personListMock.SetupGet(p => p["Pesho"]).Returns(new Mock<IPerson>().Object);
            var sut = new Getters(personListMock.Object, wIMTeamMock.Object);
            //Act
            var getPerson=sut.GetPerson("Pesho");
            //Assert
            personListMock.Verify(x => x.Contains("Pesho"), Times.Once);
            personListMock.Verify(p => p["Pesho"], Times.Once);                    
        }        
    }
}
