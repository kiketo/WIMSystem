using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIMSystem.Core.Contracts;
using WIMSystem.Core.Utils;
using WIMSystem.Models;
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
        private const string ObjectAddedToTeam = "{0} {1} added to {2} team";
        private const string ObjectRemovedFromTeam = "{0} {1} removed from {2} team";


        private const char SPLIT_CHAR = ',';


        private readonly IFactory factory;
        private readonly IWIMTeams wimTeams;
        private readonly IPersonsCollection membersList;

        public WIMEngine(IFactory factory, IWIMTeams wimTeams, IPersonsCollection membersList)
        {
            this.factory = factory;
            this.wimTeams = wimTeams;
            this.membersList = membersList;
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
                        var memberForAdding = this.GetMember(command.Parameters[1]);
                        return this.AddMemberToTeam(teamToAddTo, memberForAdding);
                    }

                case "RemoveMemberFromTeam":
                    {
                        var teamToRemove = this.GetTeam(command.Parameters[0]);
                        var memberForRemoving = this.GetMember(command.Parameters[1]);
                        return this.RemoveMemberFromTeam(teamToRemove, memberForRemoving);
                    }

                case "CreateBug":
                    {
                        var bugTitle = command.Parameters[0];
                        var bugDescription = command.Parameters[1];
                        var stepsToReproduce = command.Parameters[2].Trim().Split(SPLIT_CHAR).ToList();
                        var bugPriority = StringToEnum<PriorityType>.Convert(command.Parameters[3]);
                        var bugSeverity = StringToEnum<BugSeverityType>.Convert(command.Parameters[4]);
                        var board = this.GetBoard(command.Parameters[5]);
                        var bugAssignee = this.GetMember(command.Parameters[6]);
                        //var bugComments = command.Parameters[6].Trim().Split(SPLIT_CHAR).ToList();

                        return this.CreateBug(bugTitle, bugDescription, stepsToReproduce, bugPriority, bugSeverity, board, bugAssignee);
                    }

                case "CreateStory":
                    {
                        var storyTitle = command.Parameters[0];
                        var storyDescription = command.Parameters[1];
                        var storyPriority = StringToEnum<PriorityType>.Convert(command.Parameters[3]);
                        var storySize = StringToEnum<StorySizeType>.Convert(command.Parameters[4]);
                        var board = this.GetBoard(command.Parameters[5]);
                        var storyAssignee = this.GetMember(command.Parameters[6]);

                        return this.CreateStory(storyTitle, storyDescription, storyPriority, storySize, board, storyAssignee);
                    }

                case "CreateFeedback":
                    {
                        var feedbackTitle = command.Parameters[0];
                        var feedbackDescription = command.Parameters[1];
                        var feedbackRating = int.Parse(command.Parameters[2]);
                        var board = this.GetBoard(command.Parameters[3]);

                        return this.CreateFeedback(feedbackTitle, feedbackDescription, feedbackRating, board);
                    }

                default:
                    return string.Format(InvalidCommand, command.Name);
            }
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

            var member = this.factory.CreateMember(memberName, this.membersList);
            this.membersList.AddPerson(member);

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

        private IPerson GetMember(string memberAsString)
        {
            var member = this.membersList[memberAsString];
            return member;

        }

        private ITeam GetTeam(string teamAsString)
        {
            var team = this.wimTeams[teamAsString];
            return team;

        }

        private IBoard GetBoard(string boardAsString)
        {
            var teamResult = this.wimTeams.TeamsList
                            .Select(team => team.Value)
                            .Where(team => team.BoardList.Keys.Any(board => board == boardAsString))
                            .Single();
            var boardResult = teamResult.BoardList[boardAsString];
            return boardResult;

        }
    }
}
