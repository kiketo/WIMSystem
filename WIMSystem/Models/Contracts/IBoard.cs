using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface IBoard
    {
        string BoardName { get; set; }
        IList<IWorkItem> BoardWorkItems { get; }
    }
}
