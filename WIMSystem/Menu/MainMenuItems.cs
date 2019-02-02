using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Menu
{
    internal static class MainMenuItems
    {
        internal static IList<MenuItem> mainMenuItems = new List<MenuItem>()
        {
            new MenuItem("CreateMember","* Create a new person","Enter parameter member name:"),
            new MenuItem("ShowAllMembers","* Show all people", "Members list:"),
            new MenuItem("ShowMembersActivity","* Show person's activity", "Members activity:"),
            new MenuItem("CreateTeam","* Create a new team", "Enter team name:"),
            new MenuItem("ShowAllTeams","* Show all teams", "Team list:"),
            new MenuItem("ShowTeamActivity","* Show team's activity", "Team activity:"),
            new MenuItem("AddMemberToTeam","* Add person to team", "Enter parameters team name and member name:"),
            new MenuItem("ShowAllTeamMembers","* Show all team members", "Enter parameter team name:"),
            new MenuItem("CreateNewBoard","* Create a new board in a team", "Enter parameters team name and board name:"),
            new MenuItem("ShowBoards","* Show all team boards", "Enter parameter team name:"),
            new MenuItem("ShowBoardActivity","* Show board's activity", "Enter board name:"),
            new MenuItem("","* Create a new Bug/Story/Feedback in a board", ""),
            new MenuItem("","* Change Priority/Severity/Status of a bug", ""),
            new MenuItem("","* Change Priority/Size/Status of a story", ""),
            new MenuItem("","* Change Rating/Status of a feedback", ""),
            new MenuItem("","* Assign work item to a person", ""),
            new MenuItem("","* Unassign work item to a person", ""),
            new MenuItem("","* Add comment to a work item", ""),
            new MenuItem("exit","****Exit****","")
        };

    }

}
