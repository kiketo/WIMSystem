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
    public class GetTeam_Should
    {
        [TestMethod]
        public void Throw_ArgumentException_When_ТеамDontExist()
        {
            //Arrange           
            var personListMock = new Mock<IPersonsCollection>();
            var wimTeamMock = new Mock<IWIMTeams>();
            var teamName = "TeamName";
            wimTeamMock.Setup(x => x.TeamsList.ContainsKey(teamName)).Returns(false);
            var sut = new Getters(personListMock.Object, wimTeamMock.Object);
            //Act&&Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => sut.GetTeam(teamName));
            Assert.AreEqual(string.Format(CommandsConsts.NoTeamFound, teamName), ex.Message);
        }
        [TestMethod]
        public void CorrectlyPassData()
        {
            //Arrange 

            var personListMock = new Mock<IPersonsCollection>();
            var wIMTeamMock = new Mock<IWIMTeams>();
            var teamMock = new Mock<ITeam>();
            wIMTeamMock.Setup(x => x.TeamsList);
            var teamName = "TeamName";
            teamMock.Setup(x => x.TeamName).Returns(teamName);
            wIMTeamMock.Setup(x => x[teamName]).Returns(teamMock.Object);
            wIMTeamMock.Setup(x => x.TeamsList.ContainsKey(teamName)).Returns(true);
            var sut = new Getters(personListMock.Object, wIMTeamMock.Object);

            //Act
            var getTeam = sut.GetTeam(teamName);
            //Assert
            Assert.AreEqual(teamMock.Object, getTeam);
        }
    }
}
