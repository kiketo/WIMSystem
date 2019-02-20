using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.ListCommands;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Tests.CommandsTest.ListCommandsTest.ShowAllTeamBoardsCommandTest
{
    [TestClass]
    public class Execute_Should
    {
        [TestMethod]
        public void Throw_When_A_Passed_Team_Is_Null()
        {
            ITeam team = null;
            var fakeGetters = new Mock<IGetters>();
            var fakeHistoryItemsCollection = new Mock<IHistoryItemsCollection>();
            var sut = new ShowAllTeamBoardsCommand(fakeGetters.Object, fakeHistoryItemsCollection.Object);
            var fakeList = new List<string>();

            sut.Execute(fakeList);



        }
    }
}
