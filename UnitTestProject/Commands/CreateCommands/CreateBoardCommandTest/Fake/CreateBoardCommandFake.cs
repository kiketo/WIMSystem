using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.CreateCommands;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Tests.Commands.CreateCommands.CreateBoardCommandTest.Fake
{
    class CreateBoardCommandFake : CreateBoardCommand
    {
        public CreateBoardCommandFake(IHistoryEventWriter historyEventWriter, IComponentsFactory componentsFactory, IGetters getter) : base(historyEventWriter, componentsFactory, getter)
        {
        }

        public int Count { get; set; } = 0;

        public new string Execute(string boardName, ITeam teamToAddTo)
        {
            Count++;
            return "";
        }

    }
}
