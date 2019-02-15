using System.Collections.Generic;

namespace WIMSystem.Commands.Contracts
{
    interface IPrintReports
    {
        void Print(IList<string> reports);
    }
}