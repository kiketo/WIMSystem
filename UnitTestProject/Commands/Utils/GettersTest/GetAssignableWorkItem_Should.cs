using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using WIMSystem.Commands.Utils;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Tests.Commands.Utils.GettersTest
{
    [TestClass]
    public class GetAssignableWorkItem_Should
    {
        [TestMethod]
        public void Throw_ArgumentException_When_WorkItemIsNotAssignable()
        {
            //Arrange           
            var personListMock = new Mock<IPersonsCollection>();
            var wimTeamMock = new Mock<IWIMTeams>();
            var workItemMock = new Mock<IWorkItem>();
            workItemMock.Setup(x => x.Title).Returns("WorkItem");
            var boardMock = new Mock<IBoard>();
            boardMock.Setup(x => x.BoardWorkItems).Returns(
                new Dictionary<string, IWorkItem>{
                    { "WorkItem", workItemMock.Object }
                });
            boardMock.Setup(x => x.BoardWorkItems["WorkItem"]).Returns(workItemMock.Object);

            var sut = new Getters(personListMock.Object, wimTeamMock.Object);
            //Act&&Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => sut.GetAssignableWorkItem(boardMock.Object, workItemMock.Object.Title));
            Assert.AreEqual(string.Format($"{boardMock.Object.BoardWorkItems["WorkItem"].GetType().Name} is not assignable work item!"), ex.Message);
        }

        [TestMethod]
        public void CorrectlyPassData()
        {
            //Arrange 
            var personListMock = new Mock<IPersonsCollection>();
            var wimTeamMock = new Mock<IWIMTeams>();
            var workItemMock = new Mock<IAssignableWorkItem>();
            workItemMock.Setup(x => x.Title).Returns("WorkItem");
            var boardMock = new Mock<IBoard>();
            boardMock.Setup(x => x.BoardWorkItems).Returns(
                new Dictionary<string, IWorkItem>{
                    { "WorkItem", workItemMock.Object }
                });
            boardMock.Setup(x => x.BoardWorkItems["WorkItem"]).Returns(workItemMock.Object);

            var sut = new Getters(personListMock.Object, wimTeamMock.Object);

            //Act
            var GetWorkItem = sut.GetAssignableWorkItem(boardMock.Object, "WorkItem");
            //Assert
            Assert.AreEqual(workItemMock.Object, GetWorkItem);
        }
    }
}
