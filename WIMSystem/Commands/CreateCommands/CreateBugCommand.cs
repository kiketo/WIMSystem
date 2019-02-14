using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Core.Utils;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Commands.CreateCommands
{
    public class CreateBugCommand : IEngineCommand
    {
        private readonly IHistoryEventWriter historyEventWriter;
        private readonly IComponentsFactory componentsFactory;
        private readonly IGetters getter;

        public CreateBugCommand(IHistoryEventWriter historyEventWriter, IComponentsFactory componentsFactory, IGetters getter)
        {
            this.historyEventWriter = historyEventWriter ?? throw new ArgumentNullException(nameof(historyEventWriter));
            this.componentsFactory = componentsFactory ?? throw new ArgumentNullException(nameof(componentsFactory));
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
        }

        public string ReadSingleCommand(IList<string> parameters)
        {
            var bugTitle = parameters[0];
            var bugDescription = parameters[1];
            var stepsToReproduce = parameters[2].Trim().Split(ObjectConsts.SPLIT_CHAR).ToList();
            var bugPriority = StringToEnum<PriorityType>.Convert(parameters[3]);
            var bugSeverity = StringToEnum<BugSeverityType>.Convert(parameters[4]);
            var teamName = parameters[5];
            var board = this.getter.GetBoard(teamName, parameters[6]);

            return this.Execute(bugTitle, bugDescription, stepsToReproduce, bugPriority, bugSeverity, board);
        }

        private string Execute(string bugTitle, string bugDescription, List<string> stepsToReproduce, PriorityType bugPriority, BugSeverityType bugSeverity, IBoard board, IPerson bugAssignee = null)
        {
            var bug = this.componentsFactory.CreateBug(bugTitle, bugDescription, stepsToReproduce, bugPriority, bugSeverity, board, bugAssignee);

            if (bug == null)
            {
                throw new ArgumentException(string.Format(ObjectConsts.ObjectExists, nameof(Bug), bug.Title));
            }

            board.AddWorkItemToBoard(bug);

            string returnMessage = string.Format(ObjectConsts.ObjectCreated, nameof(Bug), bug.Title);

            this.historyEventWriter.AddHistoryEvent(returnMessage, bugAssignee, board, board.Team);

            return returnMessage;
        }
    }
}
