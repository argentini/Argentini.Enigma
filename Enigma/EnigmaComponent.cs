namespace Enigma;

/// <summary>
/// Base class for EnigmaMachine plug board, reflector, and rotors.
/// </summary>
public class EnigmaComponent
{
	#region State Properties
	
	/// <summary>
	/// Plug boards and reflectors use this reflective, indexed list of UTF-16 characters,
	/// which map two characters together (e.g. A => Q and Q => A); always the same pair.
	/// </summary>
	public SortedList<char, char> Characters { get; } = new ();

	/// <summary>
	/// Cipher seed used to generate predictably random character sets.
	/// </summary>
	protected long CipherSeed { get; set; }

	/// <summary>
	/// This flag is true when it's time to rotate the next rotor.
	/// </summary>
	public bool AdvanceNextRotor { get; protected set; }
	
	#endregion

	/// <summary>
	/// Quickly generates a predictably random character set based on the
	/// current object's cipher seed. The characters are filled using
	/// the range specified in the EnigmaConfiguration. 
	/// </summary>
	/// <param name="charSetList">Filled with random character set data</param>
	protected void GenerateRandomCharSet(List<char> charSetList)
	{
		var prng = new PredictableRandomNumberGenerator(CipherSeed);

		var characterSet = new List<char>();

		for (var x = EnigmaConfiguration.CharSetStart; x <= EnigmaConfiguration.CharSetEnd; x++)
		{
			characterSet.Add((char)x);
		}

		foreach (var item in characterSet.OrderBy(_ => prng.Next()))
		{
			charSetList.Add(item);
		}
	}
	
	/// <summary>
	/// Populate the Characters property with a reflective, predictably random character set.
	/// </summary>
	protected void GenerateReflectorWheel()
	{
		var scrambler = new List<char>();

		GenerateRandomCharSet(scrambler);
		
		while (scrambler.Any())
		{
			var key = scrambler.ElementAt(0);
			
			if (Characters.ContainsKey(key) == false)
			{
				scrambler.RemoveAt(0);
		
				if (scrambler.Any())
				{
					var value = scrambler.ElementAt(0);
		
					if (Characters.ContainsKey(value) == false)
					{
						Characters.Add(key, value);
						Characters.Add(value, key);
					}
				}
		
				else
				{
					Characters.Add(key, key);
				}
			}
		
			else
			{
				scrambler.RemoveAt(0);
			}
		}
	}
	
	/// <summary>
	/// Provide a character input to retrieve its reflected, predictably random character
	/// (e.g. A => Q, Q => A); always the same pair.
	/// </summary>
	/// <param name="character">Input UTF-16 character</param>
	/// <returns></returns>
	public char GetGlyph(char character)
	{
		if (character >= EnigmaConfiguration.CharSetStart && character <= EnigmaConfiguration.CharSetEnd)
			return Characters[character];

		return character;
	}
}
