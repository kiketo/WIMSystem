using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.AddCommands;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Tests.CommandsTest.AddCommandsTest.FakeClasses;

namespace WIMSystem.Tests.CommandsTest.AddCommandsTest.AddPersonToTeamCommandTest
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_When_A_Passed_Value_For_Getter_Is_Null()
        {
            //Arrange
            IGetters getter = null;
            var historyEventWriter = new Mock<IHistoryEventWriter>();

            //Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => new AddPersonToTeamCommand(getter, historyEventWriter.Object));

            //Assert
            Assert.AreEqual(string.Format(CommandsConsts.NULL_OBJECT, nameof(getter)), exception.ParamName);
        }

        [TestMethod]
        public void Throw_When_A_Passed_Value_For_HistoryEventWriter_Is_Null()
        {
            //Arrange            
            var getter = new Mock<IGetters>();
            IHistoryEventWriter historyEventWriter =null;

            //Act
            var exception = Assert.ThrowsException<ArgumentNullException>(() => new AddPersonToTeamCommand(getter.Object, historyEventWriter));

            //Assert
            Assert.AreEqual(string.Format(CommandsConsts.NULL_OBJECT, nameof(historyEventWriter)), exception.ParamName);
        }
        [TestMethod]
        public void Correctrly_Assign_Getter_And_HistoryEventWriter()
        {
            //Arrange
            var getter = new Mock<IGetters>();
            var historyEventWriter = new Mock<IHistoryEventWriter>();
            //Act
            var sut = new FakeAddPersonToTeamCommand(getter.Object, historyEventWriter.Object);
            //Assert
            Assert.AreEqual(getter.Object, sut.Getter);
            Assert.AreEqual(historyEventWriter.Object, sut.HistoryEventWriter);
        }
    }
}
