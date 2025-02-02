using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Enigma.Tests;

public class MachineTests
{
	[Fact]
	public void UnsupportedCharacterTest()
    {
        const string message = "THIS IS A TEST MESSAGE WITH SPACES";
        
        var plugBoard = new PlugBoard()
            .SetWires(new Dictionary<char, char>
            {
                { 'T', 'A' },
                { 'S', 'B' }
            });
        
        var entryWheel = new EntryWheel(new EntryWheelConfiguration
        {
            EntryWheelPreset = EntryWheelPresets.Wehrmacht,
        });
        
        var rotor1 = new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Wehrmacht_I
            });

        var rotor2 = new Rotor(new RotorConfiguration
        {
            RotorPreset = RotorPresets.Wehrmacht_II,
            RingPosition = 10
        });

        var rotor3 = new Rotor(new RotorConfiguration
        {
            RotorPreset = RotorPresets.Wehrmacht_III,
            RingPosition = 13
        });

        var reflector = new Reflector(new ReflectorConfiguration
        {
            ReflectorPreset = ReflectorPresets.Wehrmacht_B
        });

        var enciphered = new StringBuilder();

        foreach (var c in message)
        {
            rotor1.Rotate();

            if (rotor1.IsAtNotch)
                rotor2.Rotate();

            if (rotor2.IsAtNotch)
                rotor3.Rotate();
            
            var cc = plugBoard.SendCharacter(c);

            cc = entryWheel.SendCharacter(cc);
            cc = rotor1.SendCharacter(cc);
            cc = rotor2.SendCharacter(cc);
            cc = rotor3.SendCharacter(cc);
            cc = reflector.SendCharacter(cc);
            cc = rotor3.ReflectedCharacter(cc);
            cc = rotor2.ReflectedCharacter(cc);
            cc = rotor1.ReflectedCharacter(cc);
            cc = entryWheel.ReflectedCharacter(cc);

            cc = plugBoard.SendCharacter(cc);
            
            enciphered.Append(cc);
        }

        Assert.NotEqual(message, enciphered.ToString());
        Assert.Equal(message.Length, enciphered.Length);
        
        var deciphered = new StringBuilder();

        rotor1.ResetRotation();
        rotor2.ResetRotation();
        rotor3.ResetRotation();

        foreach (var c in enciphered.ToString())
        {
            rotor1.Rotate();

            if (rotor1.IsAtNotch)
                rotor2.Rotate();

            if (rotor2.IsAtNotch)
                rotor3.Rotate();

            var cc = plugBoard.SendCharacter(c);

            cc = entryWheel.SendCharacter(cc);
            cc = rotor1.SendCharacter(cc);
            cc = rotor2.SendCharacter(cc);
            cc = rotor3.SendCharacter(cc);
            cc = reflector.SendCharacter(cc);
            cc = rotor3.ReflectedCharacter(cc);
            cc = rotor2.ReflectedCharacter(cc);
            cc = rotor1.ReflectedCharacter(cc);
            cc = entryWheel.ReflectedCharacter(cc);

            cc = plugBoard.SendCharacter(cc);

            deciphered.Append(cc);
        }
        
        Assert.Equal(message, deciphered.ToString());
    }
    
	[Fact]
	public void FullAsciiTest()
    {
        var message = new StringBuilder();
        var ci = 0;

        for (var i = 0; i < Constants.CharacterSetValues[CharacterSets.Ascii].Length * Constants.CharacterSetValues[CharacterSets.Ascii].Length * Constants.CharacterSetValues[CharacterSets.Ascii].Length; i++)
        {
            message.Append(Constants.CharacterSetValues[CharacterSets.Ascii][ci++]);

            if (ci == Constants.CharacterSetValues[CharacterSets.Ascii].Length)
                ci = 0;
        }
        
        var plugBoard = new PlugBoard()
            .SetWires(new Dictionary<char, char>
            {
                { 'T', 'A' },
                { 'C', 'B' }
            });

        var entryWheel = new EntryWheel(new EntryWheelConfiguration
        {
            EntryWheelPreset = EntryWheelPresets.Ascii,
        });
        
        var rotor1 = new Rotor(new RotorConfiguration
        {
            RotorPreset = RotorPresets.Ascii_I,
        });

        var rotor2 = new Rotor(new RotorConfiguration
        {
            RotorPreset = RotorPresets.Ascii_II,
            RingPosition = 10
        });

        var rotor3 = new Rotor(new RotorConfiguration
        {
            RotorPreset = RotorPresets.Ascii_III,
            RingPosition = 65
        });

        var reflector = new Reflector(new ReflectorConfiguration
        {
            ReflectorPreset = ReflectorPresets.Ascii
        });

        var enciphered = new StringBuilder();

        foreach (var c in message.ToString())
        {
            rotor1.Rotate();

            if (rotor1.IsAtNotch)
                rotor2.Rotate();

            if (rotor2.IsAtNotch)
                rotor3.Rotate();
        
            var cc = plugBoard.SendCharacter(c);

            cc = entryWheel.SendCharacter(cc);
            cc = rotor1.SendCharacter(cc);
            cc = rotor2.SendCharacter(cc);
            cc = rotor3.SendCharacter(cc);
            cc = reflector.SendCharacter(cc);
            cc = rotor3.ReflectedCharacter(cc);
            cc = rotor2.ReflectedCharacter(cc);
            cc = rotor1.ReflectedCharacter(cc);
            cc = entryWheel.ReflectedCharacter(cc);

            cc = plugBoard.SendCharacter(cc);
            
            enciphered.Append(cc);
        }

        Assert.False(message.Equals(enciphered));
        Assert.Equal(message.Length, enciphered.Length);
        
        var deciphered = new StringBuilder();

        rotor1.ResetRotation();
        rotor2.ResetRotation();
        rotor3.ResetRotation();

        foreach (var c in enciphered.ToString())
        {
            rotor1.Rotate();

            if (rotor1.IsAtNotch)
                rotor2.Rotate();

            if (rotor2.IsAtNotch)
                rotor3.Rotate();

            var cc = plugBoard.SendCharacter(c);

            cc = entryWheel.SendCharacter(cc);
            cc = rotor1.SendCharacter(cc);
            cc = rotor2.SendCharacter(cc);
            cc = rotor3.SendCharacter(cc);
            cc = reflector.SendCharacter(cc);
            cc = rotor3.ReflectedCharacter(cc);
            cc = rotor2.ReflectedCharacter(cc);
            cc = rotor1.ReflectedCharacter(cc);
            cc = entryWheel.ReflectedCharacter(cc);

            cc = plugBoard.SendCharacter(cc);

            deciphered.Append(cc);
        }
        
        Assert.True(message.Equals(deciphered));
    }
    
	[Fact]
	public void ManualClassicMachineTest()
    {
        const string message = "THIS IS A TEST MESSAGE WITH SPACES";

        var machine = new Machine()
            .AddPlugBoard(new Dictionary<char, char>
            {
                { 'T', 'A' },
                { 'S', 'B' }
            })
            .AddEntryWheel(new EntryWheelConfiguration
            {
                EntryWheelPreset = EntryWheelPresets.Wehrmacht,
            })
            .AddRotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Wehrmacht_I
            })
            .AddRotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Wehrmacht_II,
                RingPosition = 10
            })
            .AddRotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Wehrmacht_III,
                RingPosition = 13
            })
            .AddReflector(new ReflectorConfiguration
            {
                ReflectorPreset = ReflectorPresets.Wehrmacht_B
            });

        var enciphered = machine.Encipher(message);
        
        Assert.NotEqual(message, enciphered);
        Assert.Equal(message.Length, enciphered.Length);

        machine.Reset();
        
        var deciphered = machine.Encipher(enciphered);

        Assert.Equal(message, deciphered);
    }
    
	[Fact]
	public void ManualAsciiMachineTest()
    {
        var message = new StringBuilder();
        var ci = 0;

        for (var i = 0; i < Constants.CharacterSetValues[CharacterSets.Ascii].Length * Constants.CharacterSetValues[CharacterSets.Ascii].Length * Constants.CharacterSetValues[CharacterSets.Ascii].Length; i++)
        {
            message.Append(Constants.CharacterSetValues[CharacterSets.Ascii][ci++]);

            if (ci == Constants.CharacterSetValues[CharacterSets.Ascii].Length)
                ci = 0;
        }
        
        var machine = new Machine()
            .AddPlugBoard(new Dictionary<char, char>
            {
                { 'T', 'A' },
                { 'C', 'B' }
            })
            .AddEntryWheel(new EntryWheelConfiguration
            {
                EntryWheelPreset = EntryWheelPresets.Ascii,
            })
            .AddRotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Ascii_I,
                StartingRotation = 15
            })
            .AddRotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Ascii_II,
                RingPosition = 10,
                StartingRotation = 5
            })
            .AddRotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Ascii_III,
                RingPosition = 65,
                StartingRotation = 45
            })
            .AddReflector(new ReflectorConfiguration
            {
                ReflectorPreset = ReflectorPresets.Ascii
            });

        var enciphered = machine.Encipher(message.ToString());
        
        Assert.NotEqual(message.ToString(), enciphered);
        Assert.Equal(message.Length, enciphered.Length);

        machine.Reset();
        
        var deciphered = machine.Encipher(enciphered);

        Assert.Equal(message.ToString(), deciphered);
    }
    
    [Fact]
    public void GeneratedAsciiMachineTest()
    {
        var message = new StringBuilder();
        var ci = 0;

        for (var i = 0; i < Constants.CharacterSetValues[CharacterSets.Ascii].Length * Constants.CharacterSetValues[CharacterSets.Ascii].Length * Constants.CharacterSetValues[CharacterSets.Ascii].Length; i++)
        {
            message.Append(Constants.CharacterSetValues[CharacterSets.Ascii][ci++]);

            if (ci == Constants.CharacterSetValues[CharacterSets.Ascii].Length)
                ci = 0;
        }

        var machine = new Machine("ThisIsA32ByteLongSecretKey123456", "UniqueNonce12345");
        var enciphered = machine.Encipher(message.ToString());
        
        Assert.NotEqual(message.ToString(), enciphered);
        Assert.Equal(message.Length, enciphered.Length);

        machine.Reset();
        
        var deciphered = machine.Encipher(enciphered);

        Assert.Equal(message.ToString(), deciphered);
    }
    
    [Fact]
    public void MachinePresetTest()
    {
        var message = new StringBuilder();
        var ci = 0;

        for (var i = 0; i < Constants.CharacterSetValues[CharacterSets.Classic].Length * Constants.CharacterSetValues[CharacterSets.Classic].Length * Constants.CharacterSetValues[CharacterSets.Classic].Length * Constants.CharacterSetValues[CharacterSets.Classic].Length; i++)
        {
            message.Append(Constants.CharacterSetValues[CharacterSets.Classic][ci++]);

            if (ci == Constants.CharacterSetValues[CharacterSets.Classic].Length)
                ci = 0;
        }

        var machine = new Machine(new MachineConfiguration
        {
            MachinePreset = MachinePresets.Commercial_1924,
            Rotor1RingPosition = 15,
        });

        var enciphered = machine.Encipher(message.ToString());
        
        Assert.NotEqual(message.ToString(), enciphered);
        Assert.Equal(message.Length, enciphered.Length);

        machine.Reset();
        
        var deciphered = machine.Encipher(enciphered);

        Assert.Equal(message.ToString(), deciphered);
        
        machine = new Machine(new MachineConfiguration
        {
            MachinePreset = MachinePresets.Kriegsmarine_M4_1941,
            Rotor1RingPosition = 5,
            Rotor2RingPosition = 15,
            Rotor3RingPosition = 22,
            Rotor4RingPosition = 3,
            Rotor1StartingRotation = 10,
            Rotor2StartingRotation = 6,
            Rotor3StartingRotation = 17,
            Rotor4StartingRotation = 23,
            PlugBoardWires =
            {
                { 'A', 'T' },
                { 'B', 'V' },
                { 'C', 'M' },
                { 'D', 'O' },
                { 'E', 'Y' },
                { 'F', 'Q' },
                { 'G', 'R' },
                { 'H', 'S' },
                { 'I', 'L' },
                { 'J', 'K' },
            }
        });

        enciphered = machine.Encipher(message.ToString());
        
        Assert.NotEqual(message.ToString(), enciphered);
        Assert.Equal(message.Length, enciphered.Length);

        machine.Reset();
        
        deciphered = machine.Encipher(enciphered);

        Assert.Equal(message.ToString(), deciphered);
    }
}
