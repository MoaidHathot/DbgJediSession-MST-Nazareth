using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HowToBecomeADebuggingJedi
{
    class Program
    {
        static void Main(string[] args)
        {

            Foo();
            A.Foo();
            B.Bar();
            B.Foo(new B());
            
            Console.WriteLine("Press enter to quit.");
            Console.ReadKey();
        }

        public static void Foo()
        {
            
        }
    }

    class A
    {
        public static void Foo()
        {
            
        }
    }

    class B
    {
        public static void Foo(B b)
        {
            b.Baz();
        }

        public void Baz()
        {
            
        }

        public static int Bar()
        {
            
            
            return 42;
        }

    }
}
