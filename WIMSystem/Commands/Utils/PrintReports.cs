using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Commands.Contracts;
using WIMSystem.Core.Contracts;

namespace WIMSystem.Commands.Utils
{
    internal class PrintReports : IPrintReports
    {
        private readonly IWriter writer;

        public PrintReports(IWriter writer)
        {
            this.writer = writer;
        }

        public void Print(IList<string> reports)
        {
            var output = new StringBuilder();

            foreach (var report in reports)
            {
                output.AppendLine(report);
            }

            //Console.Write(output.ToString());
            this.writer.Write(output.ToString());
        }
    }
}
