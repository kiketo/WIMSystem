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
            StorySizeType storySize, IBoard board, IPerson assignee = null) //assignee is optional?
            : base(title, description, priority, board, assignee)
        {
            this.StorySize = storySize;
            this.StoryStatus = StoryStatusType.NotDone;
        }

        public StorySizeType StorySize { get; set; }

        public StoryStatusType StoryStatus { get; set; }

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

        public override void ChangeStatus(string status)
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

        public override Enum GetStatus()
        {
            return this.StoryStatus;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append(base.ToString());
            str.AppendLine($"Story size: {this.StorySize}");
            str.AppendLine($"Story status: {this.StoryStatus}");
            
            return str.ToString();
        }
    }
}
