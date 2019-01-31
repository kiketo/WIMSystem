﻿using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Core.Contracts;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Enums;

namespace WIMSystem.Core
{
    public class Factory : IFactory
    {
        public ITeam CreateTeam(string teamName, IWIMTeams teamsList)
        {
            return new Team(teamName,teamsList);
        }

        public IMember CreateMember(string memberName, IMembersCollection membersCollection)
        {
            return new Member(memberName, membersCollection);
        }

        public IBoard CreateBoard(string name, ITeam team)
        {
            return new Board(name,team);
        }

        public IBug CreateBug(string title, string description, IList<string> stepsToReproduce, PriorityType priority, BugSeverityType severity,IBoard board, IMember assignee = null)
        {
            return new Bug(title, description, stepsToReproduce, priority, severity,board);
        }

        public IFeedback CreateFeedback(string title, string description, int rating, IBoard board)
        {
            return new Feedback(title, description, rating, board);
        }

        public IStory CreateStory(string title, string description, PriorityType priority,
            StorySizeType storySize, IBoard board, IMember assignee = null)
        {
            return new Story(title, description,priority,storySize,board);
        }

        public IComment CreateComment(string message, IMember author)
        {
            return new Comment(message, author);
        }

        public IHistoryItem CreateHistoryItem(string description, IMember member, IBoard board, ITeam team)
        {
            return new HistoryItem(description, DateTime.Now, member, board, team);
        }
    }
}