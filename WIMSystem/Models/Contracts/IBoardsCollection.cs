using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Models.Contracts
{
    public interface IBoardsCollection
    {
        IDictionary<string, IBoard> BoardsInTheTeam { get; }
    }
}
