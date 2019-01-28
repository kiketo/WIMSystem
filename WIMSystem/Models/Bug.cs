using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models
{
    public class Bug : AssignableWorkItem, IBug
    {
        private readonly IList<string> stepsToReproduce;

        public Bug(string title, string description, IList<string> stepsToReproduce,
            PriorityType priority, BugSeverityType severity, /*BugStatusType bugStatus*/IMember assignee=null) // assignee is optional?
            : base(title, description,assignee)
        {
            if (stepsToReproduce == null)
            {
                throw new ArgumentNullException("stepsToReproduce");
            }
            if (!stepsToReproduce.Any())
            {
                throw new ArgumentException("stepsToReproduce", "There must be at least one step to reproduce for the bug!");
            }
            this.stepsToReproduce = stepsToReproduce;
            this.Priority = priority;
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

        public PriorityType Priority { get; set; }

        public BugSeverityType Severity { get; set; }

        public BugStatusType BugStatus { get ; set ; }
    }
}
