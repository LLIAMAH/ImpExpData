using ImpExpData.Classes;
using System;

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
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
            }

            Log.Info("ImpExpData finished.");
        }
    }
}
