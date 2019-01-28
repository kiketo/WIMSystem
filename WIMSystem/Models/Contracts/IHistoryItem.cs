using System;

namespace WIMSystem.Models.Contracts
{
    public interface IHistoryItem
    {
        string Description { get; set; }
        DateTime CreationDate { get; }
        IMember Member { get; }
        IBoard Board { get; }
        T Team { get; }
    }
}
