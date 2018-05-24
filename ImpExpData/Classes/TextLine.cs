namespace ImpExpData.Classes
{
    internal enum ELineType
    {
        CustomerRecord,
        LookupRecord,
        Comment,
        Corrupted
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
    }
}
