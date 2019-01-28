﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface T
    {
        string TeamName { get; set; }
        IList<IMember> MemberList { get; }
        IDictionary<string,IBoard> BoardList { get; }
    }
}
