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
            PriorityType priority, BugSeverityType severity, IBoard board, IPerson assignee = null) // assignee is optional?
            : base(title, description, priority, board, assignee)
        {
            if (stepsToReproduce == null)
            {
                throw new ArgumentNullException("stepsToReproduce", "Steps to reproduce cannot be null!");
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
                return new List<string>(this.stepsToReproduce);
            }
        }

        public BugSeverityType Severity { get; set; }

        public BugStatusType BugStatus { get; set; }

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

        public override void ChangeStatus(string newStatus)
        {
            if (newStatus == null)
            {
                throw new ArgumentNullException("status", "Status cannot be null or empty!");
            }

            else
            {
                BugStatusType statusEnum = (BugStatusType)Enum.Parse(typeof(BugStatusType), newStatus, true);
                this.BugStatus = statusEnum;
            }
        }

        public override Enum GetStatus()
        {
            return this.BugStatus;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine(base.ToString());
            if (this.StepsToReproduce.Count>0)
            {
                str.AppendLine($"Steps to reproduce:");
                foreach (var step in this.StepsToReproduce)
                {
                    str.AppendLine(step);
                }
            }
            str.AppendLine($"Severity: {this.Severity}");
            str.AppendLine($"Bug status: {this.BugStatus}");
                       
            return str.ToString();
        }
    }
}
