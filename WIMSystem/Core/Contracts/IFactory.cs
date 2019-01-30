using System.Collections.Generic;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Core.Contracts
{
    public interface IFactory
    {
        IBoard CreateBoard(string name, ITeam team);
        IBug CreateBug(string title, string description, IList<string> stepsToReproduce, PriorityType priority, BugSeverityType severity, IBoard board, IMember assignee = null);
        IComment CreateComment(string message, IMember author);
        IFeedback CreateFeedback(string title, string description, int rating, IBoard board);
        IHistoryItem CreateHistoryItem(string description, IMember member, IBoard board, ITeam team);
        IMember CreateMember(string memberName, IMembersCollection membersCollection);
        IStory CreateStory(string title, string description, PriorityType priority, StorySizeType storySize, IBoard board, IMember assignee = null);
        ITeam CreateTeam(string teamName, IWIMTeams teamsList);
    }
}