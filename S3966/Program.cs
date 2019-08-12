using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace S2930
{
   class Program
   {
        static void Main(string[] args)
        {
            StreamWriter s = new StreamWriter("c:\temp.txt");
            s.Dispose();   // https://docs.microsoft.com/en-gb/dotnet/api/system.io.streamwriter.dispose?view=netframework-4.8
            s.Dispose();   // "...Dispose can be called multiple times by other objects...."

            // This IS calling only once and does not "violate" the rule
            StreamWriter s2 = new StreamWriter("c:\temp.txt");
            StreamWriter sw = new StreamWriter("c:\temp2.txt");
            s2.Dispose();
            sw.Dispose();


            // Via "using", where StreamReader disposes the "stream"
            using (var stream = File.Open("c:\temp2.txt", FileMode.Open, FileAccess.Read, FileShare.Write))
            {
                using (var reader = new StreamReader(stream))
                {
                }
            }

            // Without using, also called twice on FileStream (this does not raise issue, however it should)
            FileStream su = File.Open("c:\temp2.txt", FileMode.Open, FileAccess.Read, FileShare.Write);
            StreamReader r = new StreamReader(su);
            su.Dispose(); // Is not really necessary
            r.Dispose();
        }
   }
}
