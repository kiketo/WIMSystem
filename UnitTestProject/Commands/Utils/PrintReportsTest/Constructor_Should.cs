using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using WIMSystem.Commands.Utils;
using WIMSystem.Core.Contracts;

namespace WIMSystem.Tests.Commands.Utils.PrintReportsTest
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_WhenWriterIsNull()
        {
            //Arrange
            IWriter writer=null;
            //Act&&Assert
            var ex = Assert.ThrowsException<ArgumentNullException>(() => new PrintReports(writer));
            Assert.AreEqual(string.Format(CommandsConsts.NULL_OBJECT, nameof(writer)), ex.ParamName);
        }
        [TestMethod]
        public void CorrectlyCreateNewListOfReports()
        {
            //Arrange
            var writer = new Mock<IWriter>();
            //Act
            var sut = new PrintReports(writer.Object);
            //Assert
            Assert.IsInstanceOfType(sut.Reports, typeof(List<string>));
        }
    }
    
}
