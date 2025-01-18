using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace Enigma.Tests;

public class MachineTests
{
	[Fact]
	public void FullCipherTest()
    {
        var Message = new StringBuilder();
        var ci = 0;

        for (var i = 0; i < Constants.CharacterSet.Length * Constants.CharacterSet.Length * Constants.CharacterSet.Length; i++)
        {
            Message.Append(Constants.CharacterSet[ci++]);

            if (ci == Constants.CharacterSet.Length)
                ci = 0;
        }
        
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

        var timer = new Stopwatch();
        var enciphered = new StringBuilder();

        timer.Start();
        
        foreach (var c in Message.ToString())
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
            
            enciphered.Append(cc);

            rotor1.Rotation++;

            if (rotor1.Rotation != 0)
                continue;
            
            rotor2.Rotation++;
                
            if (rotor2.Rotation == 0)
                rotor3.Rotation++;
        }

        timer.Stop();
        
        Assert.False(Message.Equals(enciphered));
        Assert.Equal(Message.Length, enciphered.Length);
        
        Debug.WriteLine($"NEW Encipher => {timer.Elapsed.TotalSeconds}");
        
        var deciphered = new StringBuilder();

        rotor1.Rotation = 0;
        rotor2.Rotation = 0;
        rotor3.Rotation = 0;

        foreach (var c in enciphered.ToString())
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

            deciphered.Append(cc);

            rotor1.Rotation++;

            if (rotor1.Rotation != 0)
                continue;
            
            rotor2.Rotation++;
                
            if (rotor2.Rotation == 0)
                rotor3.Rotation++;
        }
        
        Assert.True(Message.Equals(deciphered));
    }
}
