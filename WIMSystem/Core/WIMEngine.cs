using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;
using WIMSystem.Core.Contracts;
using WIMSystem.Core.Factories;
using WIMSystem.Core.Factories.Contracts;
using WIMSystem.Core.Utils;
using WIMSystem.Models;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;

namespace WIMSystem.Core
{
    internal class WIMEngine : IWIMEngine
    {
        private const string InvalidCommand = "Invalid command name: {0}!";
        private const string ObjectExists = "{0} with name {1} already exists!";
        private const string ObjectCreated = "{0} with name {1} was created!";
        private const string ObjectDoesNotExist = "{0} {1} does not exist!";
        private const string ObjectAddedToTeam = "{0} {1} added to team \"{2}\"!";
        private const string ObjectRemovedFromTeam = "{0} {1} removed from team \"{2}\"!";
        private const string WorkItemAssigned = "{0} work item is assigned to member {1}!";
        private const string WorkItemUnAssigned = "{0} work item is unassigned from member {1}!";
        private const string WorkItemStatusChange = "{0} work item's status is changed to {1}!";
        private const string FeedbackRatingChange = "{0} feedback's rating is changed to {1}!";
        private const string WorkItemPriorityChange = "{0} work item's priority is changed to {1}!";
        private const string StorySizeChange = "{0} story's size is changed to {1}!";
        private const string BugSeverityChange = "{0} bug's severity is changed to {1}!";
        private const string CommentAdded = "Comment \"{0}\" with author {1} added to \"{2}\"!";

        private const char SPLIT_CHAR = ',';
        private readonly ICommandsFactory commandsFactory;
        private readonly IComponentsFactory factory;
        private readonly IWIMTeams wimTeams;
        private readonly IPersonsCollection personList;
        private readonly IHistoryItemsCollection historyItemsList;

        public WIMEngine(ICommandsFactory commandsFactory, IComponentsFactory factory, IWIMTeams wimTeams, IPersonsCollection personList, IHistoryItemsCollection historyItemsList)
        {
            this.commandsFactory = commandsFactory;
            this.factory = factory ?? throw new ArgumentException(string.Format(Consts.NULL_OBJECT, nameof(factory)));
            this.wimTeams = wimTeams ?? throw new ArgumentException(string.Format(Consts.NULL_OBJECT, nameof(factory)));
            this.personList = personList ?? throw new ArgumentException(string.Format(Consts.NULL_OBJECT, nameof(factory)));
            this.historyItemsList = historyItemsList ?? throw new ArgumentException(string.Format(Consts.NULL_OBJECT, nameof(factory)));
        }

        public void ExecuteCommands(ICommandParser commandParser)
        {
            var commands = commandParser.ReadCommands();
            var commandResult = this.ProcessCommands(commands);
            this.PrintReports(commandResult);
        }

        public void ExecuteCommands(IList<ICommand> commands)
        {
            var commandResult = this.ProcessCommands(commands);
            this.PrintReports(commandResult);
        }

        private IList<string> ProcessCommands(IList<ICommand> commands)
        {
            var reports = new List<string>();

            foreach (var command in commands)
            {
                try
                {
                    //var report = this.ProcessSingleCommand(command);
                    var engineCommand = this.commandsFactory.GetCommand(command.Name);
                    var report = engineCommand.ReadSingleCommand(command.Parameters);
                    reports.Add(report);
                }
                catch (Exception ex)
                {
                    reports.Add(ex.Message);
                }
            }

            return reports;
        }

        private string ProcessSingleCommand(ICommand command)
        {
            switch (command.Name)
            {

                //case "CreateTeam":
                //    {
                //        var teamName = command.Parameters[0];
                //        return this.CreateTeam(teamName);
                //    }

                //case "CreatePerson":
                //    {
                //        var personName = command.Parameters[0];
                //        return this.CreatePerson(personName);
                //    }

                //case "CreateBoard":
                //    {
                //        var boardName = command.Parameters[0];
                //        var team = this.GetTeam(command.Parameters[1]);
                //        return this.CreateBoard(boardName, team);
                //    }

                //case "AddPersonToTeam":
                //    {
                //        var memberForAdding = this.GetPerson(command.Parameters[0]);
                //        var teamToAddTo = this.GetTeam(command.Parameters[1]);
                //        return this.AddMemberToTeam(memberForAdding, teamToAddTo);
                //    }

                

                case "RemoveMemberFromTeam": 
                    {
                        var teamToRemove = this.GetTeam(command.Parameters[0]);
                        var memberForRemoving = this.GetPerson(command.Parameters[1]);
                        return this.RemoveMemberFromTeam(teamToRemove, memberForRemoving);
                    }  //Not used in the current application

                //case "CreateBug":
                //    {
                //        var bugTitle = command.Parameters[0];
                //        var bugDescription = command.Parameters[1];
                //        var stepsToReproduce = command.Parameters[2].Trim().Split(SPLIT_CHAR).ToList();
                //        var bugPriority = StringToEnum<PriorityType>.Convert(command.Parameters[3]);
                //        var bugSeverity = StringToEnum<BugSeverityType>.Convert(command.Parameters[4]);
                //        var teamName = command.Parameters[5];
                //        var board = this.GetBoard(teamName, command.Parameters[6]);
                //        //var bugAssignee = this.GetPerson(command.Parameters[7]);
                //        //var bugComments = command.Parameters[6].Trim().Split(SPLIT_CHAR).ToList();

                //        return this.CreateBug(bugTitle, bugDescription, stepsToReproduce, bugPriority, bugSeverity, board);
                //    }

                //case "CreateStory":
                //    {
                //        var storyTitle = command.Parameters[0];
                //        var storyDescription = command.Parameters[1];
                //        var storyPriority = StringToEnum<PriorityType>.Convert(command.Parameters[2]);
                //        var storySize = StringToEnum<StorySizeType>.Convert(command.Parameters[3]);
                //        var teamName = command.Parameters[4];
                //        var board = this.GetBoard(teamName, command.Parameters[5]);
                //        //var storyAssignee = this.GetPerson(command.Parameters[7]);

                //        return this.CreateStory(storyTitle, storyDescription, storyPriority, storySize, board);//, storyAssignee);
                //    }

                //case "CreateFeedback":
                //    {
                //        var feedbackTitle = command.Parameters[0];
                //        var feedbackDescription = command.Parameters[1];
                //        var feedbackRating = int.Parse(command.Parameters[2]);
                //        var teamName = command.Parameters[3];
                //        var board = this.GetBoard(teamName, command.Parameters[4]);
                //        return this.CreateFeedback(feedbackTitle, feedbackDescription, feedbackRating, board);
                //    }

                //case "CreateComment":
                //    {
                //        var teamName = this.GetTeam(command.Parameters[0]);
                //        var boardName = this.GetBoard(teamName.TeamName, command.Parameters[1]);
                //        var workItem = this.GetWorkItem(boardName, command.Parameters[2]);
                //        var comment = command.Parameters[3];
                //        var author = this.GetMember(teamName, command.Parameters[4]);

                //        return this.CreateComment(workItem, comment, author);
                //    }

                //case "ShowAllPeople":
                //    {
                //        return this.ShowAllPeople();
                //    }
                //case "ShowPersonActivity":
                //    {
                //        var person = this.GetPerson(command.Parameters[0]);
                //        return this.ShowPersonActivity(person);
                //    }
                //case "ShowAllTeams":
                //    {
                //        return this.ShowAllTeams();
                //    }
                //case "ShowTeamActivity":
                //    {
                //        var team = this.GetTeam(command.Parameters[0]);
                //        return this.ShowTeamActivity(team);
                //    }
                //case "ShowAllTeamMembers":
                //    {
                //        var team = this.GetTeam(command.Parameters[0]);
                //        return this.ShowAllTeamMembers(team);
                //    }
               // case "ShowAllTeamBoards":
               //     {
               //         var team = this.GetTeam(command.Parameters[0]);
               //         return this.ShowAllTeamBoards(team);
               //     }
                //case "ShowBoardActivity":
                //    {
                //        var teamName = command.Parameters[0];
                //        var board = this.GetBoard(teamName, command.Parameters[1]);
                //        return this.ShowBoardActivity(board);
                //    }
                //case "ChangePriority":
                //    {
                //        var teamName = command.Parameters[0];
                //        var board = this.GetBoard(teamName, command.Parameters[1]);
                //        var workItem = this.GetAssignableWorkItem(board, command.Parameters[2]);
                //        var priority = StringToEnum<PriorityType>.Convert(command.Parameters[3]);
                //        return this.ChangePriority(workItem, priority);
                //    }
                //case "ChangeSeverityOfBug":
                //    {
                //        var teamName = command.Parameters[0];
                //        var board = this.GetBoard(teamName, command.Parameters[1]);
                //        var workItem = this.GetWorkItem(board, command.Parameters[2]);
                //        var severity = StringToEnum<BugSeverityType>.Convert(command.Parameters[3]);
                //        return this.ChangeSeverityOfBug(workItem, severity);
                //    }
                //case "ChangeStatus":
                //    {
                //        var teamName = command.Parameters[0];
                //        var board = this.GetBoard(teamName, command.Parameters[1]);
                //        var workItem = this.GetWorkItem(board, command.Parameters[2]);
                //        var status = command.Parameters[3];
                //        return this.ChangeStatus(workItem, status);
                //    }
                //case "ChangeSizeOfStory":
                //    {
                //        var teamName = command.Parameters[0];
                //        var board = this.GetBoard(teamName, command.Parameters[1]);
                //        var workItem = this.GetWorkItem(board, command.Parameters[2]);
                //        var size = StringToEnum<StorySizeType>.Convert(command.Parameters[3]);
                //        return this.ChangeSizeOfStory(workItem, size);
                //    }
                //case "ChangeRatingOfFeedback":
                //    {
                //        var teamName = command.Parameters[0];
                //        var board = this.GetBoard(teamName, command.Parameters[1]);
                //        var workItem = this.GetWorkItem(board, command.Parameters[2]);
                //        var priority = int.Parse(command.Parameters[3]);
                //        return this.ChangeRatingOfFeedback(workItem, priority);
                //    }
                //case "AssignWorkItemToMember":
                //    {
                //        var teamName = command.Parameters[0];
                //        var board = this.GetBoard(teamName, command.Parameters[1]);
                //        var workItem = this.GetAssignableWorkItem(board, command.Parameters[2]);
                //        var member = this.GetPerson(command.Parameters[3]);
                //        return this.AssignWorkItemToMember(workItem, member);
                //    }
                //case "UnassignWorkItem":
                //    {
                //        var teamName = command.Parameters[0];
                //        var board = this.GetBoard(teamName, command.Parameters[1]);
                //        var workItem = this.GetAssignableWorkItem(board, command.Parameters[2]);
                //        return this.UnassignWorkItemToMember(workItem);
                //    }
                //ase "ListBoardWorkItems":
                //   {
                //       var teamName = command.Parameters[0];
                //       var board = this.GetBoard(teamName, command.Parameters[1]);
                //
                //       Type filterType = null;
                //       string filterStatus = null;
                //       IPerson filterAssignee = null;
                //       string sortBy = null;   // "filterType:Story" "filterStatus:High" "filterAssignee:Gosho" "sortBy:Title"
                //
                //       for (int i = 2; i < command.Parameters.Count; i++)
                //       {
                //           var paramOption = command.Parameters[i].Split(new[] { ':', }, StringSplitOptions.RemoveEmptyEntries);
                //           switch (paramOption[0])
                //           {
                //               case "filterType":
                //                   {
                //                       var typeAsString = "WIMSystem.Models." + paramOption[1];
                //                       var curAssembly = typeof(WIMEngine).Assembly;
                //                       filterType = curAssembly.GetType(typeAsString, false, true) ??
                //                           throw new ArgumentException("Undefined type {0}", paramOption[1]);
                //                       break;
                //                   }
                //               case "filterStatus":
                //                   {
                //                       filterStatus = paramOption[1];
                //                       break;
                //                   }
                //               case "filterAssignee":
                //                   {
                //                       filterAssignee = this.GetPerson(paramOption[1]);
                //                       break;
                //                   }
                //               case "sortBy":
                //                   {
                //                       sortBy = paramOption[1];
                //                       break;
                //                   }
                //           }
                //       }
                //
                //        return this.ListBoardWorkItems(board, filterType, filterStatus, filterAssignee, sortBy);
                //    }

                default:
                    return string.Format(InvalidCommand, command.Name);
            }

        }

        //private IAssignableWorkItem GetAssignableWorkItem(IBoard board, string assignableWorkItemTitle)
        //{
        //    if (board.BoardWorkItems[assignableWorkItemTitle] is IAssignableWorkItem)
        //    {
        //        return (IAssignableWorkItem)board.BoardWorkItems[assignableWorkItemTitle];
        //    }
        //    throw new ArgumentException(string.Format($"{board.BoardWorkItems[assignableWorkItemTitle].GetType().Name} is not assignable work item!"));
        //}

        //private string UnassignWorkItemToMember(IAssignableWorkItem workItem)
        //{

        //    if (Validators.IsNullValue(workItem))
        //    {
        //        throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
        //            nameof(WorkItem)
        //            ));
        //    }

        //    var member = workItem.Assignee;
        //    workItem.UnassignMember();
        //    member.MemberWorkItems.Remove(workItem);
        //    var returnMessage = string.Format(WorkItemUnAssigned, workItem.Title, member.PersonName);

        //    this.AddHistoryEvent(returnMessage,member,workItem.Board,workItem.Board.Team,workItem);

        //    return returnMessage;
        //}

        //private string AssignWorkItemToMember(IAssignableWorkItem workItem, IPerson member)
        //{

        //    if (Validators.IsNullValue(workItem))
        //    {
        //        throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
        //            nameof(WorkItem)
        //            ));
        //    }
        //    if (Validators.IsNullValue(member))
        //    {
        //        throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
        //            nameof(member)
        //            ));
        //    }

        //    if (!member.IsAssignedToTeam)
        //    {
        //        throw new ArgumentException(string.Format($"{member.PersonName} is not a member of any team!"));

        //    }

        //    workItem.AssignMember(member);
        //    member.MemberWorkItems.Add(workItem);
            
        //    var returnMessage = string.Format(WorkItemAssigned, workItem.Title, member.PersonName);

        //    this.AddHistoryEvent(returnMessage, member, workItem.Board, workItem.Board.Team, workItem);

        //    return returnMessage;
        //}

        // private string ListBoardWorkItems(IBoard board, Type filterType, string filterStatus, IPerson filterAssignee, string sortBy)
        // {
        //     return board.ListWorkItems(filterType, filterStatus, filterAssignee, sortBy);
        // }

        //private string ChangeRatingOfFeedback(IWorkItem workItem, int rating)
        //{
        //    if (Validators.IsNullValue(workItem))
        //    {
        //        throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
        //            nameof(WorkItem)
        //            ));
        //    }
        //    if (!(workItem is IFeedback))
        //    {
        //        throw new ArgumentException(string.Format($"{workItem.GetType().Name} is not a {nameof(Feedback)}!"));
        //    }
        //    ((IFeedback)workItem).Rating = rating;

        //    var returnMessage = string.Format(FeedbackRatingChange, workItem.Title, rating);

        //    this.AddHistoryEvent(returnMessage, null, workItem.Board, workItem.Board.Team, workItem);

        //    return returnMessage;
        //} 

        //private string ChangeSizeOfStory(IWorkItem workItem, StorySizeType size)
        //{
        //    if (Validators.IsNullValue(workItem))
        //    {
        //        throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
        //            nameof(WorkItem)
        //            ));
        //    }
        //    if (!(workItem is IStory))
        //    {
        //        throw new ArgumentException(string.Format($"{workItem.GetType().Name} is not a {nameof(Story)}!"));
        //    }
        //    ((IStory)workItem).Size = size;

        //    var returnMessage = string.Format(StorySizeChange, workItem.Title, size);

        //    IPerson member = null;
        //    if (workItem is IAssignableWorkItem)
        //    {
        //        member = (workItem as IAssignableWorkItem).Assignee;
        //    }
        //    this.AddHistoryEvent(returnMessage, member, workItem.Board, workItem.Board.Team, workItem);

        //    return returnMessage;
        //} 

        //private string ChangeStatus(IWorkItem workItem, string status)
        //{
        //    if (Validators.IsNullValue(workItem))
        //    {
        //        throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
        //            nameof(WorkItem)
        //            ));
        //    }
        //    workItem.ChangeStatus(status);

        //    var returnMessage = string.Format(WorkItemStatusChange, workItem.Title, status);

        //    IPerson member = null;
        //    if (workItem is IAssignableWorkItem)
        //    {
        //        member = (workItem as IAssignableWorkItem).Assignee;
        //    }
        //    this.AddHistoryEvent(returnMessage, member, workItem.Board, workItem.Board.Team, workItem);

        //    return returnMessage;
        //} 

        //private string ChangeSeverityOfBug(IWorkItem workItem, BugSeverityType severity)
        //{
        //    if (Validators.IsNullValue(workItem))
        //    {
        //        throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
        //            nameof(WorkItem)
        //            ));
        //    }
        //    if (!(workItem is IBug))
        //    {
        //        throw new ArgumentException(string.Format($"{workItem.GetType().Name} is not a {nameof(Bug)}!"));
        //    }
        //    ((IBug)workItem).Severity = severity;

        //    var returnMessage = string.Format(BugSeverityChange, workItem.Title, severity);

        //    IPerson member = null;
        //    if (workItem is IAssignableWorkItem)
        //    {
        //        member = (workItem as IAssignableWorkItem).Assignee;
        //    }
        //    this.AddHistoryEvent(returnMessage, member, workItem.Board, workItem.Board.Team, workItem);

        //    return returnMessage;
        //} 

        //private string ChangePriority(IAssignableWorkItem workItem, PriorityType priority)
        //{
        //    if (Validators.IsNullValue(workItem))
        //    {
        //        throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
        //            nameof(WorkItem)
        //            ));
        //    }
        //    if (!(workItem is IAssignableWorkItem))
        //    {
        //        throw new ArgumentException(string.Format($"{workItem.GetType().Name} is not a {nameof(Feedback)}!"));
        //    }
        //    workItem.Priority = priority;

        //    var returnMessage = string.Format(WorkItemPriorityChange, workItem.Title, priority);

        //    IPerson member = null;
        //    if (workItem is IAssignableWorkItem)
        //    {
        //        member = (workItem as IAssignableWorkItem).Assignee;
        //    }
        //    this.AddHistoryEvent(returnMessage, member, workItem.Board, workItem.Board.Team, workItem);

        //    return returnMessage;
        //} 

        //private string ShowBoardActivity(IBoard board)
        //{
        //    if (Validators.IsNullValue(board))
        //    {
        //        throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
        //            nameof(board)
        //            ));
        //    }
        //    return historyItemsList.ShowBoardActivity(board);
        //}

        //private string ShowAllTeamBoards(ITeam team)
        //{
        //    if (Validators.IsNullValue(team))
        //    {
        //        throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
        //            nameof(team)
        //            ));
        //    }
        //    return team.ShowAllTeamBoards();
        //}

        //private string ShowAllTeamMembers(ITeam team)
        //{
        //    if (Validators.IsNullValue(team))
        //    {
        //        throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
        //            nameof(team)
        //            ));
        //    }
        //    return team.ShowAllTeamMembers();
        //}

        //private string ShowTeamActivity(ITeam team)
        //{
        //    if (Validators.IsNullValue(team))
        //    {
        //        throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
        //            nameof(team)
        //            ));
        //    }
        //    return historyItemsList.ShowTeamActivity(team); 
        //}

        //private string ShowAllTeams()
        //{
        //    return wimTeams.ShowAllTeams();
        //}

        //private string ShowPersonActivity(IPerson person)
        //{
        //    if (Validators.IsNullValue(person))
        //    {
        //        throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
        //            nameof(person)
        //            ));
        //    }
        //    return historyItemsList.ShowPersonActivity(person);
        //}

        //private string ShowAllPeople()
        //{
        //    return this.personList.ShowAllPeople();
        //}

        //private string CreateTeam(string teamName)
        //{
        //    if (this.wimTeams.Contains(teamName))
        //    {
        //        return string.Format(ObjectExists, nameof(Team), teamName);
        //    }

        //    var team = this.factory.CreateTeam(teamName, this.wimTeams);
        //    this.wimTeams.AddTeam(team);
        //    var returnMessage = string.Format(ObjectCreated, nameof(Team), teamName);

        //    this.AddHistoryEvent(returnMessage,null,null,team);

        //    return returnMessage;
        //} 

        //private string CreatePerson(string personName)
        //{
        //    if (this.personList.Contains(personName))
        //    {
        //        throw new ArgumentException(string.Format(ObjectExists, nameof(Person), personName));
        //    }

        //    var person = this.factory.CreatePerson(personName);
        //    this.personList.AddPerson(person);
        //    var returnMessage = string.Format(ObjectCreated, nameof(Person), personName);
        //    this.AddHistoryEvent(returnMessage,person);
        //    return returnMessage;
        //} 

        //private string CreateBoard(string boardName, ITeam team)
        //{
        //    var board = this.factory.CreateBoard(boardName, team);
        //    if (board == null)
        //    {
        //        throw new ArgumentException(string.Format(ObjectExists, nameof(Board), board));
        //    }
        //    team.AddBoardToTeam(board);
        //    var returnMessage = string.Format(ObjectCreated, nameof(Board), boardName);
        //    this.AddHistoryEvent(returnMessage, null, board, team);


        //    return returnMessage;

        //}  

        //private string AddMemberToTeam(IPerson memberForAdding, ITeam teamToAddTo)
        //{
        //    teamToAddTo.AddMemberToTeam(memberForAdding);

        //    var returnMessage = string.Format(ObjectAddedToTeam, nameof(Person), memberForAdding.PersonName, teamToAddTo.TeamName);
        //    this.AddHistoryEvent(returnMessage,memberForAdding,null,teamToAddTo);

        //    return returnMessage;
        //}

        private string RemoveMemberFromTeam(ITeam teamToRemoveFrom, IPerson memberForRemoving)
        {
            teamToRemoveFrom.RemoveMemberFromTeam(memberForRemoving);
            var returnMessage = string.Format(ObjectRemovedFromTeam, nameof(Person), memberForRemoving.PersonName, teamToRemoveFrom.TeamName);
            this.AddHistoryEvent(returnMessage, memberForRemoving, null, teamToRemoveFrom);

            return returnMessage;
            //NOT USED IN THE CURRENT APPLICATION
        } 

        //private string AddBoardToTeam(ITeam teamToAddTo, IBoard boardForAdding)
        //{
        //    teamToAddTo.AddBoardToTeam(boardForAdding);
        //    string output = string.Format(ObjectAddedToTeam, nameof(Board), boardForAdding.BoardName, teamToAddTo.TeamName);
        //    this.AddHistoryEvent(output, board: boardForAdding, team: teamToAddTo);

        //    return output;
        //} 

        private string RemoveBoardFromTeam(ITeam teamToRemoveFrom, IBoard boardForRemoving)
        {
            teamToRemoveFrom.RemoveBoardFromTeam(boardForRemoving);
            string output = string.Format(ObjectRemovedFromTeam, nameof(Board), boardForRemoving.BoardName, teamToRemoveFrom.TeamName);
            this.AddHistoryEvent(output, board: boardForRemoving, team: teamToRemoveFrom);

            return output;
        } 

        //private string CreateFeedback(string feedbackTitle, string feedbackDescription, int raiting, IBoard board)
        //{
        //    var feedback = this.factory.CreateFeedback(feedbackTitle, feedbackDescription, raiting, board);
        //    if (feedback == null)
        //    {
        //        throw new ArgumentException(string.Format(ObjectExists, nameof(Feedback), feedback.Title));
        //    }
        //    board.AddWorkItemToBoard(feedback);

        //    string output = string.Format(ObjectCreated, nameof(Feedback), feedback.Title);

        //    this.AddHistoryEvent(output, board: board, team: board.Team);

        //    return output;
        //}  

        //private string CreateStory(string storyTitle, string storyDescription, PriorityType storyPriority, StorySizeType storySize, IBoard board, IPerson storyAssignee = null)
        //{
        //    var story = this.factory.CreateStory(storyTitle, storyDescription, storyPriority, storySize, board, storyAssignee);
        //    if (story == null)
        //    {
        //        throw new ArgumentException(string.Format(ObjectExists, nameof(Story), story.Title));
        //    }

        //    board.AddWorkItemToBoard(story);

        //    string output = string.Format(ObjectCreated, nameof(Story), story.Title);

        //    this.AddHistoryEvent(output, storyAssignee, board, board.Team);

        //    return output;
        //} 

        //private string CreateBug(string bugTitle, string bugDescription, List<string> stepsToReproduce, PriorityType bugPriority, BugSeverityType bugSeverity, IBoard board, IPerson bugAssignee = null)
        //{
        //    var bug = this.factory.CreateBug(bugTitle, bugDescription, stepsToReproduce, bugPriority, bugSeverity, board, bugAssignee);

        //    if (bug == null)
        //    {
        //        throw new ArgumentException(string.Format(ObjectExists, nameof(Bug), bug.Title));
        //    }

        //    board.AddWorkItemToBoard(bug);

        //    string output = string.Format(ObjectCreated, nameof(Bug), bug.Title);

        //    this.AddHistoryEvent(output, bugAssignee, board, board.Team);

        //    return output;
        //}

        //private string CreateComment(IWorkItem workitem, string message, IPerson author)
        //{
        //    var comment = this.factory.CreateComment(message, author);
        //    workitem.AddComment(comment);

        //    string output = string.Format(CommentAdded, comment.Message, comment.Author.PersonName, workitem.Title);

        //    this.AddHistoryEvent(output, author, workitem.Board, workitem.Board.Team, workitem);

        //    return output;
        //} //TODO:

        private void PrintReports(IList<string> reports)
        {
            var output = new StringBuilder();

            foreach (var report in reports)
            {
                output.AppendLine(report);
            }

            Console.Write(output.ToString());
            //writer.Write(output.ToString());
        }

        private IPerson GetPerson(string memberAsString)
        {
            var member = this.personList[memberAsString];
            return member;

        }

        private IPerson GetMember(ITeam team, string memberAsString)
        {
            if (!this.wimTeams.TeamsList.ContainsKey(team.TeamName))
            {
                throw new ArgumentException($"No {team.TeamName} team found!");
            }

            //var person = this.wimTeams.TeamsList
            //            .Where(x => x.Value == teamName)
            //            .Select(team => team.Value)
            //            .SelectMany(team => team.MemberList)
            //            .FirstOrDefault(member => member.PersonName == memberAsString);

            var person = this.personList[memberAsString];

            if (!team.MemberList.Contains(person))
            {
                throw new ArgumentNullException("person", $"There is no person with name {memberAsString} in the team.");
            }

            return person;
        }

        private ITeam GetTeam(string teamAsString)
        {
            var team = this.wimTeams[teamAsString];
            return team;
        }

        private IBoard GetBoard(string teamName, string boardAsString)
        {
            //var teamResult = this.wimTeams.TeamsList
            //                .Select(team => team.Value)
            //                .Where(team => team.BoardList.Keys.Any(board => board == boardAsString))
            //                .Single();
            var boardResult = wimTeams[teamName].BoardList[boardAsString];
            return boardResult;

        }

        private IWorkItem GetWorkItem(IBoard board, string workItemAsString)
        {
            return board.BoardWorkItems[workItemAsString];
        }

        private void AddHistoryEvent(string description, IPerson member = null, IBoard board = null, ITeam team = null, IWorkItem workItem = null)
        {
            var historyItem = this.factory.CreateHistoryItem(description, member, board, team, workItem);
            this.historyItemsList.AddHistoryItem(historyItem);
        }
    }
}
