//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using WIMSystem.Commands.Contracts;
//using WIMSystem.Core;
//using WIMSystem.Core.Contracts;
//using WIMSystem.Core.Factories.Contracts;
//using WIMSystem.Menu.Contracts;

//namespace WIMSystem.Tests.Core.EngineTests
//{
//    [TestClass]
//    public class Start_Should
//    {
//        Mock<ICommandsFactory> commandsFactoryMock = new Mock<ICommandsFactory>();
//        Mock<IPrintReports> printReporstMock = new Mock<IPrintReports>();
//        Mock<IMainMenu> mainMenuMock = new Mock<IMainMenu>();
//        Mock<ICommandParser> parserMock = new Mock<ICommandParser>();
//        Mock<IReader> readerMock = new Mock<IReader>();

//        [TestMethod]
//        public void CallThreeMainMenuMethods_WhenShowLogoIsTrueAndInputTypeChooserReturnNull()
//        {
//            var sut = new Engine(
//                commandsFactoryMock.Object,
//                printReporstMock.Object,
//                mainMenuMock.Object,
//                parserMock.Object,
//                readerMock.Object
//                );
//            //Act
//            sut.Start(true);

//            //Assert
//            mainMenuMock.Verify(x => x.ShowLogo(), Times.Once);
//            mainMenuMock.Verify(x => x.InputTypeChooser(), Times.Once);
//            mainMenuMock.Verify(x => x.ShowCredits(), Times.Once);
//        }

//        [TestMethod]
//        public void CallTwoMainMenuMethods_WhenShowLogoIsFalseAndInputTypeChooserReturnNull()
//        {
//            var sut = new Engine(
//                commandsFactoryMock.Object,
//                printReporstMock.Object,
//                mainMenuMock.Object,
//                parserMock.Object,
//                readerMock.Object
//                );
//            //Act
//            sut.Start(false);

//            //Assert
//            mainMenuMock.Verify(x => x.InputTypeChooser(), Times.Once);
//            mainMenuMock.Verify(x => x.ShowCredits(), Times.Once);
//        }

//        [TestMethod]
//        public void CallProperMethods_WhenShowLogoIsTrueAndInputTypeChooserReturnReader()
//        {
               
//            var sut = new Engine(
//                commandsFactoryMock.Object,
//                printReporstMock.Object,
//                mainMenuMock.Object,
//                parserMock.Object,
//                readerMock.Object
//                );
//            //Act
//            sut.Start(true);

//            //Assert
//            mainMenuMock.Verify(x => x.ShowLogo(), Times.Once);
//            mainMenuMock.Verify(x => x.InputTypeChooser(), Times.Once);
//            mainMenuMock.Verify(x => x.ShowCredits(), Times.Once);
//        }
//    }
//}
