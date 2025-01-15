using Xunit;

namespace Enigma.Tests;

public class V1EnigmaTests
{
	[Fact]
	public void EnigmaRotor()
	{
		var rotor = new V1EnigmaRotor(12345678);
		const string phrase = "This is only a test.";
		var encoded = string.Empty;
		var echar = rotor.GetGlyph('A');
		var rechar = rotor.GetGlyphReflected(echar);
		
		Assert.Equal('A', rechar);
		
		rotor.Reset();
		
		foreach (var t in phrase)
		{
			encoded += rotor.GetGlyph(t);
		}
		
		rotor.Reset();
		var decoded = string.Empty;
		
		foreach (var t in encoded)
		{
			decoded += rotor.GetGlyphReflected(t);
		}
		
		Assert.Equal(phrase, decoded);
		
		rotor.Reset();
		rotor.Rotate();
		rotor.Rotate();
		rotor.Rotate();
		encoded = string.Empty;
		
		foreach (var t in phrase)
		{
			var glyph = rotor.GetGlyph(t); 
			encoded += glyph;
		}
		
		rotor.Reset();
		rotor.Rotate();
		rotor.Rotate();
		rotor.Rotate();
		decoded = string.Empty;
		
		foreach (var t in encoded)
		{
			var glyph = rotor.GetGlyphReflected(t);
			decoded += glyph;
		}
		
		Assert.Equal(phrase, decoded);
	}

	[Fact]
	public void EnigmaMachine()
	{
		var enigma = new V1EnigmaMachine(new V1EnigmaConfiguration
		{
			PlugBoardCipherSeed = 5647382910,
			ReflectorCipherSeed = 3920156474,
			Rotors =
            [
                new V1EnigmaRotor(rotorCipherSeed: 9876543210, rotorStartingPosition: 0, advanceNextRotorIncrement: 50),
                new V1EnigmaRotor(rotorCipherSeed: 1234567890, rotorStartingPosition: 0, advanceNextRotorIncrement: 25),
                new V1EnigmaRotor(rotorCipherSeed: 1029384756, rotorStartingPosition: 0)
            ]
        });
		
		#region Test basic UTF-16 string
		
		var message = """
                      Fynydd is a software development & hosting company.
                      Fynydd is a Welsh word (pronounced: /ˈvənɨ̞ð/) that means mountain or hill.
                      """;

		var scrambled = enigma.RunCipher(message);
		var unscrambled = enigma.RunCipher(scrambled);

		Assert.Equal(message, unscrambled);

		#endregion
		
		#region Test entire UTF-16 character space
		
		message = string.Empty;
		
		for (var x = V1EnigmaConfiguration.CharSetStart; x <= V1EnigmaConfiguration.CharSetEnd; x++)
			message += (char) x;

		scrambled = enigma.RunCipher(message);
		unscrambled = enigma.RunCipher(scrambled);

		Assert.Equal(message, unscrambled);
		
		#endregion
	}
}
