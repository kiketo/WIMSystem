﻿using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Enums;

namespace WIMSystem.Models.Contracts
{
    public interface IStory : IAssignableWorkItem, IWorkItem
    {
        StorySizeType StorySize { get; set; }
        StoryStatusType StoryStatus { get; set; }

        void ChangeSize(string size);
    }
}
