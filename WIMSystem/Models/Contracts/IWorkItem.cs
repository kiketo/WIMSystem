using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface IWorkItem
    {
        int ID { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        IList<string> Comments { get; }
    }
}
