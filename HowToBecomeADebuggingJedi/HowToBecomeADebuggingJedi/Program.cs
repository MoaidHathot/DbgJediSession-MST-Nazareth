using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HowToBecomeADebuggingJedi;
using JediKnights;

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

            var jonSkeet = new SoftwareEngineer()
            {
                Name = "Jon Skeet",
                ID = 999,
                Team = "BestTeam",
                ProgrammingLanguages = new[] {"C#", "Java", "Python", "C++"}
            };

            var softwareEngineers = new[]
            {
                new SoftwareEngineer
                {
                    ID = 0,
                    Name = "Jon Skeet",
                    ProgrammingLanguages = new[] {"C#", "Groovy", "Java"},
                    Team = "Google"
                },
                new SoftwareEngineer
                {
                    ID = 1,
                    Name = "Stephen Cleary",
                    ProgrammingLanguages = new[] {"C#", "F#"},
                    Team = "TPL Async"
                },
                new SoftwareEngineer
                {
                    ID = 2,
                    Name = "Alon Fliess",
                    ProgrammingLanguages = new[] {"C#", "C++", "Assembly", "Java"},
                    Team = "CodeValue"
                },
                new SoftwareEngineer
                {
                    ID = 3,
                    Name = "Hehnselman",
                    ProgrammingLanguages = new[] {"C#", "F#"},
                    Team = "Microsoft"
                },
            };

            //var employees = Enumerable.Range(0, 10).Select(i => new Employee
            //{
            //    ID = i,
            //    Name = $"Jon Skeet {i}"
            //}).Concat(new []{jonSkeet}).ToArray();

            MyHashtable myHashTable = new MyHashtable();
            myHashTable.Add("one", 1);
            myHashTable.Add("two", 2);
            Console.WriteLine(myHashTable.ToString());
            Console.WriteLine("In Main.");

            var debugViewTest = new DebugViewTest();


            var jedi = new JediKnight(3000, ConsoleColor.Blue);

            Foo();


            var b = new B(42, "May the force be with you");

            Console.WriteLine("Press enter to quit.");
            Console.ReadKey();
        }

        public static void Foo()
        {

            var items = new[]
            {
                new JediKnight(3000, ConsoleColor.Green), new JediKnight(5000, ConsoleColor.Cyan),
                new JediKnight(1000, ConsoleColor.Magenta),
            };


            if (items.Any(item => item.MidiChlorians > 1000))
            {
                Debugger.Break();
            }
            




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

    //[DebuggerDisplay("Name = {Name}, ID = {ID}")]
    public class Employee
    {
        public string Name { get; set; }
        public int ID { get; set; }
    }

    [DebuggerDisplay("ID = {ID}, Name = {Name}, Team = {Team}")]
    [DebuggerTypeProxy(typeof(SoftwareEngineerDisplay))]
    public class SoftwareEngineer : Employee
    {
        public string[] ProgrammingLanguages { get; set; }
        public string Team { get; set; }
    }

    public class SoftwareEngineerDisplay
    {
        private readonly SoftwareEngineer _engineer;

        public SoftwareEngineerDisplay(SoftwareEngineer engineer)
        {
            _engineer = engineer;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int ID { get; set; }

        public string EngineerName => _engineer.Name;

        public string SoftwareTeam => _engineer.Team;

        public string Languages => _engineer.ProgrammingLanguages.Aggregate((a, b) => $"{a}, {b}");

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public string[] ProgrammingList => _engineer.ProgrammingLanguages;
    }

    public class SoftwareConsultant : SoftwareEngineer
    {
        public string Experties { get; set; }
    }

    public class ProgramManager : Employee
    {
        public string ProgramOwner { get; set; }
    }


    class DebugViewTest
    {
        // The following constant will appear in the debug window for DebugViewTest.
        string TabString = "    ";
        // The following DebuggerBrowsableAttribute prevents the property following it
        // from appearing in the debug window for the class.
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string y = "Test String";
    }

    [DebuggerDisplay("{value}", Name = "{key}")]
    internal class KeyValuePairs
    {
        private IDictionary dictionary;
        private object key;
        private object value;

        public KeyValuePairs(IDictionary dictionary, object key, object value)
        {
            this.value = value;
            this.key = key;
            this.dictionary = dictionary;
        }
    }

    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(HashtableDebugView))]
    class MyHashtable : Hashtable
    {
        private const string TestString = "This should not appear in the debug window.";

        internal class HashtableDebugView
        {
            private Hashtable hashtable;
            public const string TestString = "This should appear in the debug window.";
            public HashtableDebugView(Hashtable hashtable)
            {
                this.hashtable = hashtable;
            }

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public KeyValuePairs[] Keys
            {
                get
                {
                    KeyValuePairs[] keys = new KeyValuePairs[hashtable.Count];

                    int i = 0;
                    foreach (object key in hashtable.Keys)
                    {
                        keys[i] = new KeyValuePairs(hashtable, key, hashtable[key]);
                        i++;
                    }
                    return keys;
                }
            }
        }
    }
}
