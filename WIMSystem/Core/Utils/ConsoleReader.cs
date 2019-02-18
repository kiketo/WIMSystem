using System;
using System.Collections.Generic;
using WIMSystem.Commands.Utils;
using WIMSystem.Core.Contracts;

namespace WIMSystem.Core.Utils
{
    internal class ConsoleReader : IReader
    {
        public ICollection<string> Read()
        {
            var result = new List<string>();
            while (true)
            {
                result.Add(Console.ReadLine());

                if (result[result.Count-1] == CommandsConsts.TerminationCommand)
                {
                    break;
                }
            }
            return result;
        }
    }
}
