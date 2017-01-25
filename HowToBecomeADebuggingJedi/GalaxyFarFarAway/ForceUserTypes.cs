using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyFarFarAway
{
    public enum LightsaberType
    {
        Lightsaber,
        DoubleLightsaber,
        CrossgaurdLightsaber
    }

    [Serializable]
    [DebuggerDisplay("{Color} {LightsaberType}", Name = "{LightsaberType}")]
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
    [DebuggerDisplay("{Name}")]
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
    [DebuggerDisplay("Jedi {Name} with {MidiChlorians} MidiChlorians fights with {Weapon.Color} {Weapon.LightsaberType}")]
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

    [Serializable]
    [DebuggerDisplay("Sith Lord {Name} with {MidiChlorians} MidiChlorians fights with {Weapon.Color} {Weapon.LightsaberType}")]
    //[DebuggerTypeProxy(typeof(SithLordDisplay))]
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
}
