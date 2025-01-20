using System.Collections.Generic;
using System.Diagnostics;
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
                RotorWheel = Rotors.GetRotor(Rotors.RotorType.Wehrmacht_I)
            });

        var rotor2 = new Rotor(new RotorConfiguration
        {
            RotorWheel = Rotors.GetRotor(Rotors.RotorType.Wehrmacht_II),
            RingPosition = 10
        });

        var rotor3 = new Rotor(new RotorConfiguration
        {
            RotorWheel = Rotors.GetRotor(Rotors.RotorType.Wehrmacht_III),
            RingPosition = 13
        });

        var reflector = new Reflector()
            .SetWires(Reflectors.GetReflector(Reflectors.ReflectorType.Wehrmacht_B));

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

        for (var i = 0; i < Constants.CharacterSet.Length * Constants.CharacterSet.Length * Constants.CharacterSet.Length; i++)
        {
            Message.Append(Constants.CharacterSet[ci++]);

            if (ci == Constants.CharacterSet.Length)
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
            RotorWheel = Rotors.GetRotor(Rotors.RotorType.Ascii_I)
        });

        var rotor2 = new Rotor(new RotorConfiguration
        {
            RotorWheel = Rotors.GetRotor(Rotors.RotorType.Ascii_II),
            RingPosition = 10
        });

        var rotor3 = new Rotor(new RotorConfiguration
        {
            RotorWheel = Rotors.GetRotor(Rotors.RotorType.Ascii_III),
            RingPosition = 65
        });

        var reflector = new Reflector()
            .SetWires(Reflectors.GetReflector(Reflectors.ReflectorType.Ascii));

        var timer = new Stopwatch();
        var enciphered = new StringBuilder();

        timer.Start();
        
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

        timer.Stop();
        
        Assert.False(Message.Equals(enciphered));
        Assert.Equal(Message.Length, enciphered.Length);
        
        Debug.WriteLine($"NEW Encipher => {timer.Elapsed.TotalSeconds}");
        
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
}
