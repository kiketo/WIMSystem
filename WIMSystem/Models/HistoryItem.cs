﻿using System;
using System.Collections.Generic;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class HistoryItem : IHistoryItem
    {
        private string description;
        private readonly DateTime creationDate;
        private readonly IPerson member;
        private readonly IBoard board;
        private readonly ITeam team;

        public HistoryItem(string description, DateTime creationDate, IPerson member, IBoard board, ITeam team, IWorkItem workItem)
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
        public IPerson Member
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
