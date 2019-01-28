using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Enums;

namespace WIMSystem.Core.Contracts
{
    public interface IFactory
    {
         
        Board CreateBoard (string name, IList <IWorkItem> workItems);

        Bug CreateBug(string title, string description, IList<string> stepsToReproduce,
            PriorityType priority, BugSeverityType severity, BugStatusType bugStatus,
            IMember assignee = null);

        Comment CreateComment(string message, Member author);

        Feedback CreateFeedback(string title, string description, int rating, FeedbackStatusType feedbackStatus);

        HistoryItem CreateHistoryItem(string description, DateTime creationDate, IMember member, IBoard board, T team);

        Member CreateMember(string memberName, IList<IWorkItem> memberWorkItems);

        //Story CreateStory();

        Team CreateTeam(string name, IList<IMember>membersList, IList<IBoard>boardsList);

    }
}
