﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface ITeam
    {
        string TeamName { get; set; }
        IList<IMember> MemberList { get; }
        IList<IBoard> BoardList { get; }
    }
}
