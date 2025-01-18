using System.Linq;
using Xunit;

namespace Enigma.Tests;

public class RotorTests
{
    [Fact]
    public void GeneratedRotor()
    {
        var rotorWheel = Rotors.GenerateRotor(Rotors.CharacterSet.Ascii);
        var rotor = new Rotor()
            .SetWheel(rotorWheel);
        
        Assert.Equal(rotorWheel[' '], rotor.SendCharacter(' '));
        Assert.Equal(rotorWheel['A'], rotor.SendCharacter('A'));
        Assert.Equal(rotorWheel['B'], rotor.SendCharacter('B'));
        Assert.Equal(rotorWheel['~'], rotor.SendCharacter('~'));

        Assert.Equal(rotorWheel.First(r => r.Value == '*').Key, rotor.ReflectedCharacter('*'));
        Assert.Equal(rotorWheel.First(r => r.Value == 'E').Key, rotor.ReflectedCharacter('E'));
        Assert.Equal(rotorWheel.First(r => r.Value == 'K').Key, rotor.ReflectedCharacter('K'));
        Assert.Equal(rotorWheel.First(r => r.Value == '%').Key, rotor.ReflectedCharacter('%'));
    }

    [Fact]
	public void AsciiRotor()
    {
        var rotorWheel = Rotors.GetRotor(Rotors.RotorType.Ascii_I);
        var rotor = new Rotor()
            .SetWheel(rotorWheel);
        
        Assert.Equal(rotorWheel[' '], rotor.SendCharacter(' '));
        Assert.Equal(rotorWheel['A'], rotor.SendCharacter('A'));
        Assert.Equal(rotorWheel['B'], rotor.SendCharacter('B'));
        Assert.Equal(rotorWheel['~'], rotor.SendCharacter('~'));

        Assert.Equal(rotorWheel.First(r => r.Value == '*').Key, rotor.ReflectedCharacter('*'));
        Assert.Equal(rotorWheel.First(r => r.Value == 'E').Key, rotor.ReflectedCharacter('E'));
        Assert.Equal(rotorWheel.First(r => r.Value == 'K').Key, rotor.ReflectedCharacter('K'));
        Assert.Equal(rotorWheel.First(r => r.Value == '%').Key, rotor.ReflectedCharacter('%'));

        rotor.SetRotation(1);

        Assert.Equal('r', rotor.SendCharacter(' '));
        Assert.Equal('z', rotor.SendCharacter('A'));
        Assert.Equal('~', rotor.SendCharacter('B'));
        Assert.Equal('n', rotor.SendCharacter('~'));

        Assert.Equal('|', rotor.ReflectedCharacter('d'));
        Assert.Equal('1', rotor.ReflectedCharacter('K'));
        Assert.Equal(',', rotor.ReflectedCharacter('M'));
        Assert.Equal('a', rotor.ReflectedCharacter('*'));

        rotor.SetRotation(0);
        rotor.SetNotchPosition(1);

        Assert.Equal('r', rotor.SendCharacter(' '));
        Assert.Equal('z', rotor.SendCharacter('A'));
        Assert.Equal('~', rotor.SendCharacter('B'));
        Assert.Equal('n', rotor.SendCharacter('~'));

        Assert.Equal('|', rotor.ReflectedCharacter('d'));
        Assert.Equal('1', rotor.ReflectedCharacter('K'));
        Assert.Equal(',', rotor.ReflectedCharacter('M'));
        Assert.Equal('a', rotor.ReflectedCharacter('*'));
	}
}
