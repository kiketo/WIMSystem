using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models.Contracts
{
    public interface IStory : IAssignableWorkItem, IWorkItem
    {
        StorySizeType StorySize { get; }
        StoryStatusType StoryStatus { get; }

        void ChangeSize(string size);
        void ChangeStatus(string status);
    }
}
