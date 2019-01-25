using System;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class HistoryItem : IHistoryItem
    {
        private string description;
        private readonly DateTime creationDate;
        private readonly IMember member;
        private readonly IBoard board;
        private readonly ITeam team;

        public HistoryItem(string description, DateTime creationDate, IMember member, IBoard board, ITeam team)
        {
            this.Description = description;
            this.creationDate = creationDate;
            this.member = member;
            this.board = board;
            this.team = team;
        }

        public string Description
        {
            get => this.description;
            set
            {

                this.description = value;
            }
        }
        public DateTime CreationDate
        {
            get => this.CreationDate;
        }
        public IMember Member
        {
            get => this.member;
        }
        public IBoard Board
        {
            get => this.board;
        }
        public ITeam Team
        {
            get => this.team;
        }
    }
}
