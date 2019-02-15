using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Core.Utils;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;
using Utils;
using WIMSystem.Models;
using WIMSystem.Commands.Utils;
using WIMSystem.Utils;

namespace WIMSystem.Commands.ChangeCommands
{
    public class AssignWorkItemToMemberCommand : IEngineCommand
    {
        private readonly IHistoryEventWriter historyEventWriter;
        private readonly IGetters getter;

        public AssignWorkItemToMemberCommand(IHistoryEventWriter historyEventWriter, IGetters getter)
        {
            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(nameof(historyEventWriter));
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
        }

        public string ReadSingleCommand(IList<string> parameters)
        {
            var teamName = parameters[0];
            var board = this.getter.GetBoard(teamName, parameters[1]);
            var workItem = this.getter.GetAssignableWorkItem(board, parameters[2]);
            var member = this.getter.GetPerson(parameters[3]);
            return this.Execute(workItem, member);
        }

        private string Execute(IAssignableWorkItem workItem, IPerson member)
        {

            if (Validators.IsNullValue(workItem))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,nameof(WorkItem)));
            }

            if (Validators.IsNullValue(member))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,nameof(member)));
            }

            if (!member.IsAssignedToTeam)
            {
                throw new ArgumentException(string.Format($"{member.PersonName} is not a member of any team!"));

            }

            workItem.AssignMember(member);
            member.MemberWorkItems.Add(workItem);

            var returnMessage = string.Format(CommandsConsts.WorkItemAssigned, workItem.Title, member.PersonName);

            this.historyEventWriter.AddHistoryEvent(returnMessage, member, workItem.Board, workItem.Board.Team, workItem);

            return returnMessage;
        }
    }
}
