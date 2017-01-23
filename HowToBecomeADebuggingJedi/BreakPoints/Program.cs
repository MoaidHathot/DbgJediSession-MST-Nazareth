using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaxyFarFarAway;

namespace BreakPoints
{
    class Program
    {
        static void Main(string[] args)
        {
            var jedies = new []
            {
                new JediKnight("Yoda", 9001, new ForceUserWeapon(LightsaberType.Lightsaber, ConsoleColor.Green)),
                new JediKnight("Obi-Wan Kenobi", 5000, new ForceUserWeapon(LightsaberType.Lightsaber, ConsoleColor.Cyan)),
                new JediKnight("Luke Skywalker", 6000, new ForceUserWeapon(LightsaberType.Lightsaber, ConsoleColor.Cyan)),
                new JediKnight("Mace Windu ", 3000, new ForceUserWeapon(LightsaberType.Lightsaber, ConsoleColor.Magenta)),
            };

            var siths = new[]
            {
                new SithLord("Darth Vader", 15000, new ForceUserWeapon(LightsaberType.Lightsaber, ConsoleColor.Red)),
                new SithLord("Darth Maul", 7000, new ForceUserWeapon(LightsaberType.DoubleLightsaber, ConsoleColor.Red)),
                new SithLord("Kylo Ren", 6500, new ForceUserWeapon(LightsaberType.CrossgaurdLightsaber, ConsoleColor.Red)),
            };

            var forceUsers = jedies.Concat<IForceUser>(siths).ToList();

            Task.WaitAll(Enumerable.Range(0, int.MaxValue).SelectMany(i => forceUsers.Select(async user => await Train(user))).ToArray());
        }

        static async Task Train(IForceUser user)
        {
            Debug.WriteLine($"Started training {user}");
            await Task.Delay(TimeSpan.FromSeconds(1));
            Debug.WriteLine($"Finished training {user}");
        }
    }
}
