using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HowToBecomeADebuggingJedi
{
    class Program
    {
        static void Main(string[] args)
        {

            //Foo();
            //A.Foo();
            //B.Bar();
            //B.Foo(new B());

            
            var b = new B(42, "May the force be with you");

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
            new Thread(() =>
            {
                Fibonaci();

            }).Start();

            new Thread(() =>
            {
                Fibonaci();

            }).Start();

            new Thread(() =>
            {
                Fibonaci();

            }).Start();

            new Thread(() =>
            {
                Fibonaci();

            }).Start();

            new Thread(() =>
            {
                Fibonaci();

            }).Start();

            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

            var task1 = Task.Factory.StartNew(() =>
            {
                while (true)
                    ;
            });

            var task2 = Task.Run(() =>
            {
                task1.Wait();
            });

            task1.ContinueWith(_ => task2.Wait());
        }

        private static void Fibonaci()
        {
            int a = 0, b = 1, c = 1;

            while (true)
            {
                var temp = c;
                c = a + b;
                a = b;
                b = temp;
            }
        }
    }

    [DebuggerDisplay("IntProperty_ = {IntProperty}, StringProperty = {StringProperty}", Name = "{StringProperty}")]
    class B
    {
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }

        public B()
        {
            
        }

        public B(int intProperty, string stringProperty)
        {
            IntProperty = intProperty;
            StringProperty = stringProperty;
        }

        public override string ToString() => $"Int: {IntProperty}, String: {StringProperty}";

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
