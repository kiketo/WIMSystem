using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models
{
    public class Bug : AssignableWorkItem, IBug, IAssignableWorkItem, IWorkItem
    {
        private readonly IList<string> stepsToReproduce;

        public Bug(string title, string description, IList<string> stepsToReproduce,
            PriorityType priority, BugSeverityType severity, IBoard board, IMember assignee=null) // assignee is optional?
            : base(title, description,priority,board,assignee)
        {
            if (stepsToReproduce == null)
            {
                throw new ArgumentNullException("stepsToReproduce","Steps to reproduce cannot be null!");
            }

            if (!stepsToReproduce.Any())
            {
                throw new ArgumentException("stepsToReproduce", "There must be at least one step to reproduce for the bug!");
            }

            this.stepsToReproduce = stepsToReproduce;
            this.Severity = severity;
            this.BugStatus = BugStatusType.Active;
            
        }

        public IList<string> StepsToReproduce
        {
            get
            {
                return this.stepsToReproduce;
            }
        }

        public BugSeverityType Severity { get; private set; }

        public BugStatusType BugStatus { get ; private set; }

        public void ChangeSeverity(string severity)
        {
            if (severity == null)
            {
                throw new ArgumentNullException("severity", "Severity cannot be null or empty!");
            }

            else
            {
                BugSeverityType severityEnum = (BugSeverityType)Enum.Parse(typeof(BugSeverityType), severity, true);
                this.Severity = severityEnum;
            }
        }

        public void ChangeStatus(string status)
        {
            if (status == null)
            {
                throw new ArgumentNullException("status", "Status cannot be null or empty!");
            }

            else
            {
                BugStatusType statusEnum = (BugStatusType)Enum.Parse(typeof(BugStatusType), status, true);
                this.BugStatus = statusEnum;
            }
        }
    }
}
