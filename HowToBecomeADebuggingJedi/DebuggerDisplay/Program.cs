using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DebuggerDisplay;
using GalaxyFarFarAway;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: DebuggerVisualizer(typeof(LightsaberVisualizer), typeof(VisualizerObjectSource), Target = typeof(ForceUserWeapon), Description = "Lightsaber Weapon")]
[assembly: DebuggerVisualizer(typeof(HumanVisualizer), typeof(VisualizerObjectSource), Target = typeof(Human), Description = "Human")]

namespace DebuggerDisplay
{
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
        }
    }

    public class LightsaberVisualizer : DialogDebuggerVisualizer
    {
        //private int _state = 0;
        //private readonly string _assemblyName = typeof(LightsaberVisualizer).Assembly.GetName().Name;

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
}
