using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Core.Contracts;

namespace WIMSystem.Commands.Utils
{
    class PrintReports
    {
        readonly IWriter writer;

        public PrintReports(IWriter writer)
        {
            this.writer = writer;
        }

        private void Print(IList<string> reports)
        {
            var output = new StringBuilder();

            foreach (var report in reports)
            {
                output.AppendLine(report);
            }

            //Console.Write(output.ToString());
            writer.Write(output.ToString());
        }
    }
}
