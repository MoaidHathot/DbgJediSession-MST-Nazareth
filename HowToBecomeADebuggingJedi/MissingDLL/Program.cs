using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GalaxyFarFarAway;

namespace MissingDLL
{
    public class Program
    {
        static void Main()
        {
            DebuggerDisplay.Program.Main();

            Console.WriteLine("Press enter to quit.");
            Console.ReadLine();
        }
    }
}
