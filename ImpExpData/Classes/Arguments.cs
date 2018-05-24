using System;
using System.Linq;

namespace ImpExpData.Classes
{
    public enum EOperation
    {
        Unknown = 0,
        Import,
        Export
    }

    public class Arguments
    {
        public EOperation Operation { get; private set; }
        public string FileName { get; private set; }

        public Arguments(string[] args)
        {
            var operation = args.Where(o => o.StartsWith("-"))
                .ToArray();

            var file = args.Where(o => o.EndsWith(".csv"))
                .ToArray();

            if (operation.Count() != 1)
                throw new Exception("Error: Wrong operation parameters count - it must be only one: '-import' or '-export'");

            if(file.Count() != 1)
                throw new Exception("Error: Must be one file with '.csv' extension.");

            this.FileName = file[0];

            switch (operation[0])
            {
                case "import": { this.Operation = EOperation.Import; }break;
                case "export": { this.Operation = EOperation.Export; } break;
                default: { throw new Exception("Error: Unknown operation's type: it must be '-import' or '-export'"); }
            }
        }
    }
}
