using System;
using System.Collections.Generic;
using WIMSystem.Commands.Contracts;
using WIMSystem.Core;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Commands.ListCommands
{
    public class ShowAllPeopleCommand : IEngineCommand
    {
        private readonly IPersonsCollection personList;

        //TODO Кико: OK ли е, при положение, че нямаме параметри за тази команда
        public string ReadSingleCommand(IList<string> parameters)
        {
            return Execute();
        }

        private string Execute()
        {
            string returnMessage;
            returnMessage = personList.ShowAllPeople();

            return returnMessage;
        }
    }
}
