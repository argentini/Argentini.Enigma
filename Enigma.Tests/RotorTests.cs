using System.Linq;
using Xunit;

namespace Enigma.Tests;

public class RotorTests
{
    [Fact]
    public void GeneratedRotor()
    {
        var rotor = new Rotor(new RotorConfiguration
        {
            CharacterSet = CharacterSets.Ascii,
            Secret = "ThisIsA32ByteLongSecretKey123456",
            Nonce = "UniqueNonce12345"
        });
        
        Assert.Equal(rotor.Configuration.RotorWheel[' '], rotor.SendCharacter(' '));
        Assert.Equal(rotor.Configuration.RotorWheel['A'], rotor.SendCharacter('A'));
        Assert.Equal(rotor.Configuration.RotorWheel['B'], rotor.SendCharacter('B'));
        Assert.Equal(rotor.Configuration.RotorWheel['~'], rotor.SendCharacter('~'));

        Assert.Equal(rotor.Configuration.RotorWheel.First(r => r.Value == '*').Key, rotor.ReflectedCharacter('*'));
        Assert.Equal(rotor.Configuration.RotorWheel.First(r => r.Value == 'E').Key, rotor.ReflectedCharacter('E'));
        Assert.Equal(rotor.Configuration.RotorWheel.First(r => r.Value == 'K').Key, rotor.ReflectedCharacter('K'));
        Assert.Equal(rotor.Configuration.RotorWheel.First(r => r.Value == '%').Key, rotor.ReflectedCharacter('%'));
    }

    [Fact]
	public void AsciiRotor()
    {
        var rotor = new Rotor(new RotorConfiguration
        {
            RotorPreset = RotorPresets.Ascii_I
        });
        
        Assert.Equal(rotor.Configuration.RotorWheel[' '], rotor.SendCharacter(' '));
        Assert.Equal(rotor.Configuration.RotorWheel['A'], rotor.SendCharacter('A'));
        Assert.Equal(rotor.Configuration.RotorWheel['B'], rotor.SendCharacter('B'));
        Assert.Equal(rotor.Configuration.RotorWheel['~'], rotor.SendCharacter('~'));

        Assert.Equal(rotor.Configuration.RotorWheel.First(r => r.Value == '*').Key, rotor.ReflectedCharacter('*'));
        Assert.Equal(rotor.Configuration.RotorWheel.First(r => r.Value == 'E').Key, rotor.ReflectedCharacter('E'));
        Assert.Equal(rotor.Configuration.RotorWheel.First(r => r.Value == 'K').Key, rotor.ReflectedCharacter('K'));
        Assert.Equal(rotor.Configuration.RotorWheel.First(r => r.Value == '%').Key, rotor.ReflectedCharacter('%'));

        rotor.ResetRotation();
        rotor.Rotate();
        
        Assert.Equal('r', rotor.SendCharacter(' '));
        Assert.Equal('z', rotor.SendCharacter('A'));
        Assert.Equal('~', rotor.SendCharacter('B'));
        Assert.Equal('n', rotor.SendCharacter('~'));

        Assert.Equal('|', rotor.ReflectedCharacter('d'));
        Assert.Equal('1', rotor.ReflectedCharacter('K'));
        Assert.Equal(',', rotor.ReflectedCharacter('M'));
        Assert.Equal('a', rotor.ReflectedCharacter('*'));

        rotor = new Rotor(new RotorConfiguration
        {
            RotorPreset = RotorPresets.Ascii_I,
            RingPosition = 1
        });

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
