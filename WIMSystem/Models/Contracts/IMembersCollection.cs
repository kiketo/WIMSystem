﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface IMembersCollection
    {
        IDictionary<string, IMember> Members { get; }
        
    }
}
