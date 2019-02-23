using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Utils;
using WIMSystem.Core.Contracts;

namespace WIMSystem.Tests.Commands.Utils.PrintReportsTest
{
   // [TestClass]
   // public class Print_Should
   // {
   //     [TestMethod]
   //     public void CorrectlyPrintOut()
   //     {
   //         //Arrange
   //         var reportsMock = new Mock<IList<string>>();
   //         reportsMock.Setup(x => x.Count).Returns(1);
   //         reportsMock.Setup(x => x.Clear());
   //
   //         var writerMock = new Mock<IWriter>();
   //         writerMock.Setup(x => x.Write("message"));
   //         var sut = new PrintReports(writerMock.Object);
   //         sut.Reports.Add("message");
   //
   //         //Act
   //         sut.Print();
   //         //Assert
   //         reportsMock.Verify(x => x.Clear(), Times.Once);
   //         writerMock.Verify(x => x.Write("message"), Times.Once);
   //
   //
   //     }            
   // }
}
