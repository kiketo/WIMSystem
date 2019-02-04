using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models
{
    public class Story : AssignableWorkItem, IStory, IAssignableWorkItem, IWorkItem
    {
        public Story(string title, string description, PriorityType priority,
            StorySizeType storySize, IBoard board, IMember assignee=null) //assignee is optional?
            : base(title, description,priority,board,assignee)
        {
            this.StorySize = storySize;
            this.StoryStatus = StoryStatusType.NotDone;
        }

        public StorySizeType StorySize { get; set; }

        public StoryStatusType StoryStatus { get; set; }
    }
}
