// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Enigma;

public class MachineConfiguration
{
    public MachinePresets MachinePreset { get; set; }
    
    public int Rotor1RingPosition { get; set; }
    public int Rotor1StartingRotation { get; set; }

    public int Rotor2RingPosition { get; set; }
    public int Rotor2StartingRotation { get; set; }

    public int Rotor3RingPosition { get; set; }
    public int Rotor3StartingRotation { get; set; }

    public int Rotor4RingPosition { get; set; }
    public int Rotor4StartingRotation { get; set; }
    
    public Dictionary<char,char> PlugBoardWires { get; } = [];
}