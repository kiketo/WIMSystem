namespace Cosmetics.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using WIMSystem.Models.Enums;
    using WIMSystem.Models;
    using WIMSystem.Core.Contracts;
    using WIMSystem.Core.Utils;
    using WIMSystem.Models.Contracts;

    internal class WIMEngine
    {
        private const string InvalidCommand = "Invalid command name: {0}!";
        private const string ObjectExists = "{0} with name {1} already exists!";
        private const string ObjectCreated = "{0} with name {1} was created!";
        private const string ObjectDoesNotExist = "{0} {1} does not exist!";

        private const char SPLIT_CHAR = ',';


        private readonly IFactory factory;
        private readonly IWIMTeams wimTeams;
        private readonly IMembersCollection membersList;
        private readonly ICommandParser commandParser;


        public WIMEngine(IFactory factory, IWIMTeams wimTeams, IMembersCollection membersList, ICommandParser commandParser)
        {
            this.factory = factory;
            this.wimTeams = wimTeams;
            this.membersList = membersList;
            this.commandParser = commandParser;
        }

        public void Start()
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
                try
                {
                    var report = this.ProcessSingleCommand(command);
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
                        var teamName = command.Parameters[1];
                        return this.CreateBoard(boardName);
                    }
                    

                case "AddMemberToTeam":
                    {
                        var teamNameToAdd = command.Parameters[0];
                        var memberNameForAdding = command.Parameters[1];
                        return this.AddToTeam(teamNameToAdd, memberNameForAdding);
                    }

                case "RemoveMemberFromTeam":
                    {
                        var teamNameToRemove = command.Parameters[0];
                        var memberNameForRemoving = command.Parameters[1];
                        return this.RemoveFromTeam(teamNameToRemove, memberNameForRemoving);
                    }

                case "CreateBug":
                    {
                        var bugTitle = command.Parameters[0];
                        var bugDescription = command.Parameters[1];
                        var stepsToReproduce = command.Parameters[2].Trim().Split(SPLIT_CHAR).ToList();
                        var bugPriority = StringToEnum<PriorityType>.Convert(command.Parameters[3]);
                        var bugSeverity = StringToEnum<BugSeverityType>.Convert(command.Parameters[4]);
                        var bugAssignee = this.GetMember(command.Parameters[5]);
                        var bugComments = command.Parameters[6].Trim().Split(SPLIT_CHAR).ToList();
                        return this.CreateBug(bugTitle, bugDescription, stepsToReproduce, bugPriority, bugSeverity, bugAssignee, bugComments);
                    }

                case "CreateStory":
                    {
                        var storyTitle = command.Parameters[0];
                        var storyDescription = command.Parameters[1];
                        var storyPriority = StringToEnum<PriorityType>.Convert(command.Parameters[3]);
                        var storySize = StringToEnum<StorySizeType>.Convert(command.Parameters[4]);
                        var storyAssignee = this.GetMember(command.Parameters[5]);
                        var storyComments = command.Parameters[6].Trim().Split(SPLIT_CHAR).ToList();
                        return this.CreateStory(storyTitle, storyDescription, storyPriority, storySize, storyAssignee, storyComments);
                    }

                case "CreateFeedback":
                    {
                        var feedbackTitle = command.Parameters[0];
                        var feedbackDescription = command.Parameters[1];
                        var feedbackComments = command.Parameters[6].Trim().Split(SPLIT_CHAR).ToList();
                        return this.CreateFeedback(feedbackTitle, feedbackDescription, feedbackComments);
                    }

                default:
                    return string.Format(InvalidCommand, command.Name);
            }
        }

        private string CreateTeam(string teamName)
        {
            if (wimTeams.Contains(teamName))
            {
                return string.Format(ObjectExists, nameof(Team),teamName);
            }

            var team = this.factory.CreateTeam(teamName,null,null);
            wimTeams.AddTeam(team);

            return string.Format(ObjectCreated, nameof(Team),teamName);
        }

        private string CreateMember(string memberName)
        {
            if (wimTeams.Contains(memberName))
            {
                return string.Format(ObjectExists, nameof(Member), memberName);
            }

            var member = this.factory.CreateMember(memberName, null);
            membersList.AddMember(member);

            return string.Format(ObjectCreated, nameof(Member), memberName);
        }

        private string CreateBoard(string boardName)
        {
            throw new NotImplementedException();
        }

        private string AddToTeam(string teamNameToAdd, string memberNameForAdding)
        {
            throw new NotImplementedException();
        }

        private string RemoveFromTeam(string teamNameToRemove, string memberNameForRemoving)
        {
            throw new NotImplementedException();
        }

        private string CreateFeedback(string feedbackTitle, string feedbackDescription, List<string> feedbackComments)
        {
            throw new NotImplementedException();
        }

        private string CreateStory(string storyTitle, string storyDescription, PriorityType storyPriority, StorySizeType storySize, IMember storyAssignee, List<string> storyComments)
        {
            throw new NotImplementedException();
        }

        private string CreateBug(string bugTitle, string bugDescription, List<string> stepsToReproduce, PriorityType bugPriority, BugSeverityType bugSeverity, IMember bugAssignee, List<string> bugComments)
        {
            throw new NotImplementedException();
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

        private IMember GetMember(string memberAsString)
        {
            return membersList[memberAsString];
            
        }

    }
}
