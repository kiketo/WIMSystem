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
    public class GetBoard_Should
    {
        [TestMethod]
        public void Throw_ArgumentException_When_ТеамDontExist()
        {
            //Arrange           
            var personListMock = new Mock<IPersonsCollection>();
            var wimTeamMock = new Mock<IWIMTeams>();
            var teamName = "TeamName";
            var boardName = "BoardName";
            wimTeamMock.Setup(x => x.TeamsList).Returns(new Dictionary<string,ITeam>());
            var sut = new Getters(personListMock.Object, wimTeamMock.Object);
            //Act&&Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => sut.GetBoard(teamName,boardName));
            Assert.AreEqual(string.Format(CommandsConsts.NoTeamFound, teamName), ex.Message);
        }
        [TestMethod]
        public void Throw_ArgumentException_When_BoardDontExist()
        {
            //Arrange           
            var personListMock = new Mock<IPersonsCollection>();
            var wimTeamMock = new Mock<IWIMTeams>();
            var teamName = "TeamName";
            wimTeamMock.Setup(x => x.TeamsList.ContainsKey(teamName)).Returns(true);
            var boardName = "BoardName";
            wimTeamMock.Setup(x => x.TeamsList[teamName].BoardList).Returns(new Dictionary<string, IBoard>());

            var sut = new Getters(personListMock.Object, wimTeamMock.Object);
            //Act&&Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => sut.GetBoard(teamName, boardName));
            Assert.AreEqual(string.Format(CommandsConsts.NoBoardFound, boardName), ex.Message);
        }

        [TestMethod]
        public void CorrectlyPassData()
        {
            //Arrange 
            var personListMock = new Mock<IPersonsCollection>();
            var wimTeamMock = new Mock<IWIMTeams>();
            var teamName = "TeamName";
            var teamMock = new Mock<ITeam>();

            wimTeamMock.Setup(x => x.TeamsList.ContainsKey(teamName)).Returns(true);
            var boardName = "BoardName";
            wimTeamMock.Setup(x => x.TeamsList[teamName].BoardList.ContainsKey(boardName)).Returns(true);
            var boardMock = new Mock<IBoard>();
            boardMock.Setup(x => x.BoardName).Returns(boardName);
            wimTeamMock.Setup(x => x[teamName]).Returns(teamMock.Object);
            teamMock.Setup(x => x.BoardList[boardName]).Returns(boardMock.Object);
            var sut = new Getters(personListMock.Object, wimTeamMock.Object);

            //Act
            var getBoard = sut.GetBoard(teamName,boardName);
            //Assert
            Assert.AreEqual(boardMock.Object, getBoard);
        }
    }
}
