using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Core.Contracts;

namespace WIMSystem.Core
{
    class MenuCommandParser : ICommandParser
    {
        public IList<ICommand> ReadCommands()
        {
            return new List<ICommand>();   
        }
    }
}
