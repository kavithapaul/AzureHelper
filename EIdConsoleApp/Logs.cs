using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIdConsoleApp
{
    public static class Logs
    {
        private static string _Path = string.Empty;
        private static bool DEBUG = true;

        public static void Write(string msg)
        {
            if (_Path == String.Empty)
                _Path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            try
            {
                using (StreamWriter w = File.AppendText(Path.Combine(_Path, "log.txt")))
                {
                    Log(msg, w);
                }
                if (DEBUG)
                    Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static private void Log(string msg, TextWriter w)
        {
            try
            {
                w.Write(Environment.NewLine);
                w.Write("[{0} {1}]", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                w.Write("\t");
                w.WriteLine(" {0}", msg);
                w.WriteLine("-----------------------");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
