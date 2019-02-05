using System;

namespace WIMSystem.Models.Contracts
{
    public interface IHistoryItem
    {
        string Description { get; set; }
        DateTime CreationDate { get; }
        IPerson Member { get; }
        IBoard Board { get; }
        ITeam Team { get; }
        string FilteredByTeamToString();
        string FilteredByBoardToString();
    }
}
