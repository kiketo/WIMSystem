using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.AddCommands;
using WIMSystem.Commands.Contracts;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Tests.CommandsTest.AddCommandsTest.FakeClasses
{
    public class FakeAddBoardToTeamCommand : AddBoardToTeamCommand
    {//TODO
        public FakeAddBoardToTeamCommand(IHistoryEventWriter historyEventWriter, IGetters getters) : base(historyEventWriter, getters)
        {
        }

        //public IHistoryEventWriter HistoryEventWriter
        //{
        //    get
        //    {
        //        return base.historyEventWriter;
        //    }
        //    set
        //    {
        //        base.historyEventWriter = value;
        //    }
        //}
    }
}
