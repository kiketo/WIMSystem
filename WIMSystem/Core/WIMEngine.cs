﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Utils;
using WIMSystem.Core.Contracts;
using WIMSystem.Core.Utils;
using WIMSystem.Models;
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

                case "CreateMember":
                    {
                        var memberName = command.Parameters[0];
                        return this.CreateMember(memberName);
                    }

                case "CreateBoard":
                    {
                        var boardName = command.Parameters[0];
                        var team = this.GetTeam(command.Parameters[1]);
                        return this.CreateBoard(boardName, team);
                    }

                case "AddMemberToTeam":
                    {
                        var teamToAddTo = this.GetTeam(command.Parameters[0]);
                        var memberForAdding = this.GetPerson(command.Parameters[1]);
                        return this.AddMemberToTeam(teamToAddTo, memberForAdding);
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
                        var bugAssignee = this.GetPerson(command.Parameters[7]);
                        //var bugComments = command.Parameters[6].Trim().Split(SPLIT_CHAR).ToList();

                        return this.CreateBug(bugTitle, bugDescription, stepsToReproduce, bugPriority, bugSeverity, board, bugAssignee);
                    }

                case "CreateStory":
                    {
                        var storyTitle = command.Parameters[0];
                        var storyDescription = command.Parameters[1];
                        var storyPriority = StringToEnum<PriorityType>.Convert(command.Parameters[3]);
                        var storySize = StringToEnum<StorySizeType>.Convert(command.Parameters[4]);
                        var teamName = command.Parameters[5];
                        var board = this.GetBoard(teamName, command.Parameters[6]);
                        var storyAssignee = this.GetPerson(command.Parameters[7]);

                        return this.CreateStory(storyTitle, storyDescription, storyPriority, storySize, board, storyAssignee);
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
                        var teamName = command.Parameters[0];
                        var board = this.GetBoard(teamName, command.Parameters[1]);
                        return this.ShowAllTeamBoards(board);
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
                        var workItem = this.GetWorkItem(board, command.Parameters[2]);
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
                case "ChangeStatusOfBug":
                    {
                        var teamName = command.Parameters[0];
                        var board = this.GetBoard(teamName, command.Parameters[1]);
                        var workItem = this.GetWorkItem(board, command.Parameters[2]);
                        var status = StringToEnum<BugStatusType>.Convert(command.Parameters[3]);
                        return this.ChangeStatusOfBug(workItem, status);
                    }
                case "ChangeSizeOfStory":
                    {
                        var teamName = command.Parameters[0];
                        var board = this.GetBoard(teamName, command.Parameters[1]);
                        var workItem = this.GetWorkItem(board, command.Parameters[2]);
                        var size = StringToEnum<StorySizeType>.Convert(command.Parameters[3]);
                        return this.ChangeSizeOfStory(workItem, size);
                    }
                case "ChangeStatusOfStory":
                    {
                        var teamName = command.Parameters[0];
                        var board = this.GetBoard(teamName, command.Parameters[1]);
                        var workItem = this.GetWorkItem(board, command.Parameters[2]);
                        var status = StringToEnum<StoryStatusType>.Convert(command.Parameters[3]);
                        return this.ChangeStatusOfStory(workItem, status);
                    }
                case "ChangeRatingOfFeedback":
                    {
                        var teamName = command.Parameters[0];
                        var board = this.GetBoard(teamName, command.Parameters[1]);
                        var workItem = this.GetWorkItem(board, command.Parameters[2]);
                        var priority = int.Parse(command.Parameters[3]);
                        return this.ChangeRatingOfFeedback(workItem, priority);
                    }
                case "ChangeStatusOfFeedback":
                    {
                        var teamName = command.Parameters[0];
                        var board = this.GetBoard(teamName, command.Parameters[1]);
                        var workItem = this.GetWorkItem(board, command.Parameters[2]);
                        var status = StringToEnum<FeedbackStatusType>.Convert(command.Parameters[3]);
                        return this.ChangeStatusOfFeedback(workItem, status);
                    }
                case "AssignWorkItemToMember":
                    {
                        var teamName = command.Parameters[0];
                        var board = this.GetBoard(teamName, command.Parameters[1]);
                        var workItem = this.GetWorkItem(board, command.Parameters[2]);
                        var member = this.GetPerson(command.Parameters[3]);
                        return this.AssignWorkItemToMember(workItem, member);
                    }
                case "UnassignWorkItemToMember":
                    {
                        var teamName = command.Parameters[0];
                        var board = this.GetBoard(teamName, command.Parameters[1]);
                        var workItem = this.GetWorkItem(board, command.Parameters[2]);
                        var member = this.GetPerson(command.Parameters[3]);
                        return this.UnassignWorkItemToMember(workItem, member);
                    }
                case "ListBoardWorkItems":
                    {
                        var teamName = command.Parameters[0];
                        var board = this.GetBoard(teamName, command.Parameters[1]);
                        //var paramFilter = command.Parameters[2].Split(new[] { ':', }, StringSplitOptions.RemoveEmptyEntries);
                        //if (paramFilter[0] != "filterType") throw new ArgumentException("Type parameter is mandatory for work item filter!");
                        //var filterByType = this.NormalizeParameter(paramFilter[1]);
                        //var curAssembly = typeof(WIMEngine).Assembly;
                        //var typeOfWorkItem = curAssembly.GetType("Models." + command.Parameters[2], true, true);
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
                                        //PropertyInfo myPropInfo = filterType.GetProperty(filterType.Name + "Status");
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

        private string UnassignWorkItemToMember(IWorkItem workItem, IPerson member)
        {
            throw new NotImplementedException();
        }

        private string AssignWorkItemToMember(IWorkItem workItem, IPerson member)
        {
            throw new NotImplementedException();
        }

        private string ListBoardWorkItems(IBoard board, Type filterType, string filterStatus, IPerson filterAssignee, string sortBy)
        {
            throw new NotImplementedException();
        }

        private object NormalizeParameter(string v)
        {
            throw new NotImplementedException();
        }

        private string ChangeStatusOfFeedback(IWorkItem workItem, FeedbackStatusType status)
        {
            throw new NotImplementedException();
        }

        private string ChangeRatingOfFeedback(IWorkItem workItem, int priority)
        {
            throw new NotImplementedException();
        }

        private string ChangeStatusOfStory(IWorkItem workItem, StoryStatusType status)
        {
            throw new NotImplementedException();
        }

        private string ChangeSizeOfStory(IWorkItem workItem, StorySizeType size)
        {
            throw new NotImplementedException();
        }

        private string ChangeStatusOfBug(IWorkItem workItem, BugStatusType status)
        {
            throw new NotImplementedException();
        }

        private string ChangeSeverityOfBug(IWorkItem workItem, BugSeverityType severity)
        {
            throw new NotImplementedException();
        }

        private string ShowBoardActivity(IBoard board)
        {
            throw new NotImplementedException();
        }

        private string ShowAllTeamBoards(IBoard board)
        {
            throw new NotImplementedException();
        }

        private string ShowAllTeamMembers(ITeam team)
        {
            throw new NotImplementedException();
        }

        private string ShowTeamActivity(ITeam team)
        {
            throw new NotImplementedException();
        }

        private string ShowAllTeams()
        {
            throw new NotImplementedException();
        }

        private string ShowPersonActivity(IPerson person)
        {
            throw new NotImplementedException();
        }

        private string ShowAllPeople()
        {
            throw new NotImplementedException();
        }

        private string ChangePriority(IWorkItem workItem, PriorityType newPriority)
        {
            throw new NotImplementedException();
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

        private string CreateMember(string memberName)
        {
            if (this.wimTeams.Contains(memberName))
            {
                throw new ArgumentException(string.Format(ObjectExists, nameof(Person), memberName));
            }

            var member = this.factory.CreateMember(memberName, this.personList);
            this.personList.AddPerson(member);

            return string.Format(ObjectCreated, nameof(Person), memberName);
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

        private string AddMemberToTeam(ITeam teamToAddTo, IPerson memberForAdding)
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

        private string CreateBug(string bugTitle, string bugDescription, List<string> stepsToReproduce, PriorityType bugPriority, BugSeverityType bugSeverity, IBoard board, IPerson bugAssignee)
        {
            var bug = this.factory.CreateBug(bugTitle, bugDescription, stepsToReproduce, bugPriority, bugSeverity, board, bugAssignee);
            if (bug == null)
            {
                throw new ArgumentException(string.Format(ObjectExists, nameof(Bug), bug.Title));
            }

            board.AddWorkItemToBoard(bug);

            return string.Format(ObjectCreated, nameof(Bug), bug.Title);
        }

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

        private ITeam GetTeam(string teamAsString)
        {
            var team = this.wimTeams[teamAsString];
            return team;

        }

        private IBoard GetBoard(string teamName, string boardAsString)
        {
            var teamResult = this.wimTeams.TeamsList
                            .Select(team => team.Value)
                            .Where(team => team.BoardList.Keys.Any(board => board == boardAsString))
                            .Single();
            var boardResult = teamResult.BoardList[boardAsString];
            return boardResult;

        }

        private IWorkItem GetWorkItem(IBoard board, string workItemAsString)
        {
            return null;
        }

    }
}
