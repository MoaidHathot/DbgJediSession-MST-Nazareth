using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GalaxyFarFarAway;
using Microsoft.VisualStudio.DebuggerVisualizers;

#region Debugger Display Attributes
[assembly: DebuggerDisplay("{Color} {LightsaberType}", Target = typeof(ForceUserWeapon), Name = "{LightsaberType}")]
 [assembly: DebuggerDisplay("Jedi {Name} with {MidiChlorians} MidiChlorians fights with {Weapon.Color} {Weapon.LightsaberType}", Target = typeof(JediKnight))]
 [assembly: DebuggerDisplay("Sith Lord {Name} with {MidiChlorians} MidiChlorians fights with {Weapon.Color} {Weapon.LightsaberType}", Target = typeof(SithLord))]
 [assembly: DebuggerDisplay("{Name}", Target = typeof(Human))]
#endregion Debugger Display Attributes

namespace GalaxyFarFarAway
{
    #region Types
    public enum LightsaberType
    {
        Lightsaber,
        DoubleLightsaber,
        CrossgaurdLightsaber
    }

    [Serializable]
    public class ForceUserWeapon
    {
        public LightsaberType LightsaberType { get; }
        public ConsoleColor Color { get; }

        public ForceUserWeapon(LightsaberType lightsaberType, ConsoleColor color)
        {
            LightsaberType = lightsaberType;
            Color = color;
        }
    }

    public interface IForceUser
    {
        int MidiChlorians { get; }
        ForceUserWeapon Weapon { get; }
    }

    [Serializable]
    public class Human
    {
        public string Name { get; }

        public Human(string name)
        {
            Name = name;
        }

        //public override string ToString() => Name;
    }

    [Serializable]
    public class JediKnight : Human, IForceUser
    {
        public int MidiChlorians { get; }
        public ForceUserWeapon Weapon { get; }

        public JediKnight(string name, int midiChlorians, ForceUserWeapon weapon)
            : base(name)
        {
            MidiChlorians = midiChlorians;
            Weapon = weapon;
        }
    }

    //[DebuggerTypeProxy(typeof(SithLordDisplay))]
    [Serializable]
    public class SithLord : Human, IForceUser
    {
        public int MidiChlorians { get; }
        public ForceUserWeapon Weapon { get; }

        public string[] Abilities { get; }

        public SithLord(string name, int midiChlorians, ForceUserWeapon weapon, params string[] abilities)
            : base(name)
        {
            MidiChlorians = midiChlorians;
            Weapon = weapon;
            Abilities = abilities;
        }

        class SithLordDisplay
        {
            private readonly SithLord _sithLord;

            public SithLordDisplay(SithLord sithLord)
            {
                _sithLord = sithLord;
            }

            public int MidiChlorians => _sithLord.MidiChlorians;
            public ForceUserWeapon Weapon => _sithLord.Weapon;

            //[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public string[] Abilities => _sithLord.Abilities;
        }
    }
    #endregion Types

    public class Program
    {
        public static void Main()
        {
            var jedies = new[]
            {
                new JediKnight("Yoda", 9001, new ForceUserWeapon(LightsaberType.Lightsaber, ConsoleColor.Green)),
                new JediKnight("Obi-Wan Kenobi", 5000, new ForceUserWeapon(LightsaberType.Lightsaber, ConsoleColor.Cyan)),
                new JediKnight("Luke Skywalker", 6000, new ForceUserWeapon(LightsaberType.Lightsaber, ConsoleColor.Cyan)),
                new JediKnight("Mace Windu", 3000, new ForceUserWeapon(LightsaberType.Lightsaber, ConsoleColor.Magenta))
            };

            var siths = new[]
            {
                new SithLord("Darth Vader", 15000, new ForceUserWeapon(LightsaberType.Lightsaber, ConsoleColor.Red), "Cool Voice", "Swag"),
                new SithLord("Darth Maul", 7000, new ForceUserWeapon(LightsaberType.DoubleLightsaber, ConsoleColor.Red)),
                new SithLord("Kylo Ren", 6500, new ForceUserWeapon(LightsaberType.CrossgaurdLightsaber, ConsoleColor.Red), "Freeze layzer rays", "Be stupid")
            };

            var forceUsers = jedies.Concat<IForceUser>(siths).ToList();

            #region BreakPoints
            //Task.WaitAll(Enumerable.Range(0, int.MaxValue).SelectMany(i => forceUsers.Select(async user => await Train(user))).ToArray());
            #endregion BreakPoints

            #region Multithreading
            ////Task.WaitAll(forceUsers.Select(async user => await Train(user)).ToArray());

            //Parallel.Invoke(forceUsers.Select<IForceUser, Action>(user => () => Show(user.MidiChlorians)).ToArray());
            #endregion Multithreading
        }

        #region BreakPoints      
        static async Task Train(IForceUser user)
        {
            Debug.WriteLine($"Started training {user}");
            await Task.Run(() => UseForce());
            Debug.WriteLine($"Finished training {user}");
        }

        public static void UseForce()
        {
            UseForce(TimeSpan.FromSeconds(1));
        }

        public static void UseForce(TimeSpan timeSpan)
        {
            Task.Delay(timeSpan).Wait();
        }

        [Conditional("DEBUG")]
        static void BreakPoint(bool condition = true)
        {
            if (condition)
            {
                Debugger.Break();
            }
        }
        #endregion BreakPoints

        #region DebuggerDisplay Visualizers
        public class LightsaberVisualizer : DialogDebuggerVisualizer
        {
            protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
            {
                var weapon = (ForceUserWeapon)objectProvider.GetObject();

                var window = new Window
                {
                    Title = $"{weapon.Color} {weapon.LightsaberType}",
                    Width = 400,
                    Height = 300
                };

                var colorsMap = new Dictionary<ConsoleColor, string>
                {
                    [ConsoleColor.Cyan] = "CyanLightsaber.png",
                    [ConsoleColor.Green] = "GreenLightsaber.jpg",
                    [ConsoleColor.Magenta] = "PurpleLightsaber.png",
                    [ConsoleColor.Red] = "RedLightsaber.png"
                };

                string imageName = null;

                switch (weapon.LightsaberType)
                {
                    case LightsaberType.Lightsaber:
                        imageName = colorsMap[weapon.Color];
                        break;
                    case LightsaberType.CrossgaurdLightsaber:
                        imageName = "RedCrossgaurdLightsaber.png";
                        break;
                    case LightsaberType.DoubleLightsaber:
                        imageName = "RedDoubleLightsaber.png";
                        break;
                }

                if (null != imageName)
                {
                    window.Background = new ImageBrush(new BitmapImage(new Uri($@"pack://application:,,,/{typeof(LightsaberVisualizer).Assembly.GetName().Name};component/Images/{imageName}")));
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                    window.ShowDialog();
                }
            }
        }

        public class HumanVisualizer : DialogDebuggerVisualizer
        {
            protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
            {
                var human = (Human)objectProvider.GetObject();

                var window = new Window
                {
                    Title = human.Name,
                    Width = 400,
                    Height = 300
                };

                var nameDictionary = new Dictionary<string, string>
                {
                    ["Yoda"] = "Yoda.jpg",
                    ["Obi-Wan Kenobi"] = "ObiWan.jpg",
                    ["Luke Skywalker"] = "LukeSkywalker.jpg",
                    ["Mace Windu"] = "MaceWindu.jpg",
                    ["Darth Vader"] = "DarthVader.png",
                    ["Darth Maul"] = "DarthMaul.jpg",
                    ["Kylo Ren"] = "KyloRen.jpg"
                };

                if (nameDictionary.ContainsKey(human.Name))
                {
                    var name = nameDictionary[human.Name];

                    window.Background = new ImageBrush(new BitmapImage(new Uri($@"pack://application:,,,/{typeof(HumanVisualizer).Assembly.GetName().Name};component/Images/{name}")));
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                    window.ShowDialog();
                }
            }
        }
        #endregion Debuggerdisplay Visualizers

        #region Multithreading
        static async Task TrainMultiThreading(IForceUser user)
        {
            Debug.WriteLine($"Started training {user}");
            await Task.Factory.StartNew(() => Task.Delay(TimeSpan.FromMinutes(2)).Wait(), TaskCreationOptions.LongRunning);
            Debug.WriteLine($"Finished training {user}");
        }

        static void Show(int x)
        {
            int z = x * Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine(z);

            Thread.Sleep(TimeSpan.FromSeconds(3));

            if (Thread.CurrentThread.ManagedThreadId < 10)
            {
                Task.Factory.StartNew(() => Show2(x)).Wait();
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                });
            }
        }

        static void Show2(int x)
        {
            int z = x * Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine(z);

            Thread.Sleep(TimeSpan.FromSeconds(3));

            if (Thread.CurrentThread.ManagedThreadId < 10)
            {
                Parallel.Invoke(() => Show(z));
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                });
            }
        }
        #endregion Multithreading
    }
}
