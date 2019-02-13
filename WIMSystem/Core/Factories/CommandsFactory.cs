using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Utils;
using WIMSystem.Commands.Contracts;
using WIMSystem.Core.Factories.Contracts;

namespace WIMSystem.Core.Factories
{
    public class CommandsFactory : ICommandsFactory
    {
        readonly IContainerResolver<IEngineCommand> containerResolver;

        public CommandsFactory(IContainerResolver<IEngineCommand> containerResolver)
        {
            this.containerResolver = containerResolver ?? throw new ArgumentNullException(nameof(containerResolver));
        }

        public IEngineCommand GetCommand(string commandName)
        {
            if (Validators.IsNullorEmptyValue(commandName))
            {
                throw new ArgumentException(); //TODO
            }
           return containerResolver.GetService(commandName);
        }
    }
}
