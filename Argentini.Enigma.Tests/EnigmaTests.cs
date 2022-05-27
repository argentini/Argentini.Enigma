using Xunit;
using System.Collections.Generic;

namespace Argentini.Enigma.Tests;

public class EnigmaTests
{
	[Fact]
	public void EnigmaRotor()
	{
		var rotor = new EnigmaRotor(12345678);
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
		var enigma = new EnigmaMachine(new EnigmaConfiguration
		{
			PlugBoardCipherSeed = 1234567,
			ReflectorCipherSeed = 6789101,
			Rotors = new List<EnigmaRotor>
			{
				new (42, advanceNextRotorIncrement: 50),
				new (86, advanceNextRotorIncrement: 25),
				new (99)
			}
		});
		
		#region Test basic UTF-16 string
		
		var message = @"
Fynydd is a software development & hosting company.
Fynydd is a Welsh word (prounounced: /ˈvənɨ̞ð/) that means mountain or hill.
";

		var scrambled = enigma.RunCipher(message);
		var unscrambled = enigma.RunCipher(scrambled);

		Assert.Equal(message, unscrambled);

		#endregion
		
		#region Test entire UTF-16 character space
		
		message = string.Empty;
		
		for (var x = EnigmaConfiguration.CharSetStart; x <= EnigmaConfiguration.CharSetEnd; x++)
		{
			message += (char) x;
		}

		scrambled = enigma.RunCipher(message);
		unscrambled = enigma.RunCipher(scrambled);

		Assert.Equal(message, unscrambled);
		
		#endregion
	}
}
