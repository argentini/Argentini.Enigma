using System.Linq;
using Xunit;

namespace Enigma.Tests;

public class EntryWheelTests
{
    [Fact]
    public void GeneratedEntryWheel()
    {
        var etw = new EntryWheel(new EntryWheelConfiguration
        {
            CharacterSet = CharacterSets.Ascii,
            Secret = "ThisIsA32ByteLongSecretKey123456",
            Nonce = "UniqueNonce12345"
        });
        
        Assert.Equal(etw.Configuration.EntryWheel[' '], etw.SendCharacter(' '));
        Assert.Equal(etw.Configuration.EntryWheel['A'], etw.SendCharacter('A'));
        Assert.Equal(etw.Configuration.EntryWheel['B'], etw.SendCharacter('B'));
        Assert.Equal(etw.Configuration.EntryWheel['~'], etw.SendCharacter('~'));

        Assert.Equal(etw.Configuration.EntryWheel.First(r => r.Value == '*').Key, etw.ReflectedCharacter('*'));
        Assert.Equal(etw.Configuration.EntryWheel.First(r => r.Value == 'E').Key, etw.ReflectedCharacter('E'));
        Assert.Equal(etw.Configuration.EntryWheel.First(r => r.Value == 'K').Key, etw.ReflectedCharacter('K'));
        Assert.Equal(etw.Configuration.EntryWheel.First(r => r.Value == '%').Key, etw.ReflectedCharacter('%'));
    }

    [Fact]
	public void AsciiEntryWheel()
    {
        var etw = new EntryWheel(new EntryWheelConfiguration
        {
            EntryWheelPreset = EntryWheelPresets.Ascii
        });
        
        Assert.Equal(etw.Configuration.EntryWheel[' '], etw.SendCharacter(' '));
        Assert.Equal(etw.Configuration.EntryWheel['A'], etw.SendCharacter('A'));
        Assert.Equal(etw.Configuration.EntryWheel['B'], etw.SendCharacter('B'));
        Assert.Equal(etw.Configuration.EntryWheel['~'], etw.SendCharacter('~'));

        Assert.Equal(etw.Configuration.EntryWheel.First(r => r.Value == '*').Key, etw.ReflectedCharacter('*'));
        Assert.Equal(etw.Configuration.EntryWheel.First(r => r.Value == 'E').Key, etw.ReflectedCharacter('E'));
        Assert.Equal(etw.Configuration.EntryWheel.First(r => r.Value == 'K').Key, etw.ReflectedCharacter('K'));
        Assert.Equal(etw.Configuration.EntryWheel.First(r => r.Value == '%').Key, etw.ReflectedCharacter('%'));
	}
}
