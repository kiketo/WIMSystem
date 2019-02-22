using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Utils;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Tests.Commands.Utils.HistoryEventWriterTest
{
    [TestClass]
    public class AddHistoryEvent_Should
    {
        [TestMethod]
        public void Throw_ExceptionIfDescriptionIsNull()
        {
            //Arrange
            string description = null;
            var factoryMock = new Mock<IComponentsFactory>();
            var historyItemsListMock = new Mock<IHistoryItemsCollection>();

            var sut = new HistoryEventWriter(historyItemsListMock.Object, factoryMock.Object);
            //Act
            var ex=Assert.ThrowsException<ArgumentNullException>(()=>sut.AddHistoryEvent(description));
            Assert.AreEqual(string.Format(CommandsConsts.NULL_OBJECT, nameof(description)), ex.ParamName);
        }
        [TestMethod]
        public void CorectrlyPassData()
        {
            //Arrange
            var factoryMock = new Mock<IComponentsFactory>();
            string description = "message";
            var memberMock = new Mock<IPerson>();
            var boardMock = new Mock<IBoard>();
            var teamMock = new Mock<ITeam>();
            var workItemMock = new Mock<IWorkItem>();
           var historyItemMock = new Mock<IHistoryItem>();
            factoryMock.Setup(x => x.CreateHistoryItem(description, memberMock.Object, boardMock.Object, teamMock.Object, workItemMock.Object)).Returns(historyItemMock.Object);
            var historyItemsListMock = new Mock<IHistoryItemsCollection>();
            historyItemsListMock.Setup(x => x.AddHistoryItem(historyItemMock.Object));
           var sut= new HistoryEventWriter(historyItemsListMock.Object, factoryMock.Object);
            //Act
            sut.AddHistoryEvent(description, memberMock.Object, boardMock.Object, teamMock.Object, workItemMock.Object);
            //Assert
            factoryMock.Verify(x => x.CreateHistoryItem(description, memberMock.Object, boardMock.Object, teamMock.Object, workItemMock.Object), Times.Once);
            historyItemsListMock.Verify(x => x.AddHistoryItem(historyItemMock.Object), Times.Once);

        }
    }
}
