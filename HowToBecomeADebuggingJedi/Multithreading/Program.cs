using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GalaxyFarFarAway;

namespace Multithreading
{
    class Program
    {
        static void Main(string[] args)
        {
            var jedies = new[]
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

            //Task.WaitAll(forceUsers.Select(async user => await Train(user)).ToArray());

            Parallel.Invoke(forceUsers.Select<IForceUser, Action>(user => () => Show(user.MidiChlorians)).ToArray());
        }

        static async Task Train(IForceUser user)
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
    }
}
