using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Commands.Utils
{
    internal class Consts
    {
        internal const string InvalidCommand = "Invalid command name: {0}!";
        internal const string ObjectExists = "{0} with name {1} already exists!";
        internal const string ObjectCreated = "{0} with name {1} was created!";
        internal const string ObjectDoesNotExist = "{0} {1} does not exist!";
        internal const string ObjectAddedToTeam = "{0} {1} added to team \"{2}\"!";
        internal const string ObjectRemovedFromTeam = "{0} {1} removed from team \"{2}\"!";
        internal const string WorkItemAssigned = "{0} work item is assigned to member {1}!";
        internal const string WorkItemUnAssigned = "{0} work item is unassigned from member {1}!";
        internal const string WorkItemStatusChange = "{0} work item's status is changed to {1}!";
        internal const string FeedbackRatingChange = "{0} feedback's rating is changed to {1}!";
        internal const string WorkItemPriorityChange = "{0} work item's priority is changed to {1}!";
        internal const string StorySizeChange = "{0} story's size is changed to {1}!";
        internal const string BugSeverityChange = "{0} bug's severity is changed to {1}!";
        internal const string CommentAdded = "Comment \"{0}\" with author {1} added to \"{2}\"!";
    }
}
