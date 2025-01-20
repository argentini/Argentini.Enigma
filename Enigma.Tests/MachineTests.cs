using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Enigma.Tests;

public class MachineTests
{
	[Fact]
	public void UnsupportedCharacterTest()
    {
        const string Message = "THIS IS A TEST MESSAGE WITH SPACES";
        
        var plugBoard = new PlugBoard()
            .SetWires(new Dictionary<char, char>
            {
                { 'T', 'A' },
                { 'S', 'B' }
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

        foreach (var c in Message)
        {
            rotor1.Rotate();

            if (rotor1.IsAtNotch)
                rotor2.Rotate();

            if (rotor2.IsAtNotch)
                rotor3.Rotate();
            
            var cc = plugBoard.SendCharacter(c);

            cc = rotor1.SendCharacter(cc);
            cc = rotor2.SendCharacter(cc);
            cc = rotor3.SendCharacter(cc);
            cc = reflector.SendCharacter(cc);
            cc = rotor3.ReflectedCharacter(cc);
            cc = rotor2.ReflectedCharacter(cc);
            cc = rotor1.ReflectedCharacter(cc);
            cc = plugBoard.SendCharacter(cc);
            
            enciphered.Append(cc);
        }

        Assert.NotEqual(Message, enciphered.ToString());
        Assert.Equal(Message.Length, enciphered.Length);
        
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

            cc = rotor1.SendCharacter(cc);
            cc = rotor2.SendCharacter(cc);
            cc = rotor3.SendCharacter(cc);
            cc = reflector.SendCharacter(cc);
            cc = rotor3.ReflectedCharacter(cc);
            cc = rotor2.ReflectedCharacter(cc);
            cc = rotor1.ReflectedCharacter(cc);
            cc = plugBoard.SendCharacter(cc);

            deciphered.Append(cc);
        }
        
        Assert.Equal(Message, deciphered.ToString());
    }
    
	[Fact]
	public void FullAsciiTest()
    {
        var Message = new StringBuilder();
        var ci = 0;

        for (var i = 0; i < Constants.CharacterSetValues[CharacterSets.Ascii].Length * Constants.CharacterSetValues[CharacterSets.Ascii].Length * Constants.CharacterSetValues[CharacterSets.Ascii].Length; i++)
        {
            Message.Append(Constants.CharacterSetValues[CharacterSets.Ascii][ci++]);

            if (ci == Constants.CharacterSetValues[CharacterSets.Ascii].Length)
                ci = 0;
        }
        
        var plugBoard = new PlugBoard()
            .SetWires(new Dictionary<char, char>
            {
                { 'T', 'A' },
                { 'C', 'B' }
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

        foreach (var c in Message.ToString())
        {
            rotor1.Rotate();

            if (rotor1.IsAtNotch)
                rotor2.Rotate();

            if (rotor2.IsAtNotch)
                rotor3.Rotate();
        
            var cc = plugBoard.SendCharacter(c);

            cc = rotor1.SendCharacter(cc);
            cc = rotor2.SendCharacter(cc);
            cc = rotor3.SendCharacter(cc);
            cc = reflector.SendCharacter(cc);
            cc = rotor3.ReflectedCharacter(cc);
            cc = rotor2.ReflectedCharacter(cc);
            cc = rotor1.ReflectedCharacter(cc);
            cc = plugBoard.SendCharacter(cc);
            
            enciphered.Append(cc);
        }

        Assert.False(Message.Equals(enciphered));
        Assert.Equal(Message.Length, enciphered.Length);
        
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

            cc = rotor1.SendCharacter(cc);
            cc = rotor2.SendCharacter(cc);
            cc = rotor3.SendCharacter(cc);
            cc = reflector.SendCharacter(cc);
            cc = rotor3.ReflectedCharacter(cc);
            cc = rotor2.ReflectedCharacter(cc);
            cc = rotor1.ReflectedCharacter(cc);
            cc = plugBoard.SendCharacter(cc);

            deciphered.Append(cc);
        }
        
        Assert.True(Message.Equals(deciphered));
    }
    
	[Fact]
	public void ClassicMachineTest()
    {
        const string Message = "THIS IS A TEST MESSAGE WITH SPACES";

        var machine = new Machine()
            .AddPlugBoard(new Dictionary<char, char>
            {
                { 'T', 'A' },
                { 'S', 'B' }
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

        var enciphered = machine.Encipher(Message);
        
        Assert.NotEqual(Message, enciphered);
        Assert.Equal(Message.Length, enciphered.Length);

        machine.Reset();
        
        var deciphered = machine.Encipher(enciphered);

        Assert.Equal(Message, deciphered);
    }
    
	[Fact]
	public void AsciiMachineTest()
    {
        var Message = new StringBuilder();
        var ci = 0;

        for (var i = 0; i < Constants.CharacterSetValues[CharacterSets.Ascii].Length * Constants.CharacterSetValues[CharacterSets.Ascii].Length * Constants.CharacterSetValues[CharacterSets.Ascii].Length; i++)
        {
            Message.Append(Constants.CharacterSetValues[CharacterSets.Ascii][ci++]);

            if (ci == Constants.CharacterSetValues[CharacterSets.Ascii].Length)
                ci = 0;
        }
        
        var machine = new Machine()
            .AddPlugBoard(new Dictionary<char, char>
            {
                { 'T', 'A' },
                { 'C', 'B' }
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

        var enciphered = machine.Encipher(Message.ToString());
        
        Assert.NotEqual(Message.ToString(), enciphered);
        Assert.Equal(Message.Length, enciphered.Length);

        machine.Reset();
        
        var deciphered = machine.Encipher(enciphered);

        Assert.Equal(Message.ToString(), deciphered);
    }
    
    [Fact]
    public void GeneratedMachineTest()
    {
        var Message = new StringBuilder();
        var ci = 0;

        for (var i = 0; i < Constants.CharacterSetValues[CharacterSets.Ascii].Length * Constants.CharacterSetValues[CharacterSets.Ascii].Length * Constants.CharacterSetValues[CharacterSets.Ascii].Length; i++)
        {
            Message.Append(Constants.CharacterSetValues[CharacterSets.Ascii][ci++]);

            if (ci == Constants.CharacterSetValues[CharacterSets.Ascii].Length)
                ci = 0;
        }

        var machine = new Machine("ThisIsA32ByteLongSecretKey123456", "UniqueNonce12345");
        var enciphered = machine.Encipher(Message.ToString());
        
        Assert.NotEqual(Message.ToString(), enciphered);
        Assert.Equal(Message.Length, enciphered.Length);

        machine.Reset();
        
        var deciphered = machine.Encipher(enciphered);

        Assert.Equal(Message.ToString(), deciphered);
    }
}
