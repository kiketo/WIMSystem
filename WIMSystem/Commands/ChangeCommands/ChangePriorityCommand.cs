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
    public class ChangePriorityCommand : IEngineCommand
    {
        private readonly IHistoryEventWriter historyEventWriter;
        private readonly IGetters getter;

        public ChangePriorityCommand(IHistoryEventWriter historyEventWriter, IGetters getter)
        {
            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(nameof(historyEventWriter));
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
        }

        public string ReadSingleCommand(IList<string> parameters)
        {
            var teamName = parameters[0];
            var board = this.getter.GetBoard(teamName, parameters[1]);
            var workItem = this.getter.GetAssignableWorkItem(board, parameters[2]);
            var priority = StringToEnum<PriorityType>.Convert(parameters[3]);
            return this.Execute(workItem, priority);
        }

        private string Execute(IAssignableWorkItem workItem, PriorityType priority)
        {
            if (Validators.IsNullValue(workItem))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,nameof(WorkItem)));
            }

            if (!(workItem is IAssignableWorkItem))
            {
                throw new ArgumentException(string.Format($"{workItem.GetType().Name} is not a {nameof(Feedback)}!"));
            }

            workItem.Priority = priority;

            var returnMessage = string.Format(CommandsConsts.WorkItemPriorityChange, workItem.Title, priority);

            IPerson member = null;

            if (workItem is IAssignableWorkItem)
            {
                member = (workItem as IAssignableWorkItem).Assignee;
            }

            this.historyEventWriter.AddHistoryEvent(returnMessage, member, workItem.Board, workItem.Board.Team, workItem);

            return returnMessage;
        }
    }
}
