using System.Linq;
using Xunit;

namespace Enigma.Tests;

public class ReflectorTests
{
    [Fact]
    public void AsciiReflector()
    {
        var reflector = new Reflector(new ReflectorConfiguration
        {
            ReflectorPreset = ReflectorPresets.Ascii
        });
        
        Assert.Equal(reflector.Configuration.ReflectorWheel[' '], reflector.SendCharacter(' '));
        Assert.Equal(reflector.Configuration.ReflectorWheel['A'], reflector.SendCharacter('A'));
        Assert.Equal(reflector.Configuration.ReflectorWheel['B'], reflector.SendCharacter('B'));
        Assert.Equal(reflector.Configuration.ReflectorWheel['~'], reflector.SendCharacter('~'));
        
        Assert.Equal(' ', reflector.SendCharacter('B'));
        Assert.Equal('A', reflector.SendCharacter('J'));
        Assert.Equal('B', reflector.SendCharacter(' '));
        Assert.Equal('~', reflector.SendCharacter('N'));
    }

    [Fact]
    public void GeneratedReflector()
    {
        var reflector = new Reflector(new ReflectorConfiguration
        {
            CharacterSet = CharacterSets.Ascii,
            Secret = "ThisIsA32ByteLongSecretKey123456",
            Nonce = "UniqueNonce12345"
        });
        
        Assert.Equal(reflector.Configuration.ReflectorWheel[' '], reflector.SendCharacter(' '));
        Assert.Equal(reflector.Configuration.ReflectorWheel['A'], reflector.SendCharacter('A'));
        Assert.Equal(reflector.Configuration.ReflectorWheel['B'], reflector.SendCharacter('B'));
        Assert.Equal(reflector.Configuration.ReflectorWheel['~'], reflector.SendCharacter('~'));
        
        Assert.Equal(' ', reflector.SendCharacter(reflector.Configuration.ReflectorWheel.First(w => w.Key == ' ').Value));
        Assert.Equal('A', reflector.SendCharacter(reflector.Configuration.ReflectorWheel.First(w => w.Key == 'A').Value));
        Assert.Equal('B', reflector.SendCharacter(reflector.Configuration.ReflectorWheel.First(w => w.Key == 'B').Value));
        Assert.Equal('~', reflector.SendCharacter(reflector.Configuration.ReflectorWheel.First(w => w.Key == '~').Value));
    }

}
