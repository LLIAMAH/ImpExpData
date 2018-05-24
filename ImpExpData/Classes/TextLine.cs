using ImpExpData.Models;
using System;
using System.Text.RegularExpressions;

namespace ImpExpData.Classes
{
    internal enum ELineType
    {
        CustomerRecord,
        LookupRecord,
        Comment,
        CorruptedLookupRecord
    }

    internal class TextLine
    {
        internal ELineType LineType { get; private set; }
        internal string Value { get; private set; }

        public TextLine(ELineType type, string data)
        {
            this.LineType = type;
            this.Value = data;
        }

        internal Code ParseLookup()
        {
            var r = new Regex(@"^;[\s|\t];$");
            var splitted = r.Split(this.Value.Trim(new char[] { ';' }));
            if (splitted.Length == 2)
            {
                return new Code
                {
                    CustomerTypeId = long.Parse(splitted[0]),
                    Value = splitted[1]
                };
            }

            this.LineType = ELineType.CorruptedLookupRecord;
            return null;
        }

        internal Customer ParseCustomer()
        {
            var splitted = this.Value.Split(new char[] { ';' });
            long id = 0;
            string name = string.Empty;
            string notes = string.Empty;
            long codeId = 0;

            for (int i = 0; i < splitted.Length; i++)
            {
                switch (i)
                {
                    case 0: id = long.Parse(splitted[i]); break;
                    case 1: name = splitted[i]; break;
                    case 2: notes = splitted[i]; break;
                    case 3: codeId = long.Parse(splitted[i]); break;
                }
            }

            return new Customer
            {
                Id = id,
                Name = name,
                Notes = notes,
                CodeId = codeId
            };
        }
    }
}