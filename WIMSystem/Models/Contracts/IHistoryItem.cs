using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface IHistoryItem
    {
        string Description { get; set; }
        DateTime CreationDate { get; set; }
        IMember Member { get; set; }
        IBoard Board { get; set; }
        ITeam Team { get; set; }
    }
}
