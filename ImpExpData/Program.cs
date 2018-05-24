using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpExpData
{
    internal class Program
    {
        internal static readonly log4net.ILog Log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            Log.Info("ImpExpData started.");

            var arguments = new Arguments(args);


            Log.Info("ImpExpData finished.");
        }
    }
}
