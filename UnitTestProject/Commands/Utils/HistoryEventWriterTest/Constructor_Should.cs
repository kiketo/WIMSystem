using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Utils;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Models.Contracts;
using WIMSystem.Tests.Commands.Utils.FakeClasses;

namespace WIMSystem.Tests.Commands.Utils.HistoryEventWriterTest
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_WhenHistoryItemsListIsNull()
        {
            //Assert var historyItemsList = new Mock<IHistoryItemsCollection>();
            IHistoryItemsCollection historyItemsList = null;
            var factoryMock = new Mock<IComponentsFactory>();
            //Act&&Assert
            var ex = Assert.ThrowsException<ArgumentNullException>(() => new HistoryEventWriter(historyItemsList, factoryMock.Object));
            Assert.AreEqual(string.Format(CommandsConsts.NULL_OBJECT, nameof(historyItemsList)), ex.ParamName);
        }
        [TestMethod]
        public void Throw_ArgumentNullException_WhenFactoryIsNull()
        {
            //Assert 
            var historyItemsListMock = new Mock<IHistoryItemsCollection>();
            IComponentsFactory factory = null;
            //Act&&Assert
            var ex = Assert.ThrowsException<ArgumentNullException>(() => new HistoryEventWriter(historyItemsListMock.Object, factory));
            Assert.AreEqual(string.Format(CommandsConsts.NULL_OBJECT, nameof(factory)), ex.ParamName);
        }
        [TestMethod]
        public void CorrectrlyAssignPassedData()
        {
            //Arrange
            var factoryMock = new Mock<IComponentsFactory>();
            var historyItemsListMock = new Mock<IHistoryItemsCollection>();
            //Act
            var sut = new FakeHistoryEventWriter(historyItemsListMock.Object, factoryMock.Object);
            //Assert
            Assert.AreEqual(factoryMock.Object, sut.Factory);
            Assert.AreEqual(historyItemsListMock.Object, sut.HistoryItemsList);
        }
    }
}
