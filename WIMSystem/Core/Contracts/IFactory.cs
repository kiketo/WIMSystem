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
    interface IFactory
    {
         
        Board CreateBoard (string name, List <WorkItem> workItems,List <HistoryItem>historyItems);

        Bug CreateBug(string title, string description, IList<string> stepsToReproduce,
            PriorityType priority, BugSeverityType severity, BugStatusType bugStatus,
            IMember assignee = null);

        Comment CreateComment(string message, Member author);

        Feedback CreateFeedback(int rating, FeedbackStatusType feedbackStatusType);

        HistoryItem CreateHistoryItem(string description, DateTime creationDate, IMember member, IBoard board, ITeam team);

        Member CreateMember(string memberName, IList<IWorkItem> memberWorkItems);

        //Story CreateStory();

        Team CreateTeam(string name, List<Member>membersList, List<Board>boardsList, List<HistoryItem>historyList);

    }
}
