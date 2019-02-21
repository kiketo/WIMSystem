using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.AddCommands;
using WIMSystem.Commands.Contracts;

namespace WIMSystem.Tests.CommandsTest.AddCommandsTest.FakeClasses
{
    public class FakeAddPersonToTeamCommand : AddPersonToTeamCommand
    {
        public FakeAddPersonToTeamCommand(IGetters getter, IHistoryEventWriter historyEventWriter) : base(getter, historyEventWriter)
        {
        }
        public IGetters Getter { get { return base.getter; }}
        public IHistoryEventWriter HistoryEventWriter { get { return base.historyEventWriter; } }
    }
}
