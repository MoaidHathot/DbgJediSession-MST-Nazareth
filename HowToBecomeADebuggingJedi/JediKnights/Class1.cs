using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JediKnights
{
    public abstract class ForceUser
    {
        public int MidiChlorians { get; }
        public ConsoleColor LihtSaberColor { get; }

        public ForceUser(int midiChlorians, ConsoleColor lihtSaberColor)
        {
            MidiChlorians = midiChlorians;
            LihtSaberColor = lihtSaberColor;
        }
    }

    public class JediKnight : ForceUser
    {
        public JediKnight(int midiChlorians, ConsoleColor lihtSaberColor)
            : base(midiChlorians, lihtSaberColor)
        {
            
        }
    }

    public class SithLord : ForceUser
    {
        public SithLord(int midiChlorians, ConsoleColor lihtSaberColor)
            : base(midiChlorians, lihtSaberColor)
        {

        }
    }
}
