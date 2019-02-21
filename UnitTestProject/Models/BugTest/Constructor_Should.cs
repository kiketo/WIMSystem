using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Tests.Models.BugTest
{
    [TestClass]
    public class Constructor_Should //string title, string description, IList<string> stepsToReproduce,
        //PriorityType priority, BugSeverityType severity, IBoard board, IPerson assignee = null
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_Passed_StepsToReproduce_Is_Null()
        {
            //TODO:
        }
    }
}
