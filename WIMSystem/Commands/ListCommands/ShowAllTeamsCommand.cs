using System;
using System.Collections.Generic;
using Utils;
using WIMSystem.Commands.Contracts;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Commands.ListCommands
{
    public class ShowAllTeamsCommand : IEngineCommand
    {
        private IWIMTeams wIMTeams;

        public ShowAllTeamsCommand(IWIMTeams wIMTeams)
        {
            this.wIMTeams = wIMTeams ?? throw new ArgumentNullException(nameof(wIMTeams));
        }

        public string ReadSingleCommand(IList<string> parameters)
        {            
            return this.Execute();
        }

        private string Execute()
        {
            var returnMessage = this.wIMTeams.ShowAllTeams();
            return returnMessage;
        }
    }
}
