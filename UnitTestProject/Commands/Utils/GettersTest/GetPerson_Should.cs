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
        public void Throw_ArgumentNullException_When_MemberDontExist()
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
        public void CorectrlyPassData()//TODO I need help here
        {
            /*
            Според мен:
            Трябва да провериш дали метода GetPerson извиква 
            1. personList.Contains
            2. Индексатора (индексатор се тества по следния начин: personListMock.Verify(x => x["Pesho"], Times.Once);)
            
            Не мисля, че трябва да проверяваш дали връщаното е нашия обект, защото за това се грижи нашия индексатор в класа
            personList т.е. като (ако изобщо) му правим unit test-ове ще проверяваме дали работи правилно този индексатор в
            този клас. В нашия метод GetPerson би следвало да се извикват тези методи, а вече си е тяхно задължение да работят
            правилно.
            */
            //Arrange 
            var wIMTeamMock = new Mock<IWIMTeams>();
            var personListMock = new Mock<IPersonsCollection>();
            personListMock.Setup(x => x.Contains("Pesho")).Returns(true);
            var personMock = new Mock<IPerson>();
            personMock.Setup(p => p.PersonName).Returns("Pesho");
                        
            personListMock.Setup(p => p.Persons).Returns(new Dictionary<string, IPerson> { { "Pesho", personMock.Object } });
            
            personListMock.Setup(p => p.Persons["Pesho"]).Returns(personMock.Object);
           
            var sut = new Getters(personListMock.Object, wIMTeamMock.Object);
            //Act
            var getPerson=sut.GetPerson("Pesho");
            //Assert
            Assert.AreEqual(personMock.Object, getPerson);
        }
    }
}
