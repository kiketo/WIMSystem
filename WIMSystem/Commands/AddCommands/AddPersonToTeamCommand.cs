using System.Collections.Generic;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Commands.AddCommands
{
    public class AddPersonToTeamCommand : IEngineCommand
    {
        private readonly IGetters getter;
        private readonly IHistoryEventWriter historyEventWriter;
  
        public AddPersonToTeamCommand(IGetters getter, IHistoryEventWriter historyEventWriter)
        {
            this.getter = getter;
            this.historyEventWriter = historyEventWriter;
        }

        public string ReadSingleCommand(IList<string> parameters)
        {
            var memberForAdding = this.getter.GetPerson(parameters[0]);
            var teamToAddTo = this.getter.GetTeam(parameters[1]);

            return this.Execute(memberForAdding, teamToAddTo);
        }

        private string Execute(IPerson memberForAdding, ITeam teamToAddTo)
        {
            teamToAddTo.AddMemberToTeam(memberForAdding);

            var returnMessage = string.Format(ObjectConsts.ObjectAddedToTeam, nameof(Person), memberForAdding.PersonName, teamToAddTo.TeamName);
            this.historyEventWriter.AddHistoryEvent(returnMessage, memberForAdding, null, teamToAddTo);

            return returnMessage;
        }

    }
}
