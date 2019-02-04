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

        public StorySizeType StorySize { get; private set; }

        public StoryStatusType StoryStatus { get; private set; }

        public void ChangeSize(string size)
        {
            if (size == null)
            {
                throw new ArgumentNullException("size", "Size cannot be null or empty!");
            }

            else
            {
                StorySizeType sizeEnum = (StorySizeType)Enum.Parse(typeof(StorySizeType), size, true);
                this.StorySize = sizeEnum;
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
                StoryStatusType statusEnum = (StoryStatusType)Enum.Parse(typeof(StoryStatusType), status, true);
                this.StoryStatus = statusEnum;
            }
        }
    }
}
