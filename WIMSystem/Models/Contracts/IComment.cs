using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface IComment
    {
        string Message { get; }
        IMember Author { get; }
    }
}
