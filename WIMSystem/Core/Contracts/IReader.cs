using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Core.Contracts
{
    internal interface IReader
    {
        ICollection<string> Read();
    }
}
