using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.ListCommands;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Tests.CommandsTest.ListCommandsTest.ShowAllTeamBoardsCommandTest
{
    [TestClass]
    public class Execute_Should
    {
        [TestMethod]
        public void Throw_When_A_Passed_Team_Is_Null()
        {
            var validTeamName = "validTeam";
            ITeam team = null;
            var fakeGetters = new Mock<IGetters>();
            var fakeHistoryItemsCollection = new Mock<IHistoryItemsCollection>();
            var sut = new ShowAllTeamBoardsCommand(fakeGetters.Object, fakeHistoryItemsCollection.Object);
            var fakeList = new List<string>() { validTeamName };

            fakeGetters.Setup(x => x.GetTeam(validTeamName)).Returns(team);

            Assert.ThrowsException<ArgumentException>(() => sut.Execute(fakeList));

        }
    }
}
