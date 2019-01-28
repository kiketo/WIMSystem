using System;
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
       public Board CreateBoard(string name, IList<IWorkItem> workItems)
        {
           return new Board(name, workItems);
        }

        public Bug CreateBug(string title, string description, IList<string> stepsToReproduce, PriorityType priority, BugSeverityType severity, BugStatusType bugStatus, IMember assignee = null)
        {
            return new Bug(title,description,stepsToReproduce,priority,severity,bugStatus);
        }

        public Comment CreateComment(string message, Member author)
        {
            return new Comment(message, author);
        }

        public Feedback CreateFeedback(string title, string description, int rating, FeedbackStatusType feedbackStatus)
        {
            return new Feedback(title,description,rating, feedbackStatus);
        }

        public HistoryItem CreateHistoryItem(string description, DateTime creationDate, IMember member, IBoard board, ITeam team)
        {
           return new HistoryItem(description,creationDate,member,board,team);
        }

        public Member CreateMember(string memberName, IList<IWorkItem> memberWorkItems)
        {
            return new Member(memberName,memberWorkItems);
        }

        public Team CreateTeam(string name, IList<IMember> membersList, IList<IBoard> boardsList)
        {
           return new Team(name, membersList, boardsList);
        }
    }
}
