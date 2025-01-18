using System.Collections.Generic;
using System.Security.Cryptography;
using Xunit;

namespace Enigma.Tests;

public class MachineTests
{
	[Fact]
	public void SimpleCipherTest()
    {
        const string Message = "This is a Cipher Test";

        var plugBoard = new PlugBoard
        {
            Wires = new Dictionary<char, char>
            {
                { 'T', 'A' },
                { 'C', 'B' }
            }
        };
        
        var rotor1 = new Rotor
        {
            Wheel = Constants.Rotor1
        };

        var rotor2 = new Rotor
        {
            Wheel = Constants.Rotor2
        };

        var rotor3 = new Rotor
        {
            Wheel = Constants.Rotor3
        };

        var reflector = new Reflector
        {
            Wires = Constants.Reflector1
        };
        
        var enciphered = string.Empty;

        foreach (var c in Message)
        {
            var cc = plugBoard.SendCharacter(c);

            cc = rotor1.SendCharacter(cc);
            cc = rotor2.SendCharacter(cc);
            cc = rotor3.SendCharacter(cc);
            cc = reflector.SendCharacter(cc);
            cc = rotor3.ReflectedCharacter(cc);
            cc = rotor2.ReflectedCharacter(cc);
            cc = rotor1.ReflectedCharacter(cc);
            cc = plugBoard.SendCharacter(cc);
            
            enciphered += cc;

            rotor1.Rotation++;
        }
        
        Assert.Equal("N=zi-hVx6_FV_|&UP8'lA", enciphered);
        
        var deciphered = string.Empty;

        rotor1.Rotation = 0;

        foreach (var c in enciphered)
        {
            var cc = plugBoard.SendCharacter(c);

            cc = rotor1.SendCharacter(cc);
            cc = rotor2.SendCharacter(cc);
            cc = rotor3.SendCharacter(cc);
            cc = reflector.SendCharacter(cc);
            cc = rotor3.ReflectedCharacter(cc);
            cc = rotor2.ReflectedCharacter(cc);
            cc = rotor1.ReflectedCharacter(cc);
            cc = plugBoard.SendCharacter(cc);

            deciphered += cc;

            rotor1.Rotation++;
        }
        
        Assert.Equal(Message, deciphered);
    }
}
