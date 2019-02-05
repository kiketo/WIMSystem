using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Menu
{
    internal static class MainMenuItems
    {
        internal static IList<MenuItem> mainMenuItems = new List<MenuItem>()
        {
            new MenuItem("CreatePerson","* Create a new person","Enter person name:"),
            new MenuItem("ShowAllPeople","* Show all people", "People list:"),
            new MenuItem("ShowPersonActivity","* Show person's activity", "Person's activity:"),
            new MenuItem("CreateTeam","* Create a new team", "Enter team name:"),
            new MenuItem("ShowAllTeams","* Show all teams", "Team list:"),
            new MenuItem("ShowTeamActivity","* Show team's activity", "Team activity:"),
            new MenuItem("AddPersonToTeam","* Add person to team", "Enter parameters team name and member name:"),
            new MenuItem("ShowAllTeamMembers","* Show all team members", "Enter team name:"),
            new MenuItem("CreateBoard","* Create a new board in a team", "Enter board name and team name:"),
            new MenuItem("ShowAllTeamBoards","* Show all team boards", "Enter team name:"),
            new MenuItem("ShowBoardActivity","* Show board's activity", "Enter board name:"),
            new MenuItem("CreateBug","* Create a new Bug in a board", "Enter bug title, description, stepsto reproduce (divided by comma), priority, severity,team, board"),
            new MenuItem("CreateStory","* Create a new Story in a board", "Enter story title, description, priority, storySize, team, board:"),
            new MenuItem("CreateFeedback","* Create a new Feedback in a board", "Enter feedback title, description, rating, team, board"),
            new MenuItem("ChangeStatus","* Change Status of a work item", "Enter work item team name, board name, work item title, new status"),
            new MenuItem("ChangePriority","* Change Priority of a bug", "Enter bug team name, board name, work item title, new priority"),
            new MenuItem("ChangeSeverityOfBug","* Change Severity of a bug", "Enter bug team name, board name, work item title, new severity"),
            new MenuItem("ChangePriority","* Change Priority of a story", "Enter story team name, board name, work item title, new priority"),
            new MenuItem("ChangeSizeOfStory","* Change Size of a story", "Enter story team name, board name, work item title, new size"),
            new MenuItem("ChangeRatingOfFeedback","* Change Rating of a feedback", "Enter feedback team name, board name, work item title, new rating"),
            new MenuItem("AssignWorkItemToMember","* Assign work item to a person", "Enter team name, board name, work item title and member name"),
            new MenuItem("UnassignWorkItem","* Unassign work item to a person", "Enter work item team name, board name, work item title, member name"),
            new MenuItem("AddComment","* Add comment to a work item", "Enter work item team name, board name, work item title"),
            new MenuItem("ListBoardWorkItems","* List board items by filter and sort order",
                "List board items. Optional filter parameters: filterStatus:Bug/Story/Feedback, filterStatus:status, filterAssignee:name, sortBy:Title/Priority/Severity/Size/Ratin"),
            new MenuItem("BatchCommands","Enter multiple commands",""),
            new MenuItem("exit","****Exit****","")
        };

    }

}
