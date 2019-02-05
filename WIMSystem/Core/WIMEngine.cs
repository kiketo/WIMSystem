using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Utils;
using WIMSystem.Core.Contracts;
using WIMSystem.Core.Utils;
using WIMSystem.Models;
using WIMSystem.Models.Abstract;
using WIMSystem.Models.Contracts;
using WIMSystem.Models.Enums;
using WIMSystem.Utils;

namespace WIMSystem.Core
{
    internal class WIMEngine : IWIMEngine
    {
        private const string InvalidCommand = "Invalid command name: {0}!";
        private const string ObjectExists = "{0} with name {1} already exists!";
        private const string ObjectCreated = "{0} with name {1} was created!";
        private const string ObjectDoesNotExist = "{0} {1} does not exist!";
        private const string ObjectAddedToTeam = "{0} {1} added to {2} team";
        private const string ObjectRemovedFromTeam = "{0} {1} removed from {2} team";
        private const string WorkItemAssigned = "{0} work item is assigned to {1} member";
        private const string WorkItemUnAssigned = "{0} work item is unassigned to {1} member";
        private const string WorkItemStatusChange = "{0} work item's status is changed to {1}";
        private const string FeedbackRatingChange = "{0} feedback's rating is changed to {1}";
        private const string WorkItemPriorityChange = "{0} work item's priority is changed to {1}";
        private const string StorySizeChange = "{0} story's size is changed to {1}";
        private const string BugSeverityChange = "{0} bug's severity is changed to {1}";
        private const string CommentAdded = "Comment: \"{0}\" with author: {1} added to \"{2}\".";





        private const char SPLIT_CHAR = ',';


        private readonly IFactory factory;
        private readonly IWIMTeams wimTeams;
        private readonly IPersonsCollection personList;
        private readonly IHistoryItemsCollection historyItemsList;

        public WIMEngine(IFactory factory, IWIMTeams wimTeams, IPersonsCollection personList, IHistoryItemsCollection historyItemsList)
        {
            this.factory = factory ?? throw new ArgumentException(string.Format(Consts.NULL_OBJECT, nameof(factory)));
            this.wimTeams = wimTeams ?? throw new ArgumentException(string.Format(Consts.NULL_OBJECT, nameof(factory)));
            this.personList = personList ?? throw new ArgumentException(string.Format(Consts.NULL_OBJECT, nameof(factory)));
            this.historyItemsList = historyItemsList ?? throw new ArgumentException(string.Format(Consts.NULL_OBJECT, nameof(factory)));
        }

        //public void Start()
        //{
        //    IList<ICommand> commands = new List<ICommand>();
        //    do
        //    {
        //        commands = this.commandParser.ReadCommands();
        //        var commandResult = this.ProcessCommands(commands);
        //        this.PrintReports(commandResult);
        //    }
        //    while (commands.Count > 0);

        //}

        public void ExecuteCommands(ICommandParser commandParser)
        {
            var commands = commandParser.ReadCommands();
            var commandResult = this.ProcessCommands(commands);
            this.PrintReports(commandResult);
        }

        private IList<string> ProcessCommands(IList<ICommand> commands)
        {
            var reports = new List<string>();

            foreach (var command in commands)
            {
                // try
                {
                    var report = this.ProcessSingleCommand(command);
                    reports.Add(report);
                }
                //  catch (Exception ex)
                {
                    //      reports.Add(ex.Message);
                }
            }

            return reports;
        }

        private string ProcessSingleCommand(ICommand command)
        {
            switch (command.Name)
            {

                case "CreateTeam":
                    {
                        var teamName = command.Parameters[0];
                        return this.CreateTeam(teamName);
                    }

                case "CreatePerson":
                    {
                        var personName = command.Parameters[0];
                        return this.CreatePerson(personName);
                    }

                case "CreateBoard":
                    {
                        var boardName = command.Parameters[0];
                        var team = this.GetTeam(command.Parameters[1]);
                        return this.CreateBoard(boardName, team);
                    }

                case "AddPersonToTeam":
                    {
                        var memberForAdding = this.GetPerson(command.Parameters[0]);
                        var teamToAddTo = this.GetTeam(command.Parameters[1]);
                        return this.AddMemberToTeam(memberForAdding, teamToAddTo);
                    }

                case "RemoveMemberFromTeam":
                    {
                        var teamToRemove = this.GetTeam(command.Parameters[0]);
                        var memberForRemoving = this.GetPerson(command.Parameters[1]);
                        return this.RemoveMemberFromTeam(teamToRemove, memberForRemoving);
                    }

                case "CreateBug":
                    {
                        var bugTitle = command.Parameters[0];
                        var bugDescription = command.Parameters[1];
                        var stepsToReproduce = command.Parameters[2].Trim().Split(SPLIT_CHAR).ToList();
                        var bugPriority = StringToEnum<PriorityType>.Convert(command.Parameters[3]);
                        var bugSeverity = StringToEnum<BugSeverityType>.Convert(command.Parameters[4]);
                        var teamName = command.Parameters[5];
                        var board = this.GetBoard(teamName, command.Parameters[6]);
                        //var bugAssignee = this.GetPerson(command.Parameters[7]);
                        //var bugComments = command.Parameters[6].Trim().Split(SPLIT_CHAR).ToList();

                        return this.CreateBug(bugTitle, bugDescription, stepsToReproduce, bugPriority, bugSeverity, board);
                    }

                case "CreateStory":
                    {
                        var storyTitle = command.Parameters[0];
                        var storyDescription = command.Parameters[1];
                        var storyPriority = StringToEnum<PriorityType>.Convert(command.Parameters[2]);
                        var storySize = StringToEnum<StorySizeType>.Convert(command.Parameters[3]);
                        var teamName = command.Parameters[4];
                        var board = this.GetBoard(teamName, command.Parameters[5]);
                        //var storyAssignee = this.GetPerson(command.Parameters[7]);

                        return this.CreateStory(storyTitle, storyDescription, storyPriority, storySize, board);//, storyAssignee);
                    }

                case "CreateFeedback":
                    {
                        var feedbackTitle = command.Parameters[0];
                        var feedbackDescription = command.Parameters[1];
                        var feedbackRating = int.Parse(command.Parameters[2]);
                        var teamName = command.Parameters[3];
                        var board = this.GetBoard(teamName, command.Parameters[4]);
                        return this.CreateFeedback(feedbackTitle, feedbackDescription, feedbackRating, board);
                    }

                //case "AddComment":
                //    {
                //        var teamName = this.GetTeam(command.Parameters[0]);
                //        var boardName = this.GetBoard(teamName.TeamName, command.Parameters[1]);
                //        var workItemTitle = this.GetWorkItem(boardName, command.Parameters[2]);
                //        var comment = command.Parameters[3];
                //        var authorName = this.Get

                //        return this.AddComment()
                //    }

                case "ShowAllPeople":
                    {
                        return this.ShowAllPeople();
                    }
                case "ShowPersonActivity":
                    {
                        var person = this.GetPerson(command.Parameters[0]);
                        return this.ShowPersonActivity(person);
                    }
                case "ShowAllTeam":
                    {
                        return this.ShowAllTeams();
                    }
                case "ShowTeamActivity":
                    {
                        var team = this.GetTeam(command.Parameters[0]);
                        return this.ShowTeamActivity(team);
                    }
                case "ShowAllTeamMembers":
                    {
                        var team = this.GetTeam(command.Parameters[0]);
                        return this.ShowAllTeamMembers(team);
                    }
                case "ShowAllTeamBoards":
                    {
                        var team = this.GetTeam(command.Parameters[0]);
                        return this.ShowAllTeamBoards(team);
                    }
                case "ShowBoardActivity":
                    {
                        var teamName = command.Parameters[0];
                        var board = this.GetBoard(teamName, command.Parameters[1]);
                        return this.ShowBoardActivity(board);
                    }
                case "ChangePriority":
                    {
                        var teamName = command.Parameters[0];
                        var board = this.GetBoard(teamName, command.Parameters[1]);
                        var workItem = this.GetAssignableWorkItem(board, command.Parameters[2]);
                        var priority = StringToEnum<PriorityType>.Convert(command.Parameters[3]);
                        return this.ChangePriority(workItem, priority);
                    }
                case "ChangeSeverityOfBug":
                    {
                        var teamName = command.Parameters[0];
                        var board = this.GetBoard(teamName, command.Parameters[1]);
                        var workItem = this.GetWorkItem(board, command.Parameters[2]);
                        var severity = StringToEnum<BugSeverityType>.Convert(command.Parameters[3]);
                        return this.ChangeSeverityOfBug(workItem, severity);
                    }
                case "ChangeStatus":
                    {
                        var teamName = command.Parameters[0];
                        var board = this.GetBoard(teamName, command.Parameters[1]);
                        var workItem = this.GetWorkItem(board, command.Parameters[2]);
                        var status = command.Parameters[3];
                        return this.ChangeStatus(workItem, status);
                    }
                case "ChangeSizeOfStory":
                    {
                        var teamName = command.Parameters[0];
                        var board = this.GetBoard(teamName, command.Parameters[1]);
                        var workItem = this.GetWorkItem(board, command.Parameters[2]);
                        var size = StringToEnum<StorySizeType>.Convert(command.Parameters[3]);
                        return this.ChangeSizeOfStory(workItem, size);
                    }
                case "ChangeRatingOfFeedback":
                    {
                        var teamName = command.Parameters[0];
                        var board = this.GetBoard(teamName, command.Parameters[1]);
                        var workItem = this.GetWorkItem(board, command.Parameters[2]);
                        var priority = int.Parse(command.Parameters[3]);
                        return this.ChangeRatingOfFeedback(workItem, priority);
                    }
                case "AssignWorkItemToMember":
                    {
                        var teamName = command.Parameters[0];
                        var board = this.GetBoard(teamName, command.Parameters[1]);
                        var workItem = this.GetAssignableWorkItem(board, command.Parameters[2]);
                        var member = this.GetPerson(command.Parameters[3]);
                        return this.AssignWorkItemToMember(workItem, member);
                    }
                case "UnassignWorkItem":
                    {
                        var teamName = command.Parameters[0];
                        var board = this.GetBoard(teamName, command.Parameters[1]);
                        var workItem = this.GetAssignableWorkItem(board, command.Parameters[2]);
                        return this.UnassignWorkItemToMember(workItem);
                    }
                case "ListBoardWorkItems":
                    {
                        var teamName = command.Parameters[0];
                        var board = this.GetBoard(teamName, command.Parameters[1]);
                        String filterStatus = null, sortBy = null;
                        IPerson filterAssignee = null;
                        Type filterType = null;

                        for (int i = 2; i < command.Parameters.Count; i++)
                        {
                            var paramOption = command.Parameters[i].Split(new[] { ':', }, StringSplitOptions.RemoveEmptyEntries);
                            switch (paramOption[0])
                            {
                                case "filterType":
                                    {
                                        var curAssembly = typeof(WIMEngine).Assembly;
                                        filterType = curAssembly.GetType("Models." + paramOption[1], false, true) ??
                                            throw new ArgumentException("Undefined type {0}", paramOption[1]);
                                        break;
                                    }
                                case "filterStatus":
                                    {
                                        filterStatus = command.Parameters[1];
                                        break;
                                    }
                                case "filterAssignee":
                                    {
                                        filterAssignee = this.GetPerson(paramOption[1]);
                                        break;
                                    }
                                case "sortBy":
                                    {
                                        sortBy = paramOption[1];
                                        break;
                                    }
                            }
                        }

                        return this.ListBoardWorkItems(board, filterType, filterStatus, filterAssignee, sortBy);
                    }

                default:
                    return string.Format(InvalidCommand, command.Name);
            }

        }

        private IAssignableWorkItem GetAssignableWorkItem(IBoard board, string assignableWorkItemTitle)
        {
            if (board.BoardWorkItems[assignableWorkItemTitle] is IAssignableWorkItem)
            {
                return (IAssignableWorkItem)board.BoardWorkItems[assignableWorkItemTitle];
            }
            throw new ArgumentException(string.Format($"{board.BoardWorkItems[assignableWorkItemTitle].GetType().Name} is not assignable work item!"));
        }

        private string UnassignWorkItemToMember(IAssignableWorkItem workItem)
        {

            if (Validators.IsNullValue(workItem))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
                    nameof(WorkItem)
                    ));
            }

            var member = workItem.Assignee;
            workItem.UnassignMember();
            member.MemberWorkItems.Remove(workItem);
            return string.Format(WorkItemUnAssigned, workItem.Title, member.PersonName);
        }

        private string AssignWorkItemToMember(IAssignableWorkItem workItem, IPerson member)
        {

            if (Validators.IsNullValue(workItem))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
                    nameof(WorkItem)
                    ));
            }
            if (Validators.IsNullValue(member))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
                    nameof(member)
                    ));
            }

            if (!member.IsAssignedToTeam)
            {
                throw new ArgumentException(string.Format($"{member.PersonName} is not a member of any team!"));

            }

            workItem.AssignMember(member);
            member.MemberWorkItems.Add(workItem);
            return string.Format(WorkItemAssigned, workItem.Title, member.PersonName);
        }

        private string ListBoardWorkItems(IBoard board, Type filterType, string filterStatus, IPerson filterAssignee, string sortBy)
        {
            return board.ListWorkItems(filterType, filterStatus, filterAssignee, sortBy);
        }

        private string ChangeRatingOfFeedback(IWorkItem workItem, int rating)
        {
            if (Validators.IsNullValue(workItem))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
                    nameof(WorkItem)
                    ));
            }
            if (!(workItem is IFeedback))
            {
                throw new ArgumentException(string.Format($"{workItem.GetType().Name} is not a {nameof(Feedback)}!"));
            }
            ((IFeedback)workItem).Rating = rating;
            return string.Format(FeedbackRatingChange, workItem.Title, rating);
        }

        private string ChangeSizeOfStory(IWorkItem workItem, StorySizeType size)
        {
            if (Validators.IsNullValue(workItem))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
                    nameof(WorkItem)
                    ));
            }
            if (!(workItem is IStory))
            {
                throw new ArgumentException(string.Format($"{workItem.GetType().Name} is not a {nameof(Story)}!"));
            }
            ((IStory)workItem).StorySize = size;
            return string.Format(StorySizeChange, workItem.Title, size);
        }

        private string ChangeStatus(IWorkItem workItem, string status)
        {
            if (Validators.IsNullValue(workItem))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
                    nameof(WorkItem)
                    ));
            }
            workItem.ChangeStatus(status);
            return string.Format(WorkItemStatusChange, workItem.Title, status);
        }

        private string ChangeSeverityOfBug(IWorkItem workItem, BugSeverityType severity)
        {
            if (Validators.IsNullValue(workItem))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
                    nameof(WorkItem)
                    ));
            }
            if (!(workItem is IBug))
            {
                throw new ArgumentException(string.Format($"{workItem.GetType().Name} is not a {nameof(Bug)}!"));
            }
            ((IBug)workItem).Severity = severity;
            return string.Format(WorkItemStatusChange, workItem.Title, severity);
        }

        private string ChangePriority(IAssignableWorkItem workItem, PriorityType priority)
        {
            if (Validators.IsNullValue(workItem))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
                    nameof(WorkItem)
                    ));
            }
            if (!(workItem is IAssignableWorkItem))
            {
                throw new ArgumentException(string.Format($"{workItem.GetType().Name} is not a {nameof(Feedback)}!"));
            }
            workItem.Priority = priority;
            return string.Format(WorkItemStatusChange, workItem.Title, priority);
        }

        private string ShowBoardActivity(IBoard board)
        {
            if (Validators.IsNullValue(board))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
                    nameof(board)
                    ));
            }
            return "";//TODO board.ShowBoardActivity();
        }

        private string ShowAllTeamBoards(ITeam team)
        {
            if (Validators.IsNullValue(team))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
                    nameof(team)
                    ));
            }
            return team.ShowAllTeamBoards();
        }

        private string ShowAllTeamMembers(ITeam team)
        {
            if (Validators.IsNullValue(team))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
                    nameof(team)
                    ));
            }
            return team.ShowAllTeamMembers();
        }

        private string ShowTeamActivity(ITeam team)
        {
            if (Validators.IsNullValue(team))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
                    nameof(team)
                    ));
            }
            return team.ShowAllTeamMembers();
        }

        private string ShowAllTeams()
        {
            return wimTeams.ShowAllTeams();
        }

        private string ShowPersonActivity(IPerson person)
        {
            if (Validators.IsNullValue(person))
            {
                throw new ArgumentException(string.Format(Consts.NULL_OBJECT,
                    nameof(person)
                    ));
            }
            return "";//TODO person.ShowPersonActivity();  
        }

        private string ShowAllPeople()
        {
            return "";//TODO this.personList.ShowAllPeople;
        }


        private string CreateTeam(string teamName)
        {
            if (this.wimTeams.Contains(teamName))
            {
                return string.Format(ObjectExists, nameof(Team), teamName);
            }

            var team = this.factory.CreateTeam(teamName, this.wimTeams);
            this.wimTeams.AddTeam(team);

            return string.Format(ObjectCreated, nameof(Team), teamName);
        }

        private string CreatePerson(string personName)
        {
            if (this.personList.Contains(personName))
            {
                throw new ArgumentException(string.Format(ObjectExists, nameof(Person), personName));
            }

            var person = this.factory.CreatePerson(personName);
            this.personList.AddPerson(person);

            return string.Format(ObjectCreated, nameof(Person), personName);
        }

        private string CreateBoard(string boardName, ITeam team)
        {
            var board = this.factory.CreateBoard(boardName, team);
            if (board == null)
            {
                throw new ArgumentException(string.Format(ObjectExists, nameof(Board), board));
            }
            team.AddBoardToTeam(board);

            return string.Format(ObjectCreated, nameof(Board), boardName);

        }

        private string AddMemberToTeam(IPerson memberForAdding, ITeam teamToAddTo)
        {
            teamToAddTo.AddMemberToTeam(memberForAdding);            
            return string.Format(ObjectAddedToTeam, nameof(Person), memberForAdding.PersonName, teamToAddTo.TeamName);
        }

        private string RemoveMemberFromTeam(ITeam teamToRemoveFrom, IPerson memberForRemoving)
        {
            teamToRemoveFrom.RemoveMemberFromTeam(memberForRemoving);
            return string.Format(ObjectRemovedFromTeam, nameof(Person), memberForRemoving.PersonName, teamToRemoveFrom.TeamName);

        }

        private string AddBoardToTeam(ITeam teamToAddTo, IBoard boardForAdding)
        {
            teamToAddTo.AddBoardToTeam(boardForAdding);
            return string.Format(ObjectAddedToTeam, nameof(Board), boardForAdding.BoardName, teamToAddTo.TeamName);


        }

        private string RemoveBoardFromTeam(ITeam teamToRemoveFrom, IBoard boardForRemoving)
        {
            teamToRemoveFrom.RemoveBoardFromTeam(boardForRemoving);
            return string.Format(ObjectRemovedFromTeam, nameof(Board), boardForRemoving.BoardName, teamToRemoveFrom.TeamName);

        }

        private string CreateFeedback(string feedbackTitle, string feedbackDescription, int raiting, IBoard board)
        {
            var feedback = this.factory.CreateFeedback(feedbackTitle, feedbackDescription, raiting, board);
            if (feedback == null)
            {
                throw new ArgumentException(string.Format(ObjectExists, nameof(Feedback), feedback.Title));
            }
            //team.AddBoardToTeam(board);
            board.AddWorkItemToBoard(feedback);

            return string.Format(ObjectCreated, nameof(Feedback), feedback.Title);


        }

        private string CreateStory(string storyTitle, string storyDescription, PriorityType storyPriority, StorySizeType storySize, IBoard board, IPerson storyAssignee = null)
        {
            var story = this.factory.CreateStory(storyTitle, storyDescription, storyPriority, storySize, board, storyAssignee);
            if (story == null)
            {
                throw new ArgumentException(string.Format(ObjectExists, nameof(Story), story.Title));
            }

            board.AddWorkItemToBoard(story);

            return string.Format(ObjectCreated, nameof(Story), story.Title);
        }

        private string CreateBug(string bugTitle, string bugDescription, List<string> stepsToReproduce, PriorityType bugPriority, BugSeverityType bugSeverity, IBoard board, IPerson bugAssignee=null)
        {
            var bug = this.factory.CreateBug(bugTitle, bugDescription, stepsToReproduce, bugPriority, bugSeverity, board, bugAssignee);

            if (bug == null)
            {
                throw new ArgumentException(string.Format(ObjectExists, nameof(Bug), bug.Title));
            }

            board.AddWorkItemToBoard(bug);

            return string.Format(ObjectCreated, nameof(Bug), bug.Title);
        }

        //private string AddComment(string )

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

        private IPerson GetMember(string teamAsString, string memberAsString)
        {
            var teamResult = this.wimTeams.TeamsList
                            .Select(team => team.Value)
                            .Where(team => team.MemberList.Any(member => member.PersonName == memberAsString))
                            .Single();
            return null;
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

    }
}
