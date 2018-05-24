using ImpExpData.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ImpExpData
{
    internal class Program
    {
        internal static readonly log4net.ILog Log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            Log.Info("ImpExpData started.");
            try
            {
                var arguments = new Arguments(args);

                switch (arguments.Operation)
                {
                    case EOperation.Import:
                        {
                            ReadAndParseFils(arguments.FileName);
                            break;
                        };
                    case EOperation.Export: { break; };
                    default: { Log.Error("Unknown operation."); break; }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

            Log.Info("ImpExpData finished.");
        }

        private static void ReadAndParseFils(string fileName)
        {
            var file = new FileInfo(fileName);
            if (!file.Exists)
                throw new Exception($"Error: Such file '{fileName}' is not exist.");

            var listOfLines = new List<TextLine>();
            using (var reader = file.OpenText())
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("#"))
                        listOfLines.Add(new TextLine(ELineType.Comment, line));
                    else if (line.Contains(";"))
                    {
                        Regex r = new Regex(@"^;\d+; ;\w+;$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
                        if (r.Match(line).Success)
                            listOfLines.Add(new TextLine(ELineType.LookupRecord, line));
                        else
                            listOfLines.Add(new TextLine(ELineType.CustomerRecord, line));
                    }
                    else
                        listOfLines.Add(new TextLine(ELineType.Corrupted, line));
                }
                reader.Close();
            }
        }
    }
}
