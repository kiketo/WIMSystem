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
    public class GetWorkItem_Should
    {
        [TestMethod]
        public void Throw_ArgumentException_When_WorkItemDontExistInTheBoard()
        {
            //Arrange           
            var personListMock = new Mock<IPersonsCollection>();
            var wimTeamMock = new Mock<IWIMTeams>();
            var boardMock = new Mock<IBoard>();
            boardMock.Setup(x => x.BoardWorkItems).Returns(new Dictionary<string, IWorkItem>());
            var workItem = "WorkItem";
            var sut = new Getters(personListMock.Object, wimTeamMock.Object);
            //Act&&Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => sut.GetWorkItem(boardMock.Object, workItem));
            Assert.AreEqual(string.Format(CommandsConsts.NoWorkItemFound, workItem), ex.Message);
        }

        [TestMethod]
        public void CorrectlyPassData()
        {
            //Arrange 
            var personListMock = new Mock<IPersonsCollection>();
            var wimTeamMock = new Mock<IWIMTeams>();
            var boardMock = new Mock<IBoard>();
            var workItem = "WorkItem";
            var workItemMock = new Mock<IWorkItem>();
            boardMock.Setup(x => x.BoardWorkItems[workItem]).Returns(workItemMock.Object);
            
            boardMock.Setup(x => x.BoardWorkItems.ContainsKey(workItem)).Returns(true);
            var sut = new Getters(personListMock.Object, wimTeamMock.Object);

            //Act
            var GetWorkItem = sut.GetWorkItem(boardMock.Object, workItem);
            //Assert
            Assert.AreEqual(workItemMock.Object, GetWorkItem);
        }
    }
}
