using System.Text;

namespace Enigma;

/// <summary>
/// EnigmaMachine is a digital Enigma machine that supports the entire UTF-16 character set.
/// Machine state is used to both encipher and decipher text with the same command.
/// The original German hardware only supported 26 upper case characters.
/// This modern tool enciphers and deciphers a string using a keyless algorythm based on the
/// inner workings of the original Enigma hardware. It uses a predictable random number
/// generator (PRNG) to ensure the cipher will work on any supported platform.
///
/// Unlike the original hardware, you can add as many virtual rotors as you like, set the cipher
/// seeds for all machine components, set starting rotor positions, and more, which will all
/// improve the overall cipher strength.
/// </summary>
public class EnigmaMachine
{
	/// <summary>
	/// The configuration passed to the constructor. 
	/// </summary>
	public EnigmaConfiguration Configuration { get; private set; }

	/// <summary>
	/// Virtual plug board.
	/// </summary>
	private EnigmaPlugBoard PlugBoard { get; set; }
	
	/// <summary>
	/// Virtual reflector.
	/// </summary>
	private EnigmaReflector Reflector { get; set; }

	/// <summary>
	/// Create a new EnigmaMachine object by passing in a configuration object.
	/// </summary>
	/// <param name="enigmaConfiguration">Configuration object</param>
	/// <exception cref="Exception"></exception>
	public EnigmaMachine(EnigmaConfiguration enigmaConfiguration)
	{
		if (enigmaConfiguration.Rotors.Any() == false)
		{
			throw new Exception("Halide.EnigmaMachine() => No rotors specified");
		}

		if (enigmaConfiguration.Rotors.Count < 2)
		{
			throw new Exception("Halide.EnigmaMachine() => Must have 2 or more rotors");
		}
		
		if (enigmaConfiguration.PlugBoardCipherSeed < 1)
		{
			throw new Exception("Halide.EnigmaMachine() => Plug board seed must be a non-zero long integer");
		}

		if (enigmaConfiguration.ReflectorCipherSeed < 1)
		{
			throw new Exception("Halide.EnigmaMachine() => Reflector seed must be a non-zero long integer");
		}
		
		Configuration = enigmaConfiguration;
		PlugBoard = new EnigmaPlugBoard(Configuration.PlugBoardCipherSeed);
		Reflector = new EnigmaReflector(Configuration.ReflectorCipherSeed);
	}
	
	/// <summary>
	/// Reset to the original state. This is called automatically as needed.
	/// State must be reset between encipher/decipher operations as the
	/// Enigma cipher requires a predictable state. 
	/// </summary>
	public void Reset()
	{
		foreach (var rotor in Configuration.Rotors)
		{
			rotor.Reset();
		}
	}

	/// <summary>
	/// Encipher/decipher a single character and advance the rotors.
	/// Enigma uses machine state to both encipher and decipher with the same command.
	/// </summary>
	/// <param name="character">UTF-16 character to encipher or decipher</param>
	/// <returns></returns>
	private char ScrambleCharacter(char character)
	{
		#region Core Cipher Process
		
		// Plugboard
		var glyph = PlugBoard.GetGlyph(character);
		
		// Rotors (simulate right to left operation)
		foreach (var rotor in Configuration.Rotors)
		{
			glyph = rotor.GetGlyph(glyph);
		}

		// Reflector
		glyph = Reflector.GetGlyph(glyph);
		
		// Rotors (reflected, simulate left to right operation)
		for (var x = Configuration.Rotors.Count - 1; x >= 0; x--)
		{
			glyph = Configuration.Rotors[x].GetGlyphReflected(glyph);
		}

		// Plugboard (reflected)
		glyph = PlugBoard.GetGlyph(glyph);

		#endregion
		
		#region Advance (rotate) Rotors 
		
		EnigmaRotor? previousRotor = null;

		for (var x = 0; x < Configuration.Rotors.Count; x++)
		{
			if (x == 0)
			{
				Configuration.Rotors[0].Rotate();
			}

			else
			{
				if (previousRotor?.AdvanceNextRotor ?? false) Configuration.Rotors[x].Rotate();
			}

			previousRotor = Configuration.Rotors[x];
		}
		
		#endregion

		return glyph;
	}

	/// <summary>
	/// Encipher or decipher a UTF-16 string.
	/// Enigma uses machine state to both encipher and decipher with the same command.
	/// </summary>
	/// <param name="message">UTF-16 string to encipher or decipher</param>
	/// <returns></returns>
	public string RunCipher(string message)
	{
		var scrambledText = new StringBuilder();

		Reset();

		foreach (var t in message)
		{
			if (t >= EnigmaConfiguration.CharSetStart && t <= EnigmaConfiguration.CharSetEnd)
			{
				var ch = ScrambleCharacter(t);

				scrambledText.Append(ch);
			}

			else
			{
				scrambledText.Append(t);
			}
		}

		return scrambledText.ToString();
	}
}
