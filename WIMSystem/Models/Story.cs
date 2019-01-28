using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models
{
    public class Story : AssignableWorkItem, IStory
    {
        private PriorityType priority;
        private StorySizeType storySize;
        private StoryStatusType storyStatus;
        

        public Story(string title, string description, PriorityType priority,
            StorySizeType storySize, IMember assignee=null) //assignee is optional?
            : base(title, description,assignee)
        {
            this.Priority = priority;
            this.StorySize = storySize;
            this.StoryStatus = StoryStatusType.NotDone;
        }

        public PriorityType Priority { get; set; }

        public StorySizeType StorySize { get; set; }

        public StoryStatusType StoryStatus { get; set; }
    }
}
