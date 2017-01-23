using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissingDLL
{
    public class Program
    {
        static void Main(string[] args)
        {
            DebuggerDisplay.Program.Main(args);

            Console.WriteLine("Press enter to quit.");
            Console.ReadLine();
        }
    }
}
