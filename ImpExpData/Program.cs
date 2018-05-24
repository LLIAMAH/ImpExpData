using ImpExpData.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using ImpExpData.Models;
using System.Linq;

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
                if (args == null || args.Length == 0)
                    throw new Exception("Invalid arguments.");

                var arguments = new Arguments(args);

                switch (arguments.Operation)
                {
                    case EOperation.Import:
                        {
                            var list = ReadAndParseFils(arguments.FileName);
                            ParseAndWriteToDb(list);
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

        private static void ParseAndWriteToDb(List<TextLine> list)
        {
            var listOfErrors = new List<string>();
            foreach (var item in list)
            {
                switch (item.LineType)
                {
                    case ELineType.LookupRecord:
                        {
                            var parsed = item.ParseLookup();
                            if (parsed != null)
                                WriteToDb(parsed, ref listOfErrors);

                            break;
                        }
                    case ELineType.CustomerRecord:
                        {
                            var parsed = item.ParseCustomer();
                            if(parsed != null)
                                WriteToDb(parsed, ref listOfErrors);

                            break;
                        }
                }
            }

            foreach(var item in listOfErrors)
            {
                Log.Error(item);
            }
        }

        private static void WriteToDb(Code data, ref List<string> errors)
        {
            using(var ctx = new Ctx())
            {
                var exist = ctx.Codes
                    .Where(o => o.CustomerTypeId == data.CustomerTypeId)
                    .SingleOrDefault();

                if (exist == null)
                {
                    ctx.Codes.Add(data);
                    try
                    {
                        ctx.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Data Id: {data.CustomerTypeId}, Value: {data.Value} error => {ex.Message}");
                    }
                }
            }
        }

        private static void WriteToDb(Customer data, ref List<string> errors)
        {
            using (var ctx = new Ctx())
            {
                var exist = ctx.Customers
                    .Where(o => o.Id == data.Id)
                    .SingleOrDefault();

                if (exist == null)
                {
                    ctx.Customers.Add(data);
                    try
                    {
                        ctx.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Data Id: {data.Id}, Name: {data.Name} error => {ex.Message}");
                    }
                }
            }
        }

        private static List<TextLine> ReadAndParseFils(string fileName)
        {
            var file = new FileInfo(fileName);
            if (!file.Exists)
                throw new Exception($"Error: Such file '{fileName}' is not exist.");

            var result = new List<TextLine>();
            using (var reader = file.OpenText())
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line))
                        continue;
                    else if (line.StartsWith("#"))
                        result.Add(new TextLine(ELineType.Comment, line));
                    else if (line.Contains(";"))
                    {
                        var r = new Regex(@"^;(\d+);[\s|\t];(\w+);$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
                        if (r.Match(line).Success)
                            result.Add(new TextLine(ELineType.LookupRecord, line));
                        else
                            result.Add(new TextLine(ELineType.CustomerRecord, line));
                    }
                    else
                        result.Add(new TextLine(ELineType.CorruptedLookupRecord, line));
                }

                reader.Close();
            }

            return result;
        }
    }
}