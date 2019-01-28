using System;
using WIMSystem.Core.Contracts;

namespace WIMSystem.Core.Utils
{
    internal class ConsoleReader : IReader
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }
}
