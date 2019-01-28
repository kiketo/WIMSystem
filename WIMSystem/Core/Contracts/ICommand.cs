namespace WIMSystem.Core.Contracts
{
    using System.Collections.Generic;

    internal interface ICommand
    {
        string Name { get; }

        IList<string> Parameters { get; }
    }
}
