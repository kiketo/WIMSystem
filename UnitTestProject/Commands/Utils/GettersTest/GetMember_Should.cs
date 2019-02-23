using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Utils;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Tests.Commands.Utils.GettersTest
{
    [TestClass]
    public class GetMember_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_TeamDontExist()
        {
            //Arrange
            var personListMock = new Mock<IPersonsCollection>();
            var wIMTeamMock = new Mock<IWIMTeams>();
            wIMTeamMock.Setup(w => w.TeamsList).Returns(new Mock<IDictionary<string,ITeam>>().Object);
            var teamMock = new Mock<ITeam>();
            teamMock.Setup(x => x.TeamName).Returns("TeamName");
            var sut = new Getters(personListMock.Object, wIMTeamMock.Object);
            //Act&&Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => sut.GetMember(teamMock.Object,"Pesho"));
            Assert.AreEqual(string.Format(CommandsConsts.NoTeamFound,teamMock.Object.TeamName), ex.Message);
        }
       // [TestMethod]
       // public void CorrectlyPassData()
       // {
       //     //Arrange 
       //     var wIMTeamMock = new Mock<IWIMTeams>();
       //     wIMTeamMock.Setup(x => x.TeamsList.ContainsKey("TeamName")).Returns(true);
       //     var personListMock = new Mock<IPersonsCollection>();
       //     var personMock = new Mock<IPerson>();
       //     var teamMock = new Mock<ITeam>();
       //     teamMock.Setup(x => x.TeamName).Returns("TeamName");
       //     teamMock.Setup(x => x.MemberList.Contains(personMock.Object)).Returns(true);
       //     var sut = new Getters(personListMock.Object, wIMTeamMock.Object);            
       //     //Act
       //     sut.GetMember(teamMock.Object, "Member");
       //     //Assert
       //     wIMTeamMock.Verify(x => x.TeamsList.ContainsKey("TeamName"), Times.Once);
       //     teamMock.Verify(x => x.MemberList.Contains(personMock.Object), Times.Once);
       //
       // }
    }
}
