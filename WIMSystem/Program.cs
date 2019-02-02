using System;
using WIMSystem.Core;
using WIMSystem.Models;
using WIMSystem.Models.Contracts;

namespace WIMSystem
{
    class Program
    {
        static void Main()
        {
            var factory = new Factory();
            var teamList = WIMTeams.Instance;
            var memberLits = MembersCollection.Instance;
            var commandParser = new ConsoleCommandParser();
            var engine = new WIMEngine(factory,teamList,memberLits,commandParser);

            engine.Start();

        }
    }
}
