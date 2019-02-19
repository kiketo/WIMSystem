﻿using System;
using System.Collections.Generic;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.Utils;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Commands.ListCommands
{
    public class ShowAllPeopleCommand : IEngineCommand
    {
        protected readonly IPersonsCollection personList;

        public ShowAllPeopleCommand(IPersonsCollection personList)
        {
            this.personList = personList ?? throw new ArgumentNullException(
                string.Format(CommandsConsts.NULL_OBJECT,
                nameof(personList)));
        }

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
