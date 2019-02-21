using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.ListCommands;
using WIMSystem.Commands.Utils;
using WIMSystem.Models.Contracts;
using WIMSystem.Tests.CommandsTest.ListCommandsTest.FakeClasses;

namespace WIMSystem.Tests.CommandsTest.ListCommandsTest.ShowAllTeamBoardsCommandTest
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_When_A_Passed_Getter_Is_Null()
        {
            IGetters getters = null;
            var fakeHistoryItemsCollection = new Mock<IHistoryItemsCollection>();
            var expectedMessage = string.Format(CommandsConsts.NULL_OBJECT,
                nameof(getters));

            var sut = Assert.ThrowsException<ArgumentNullException>(
                () => new ShowAllTeamBoardsCommand(getters, fakeHistoryItemsCollection.Object));

            Assert.AreEqual(expectedMessage, sut.ParamName);
        }

        [TestMethod]
        public void Throw_When_A_Passed_HistoryItemsCollection_Is_Null()
        {
            var fakeGetters = new Mock<IGetters>();
            IHistoryItemsCollection historyItemsCollection = null;
            var expectedMessage = string.Format(CommandsConsts.NULL_OBJECT,
                nameof(historyItemsCollection));

            var sut = Assert.ThrowsException<ArgumentNullException>(
                () => new ShowAllTeamBoardsCommand(fakeGetters.Object, historyItemsCollection));

            Assert.AreEqual(expectedMessage, sut.ParamName);
        }

        [TestMethod]
        public void Correctly_Assign_Getters_Value()
        {
            var fakeGetters = new Mock<IGetters>();
            var fakeHistoryItemsCollection = new Mock<IHistoryItemsCollection>();

            var sut = new FakeShowAllTeamBoardsCommand(fakeGetters.Object, fakeHistoryItemsCollection.Object);

            Assert.AreEqual(fakeGetters.Object, sut.Getters);
        }

        [TestMethod]
        public void Correctly_Assign_HistoryItemsCollection_Value()
        {
            var fakeGetters = new Mock<IGetters>();
            var fakeHistoryItemsCollection = new Mock<IHistoryItemsCollection>();

            var sut = new FakeShowAllTeamBoardsCommand(fakeGetters.Object, fakeHistoryItemsCollection.Object);

            Assert.AreEqual(fakeHistoryItemsCollection.Object, sut.HistoryItemsCollection);
        }
    }
}
