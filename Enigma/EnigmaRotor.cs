namespace Enigma;

/// <summary>
/// Virtual rotor used in a series as part of the cipher process.
/// </summary>
public class EnigmaRotor: EnigmaComponent
{
	#region State Properties

	/// <summary>
	/// Overrides the EnigmaComponent property;
	/// Simple list of non-reflective, predictably random characters.
	/// </summary>
	public new List<char> Characters { get; } = new ();

	/// <summary>
	/// Current position (rotation) of the rotor.
	/// </summary>
	public int Position { get; set; }

	/// <summary>
	/// Starting position (rotation) of the rotor assigned in EingmaConfiguration.
	/// </summary>
	public int OriginalStartPosition { get; }

	/// <summary>
	/// Number of rotor position advances before the next rotor will advance.
	/// </summary>
	public int AdvanceNextRotorIncrement { get; }

	/// <summary>
	/// The current number of rotor position advances;
	/// set back to zero at every AdvanceNextRotorIncrement.
	/// </summary>
	public int AdvanceRotorCounter { get; set; }

	#endregion
	
	/// <summary>
	/// Creates a new EngimaMachine rotor object.
	/// </summary>
	/// <param name="rotorCipherSeed">Determines the predictably random order of the character set.</param>
	/// <param name="rotorStartingPosition">Usually zero but can be set to any valid rotor position index (e.g. zero through EnigmaConfiguration.CharSetCount) to change the cipher.</param>
	/// <param name="advanceNextRotorIncrement">Number of rotor position advances before the next rotor will advance.</param>
	public EnigmaRotor(long rotorCipherSeed, int rotorStartingPosition = EnigmaConfiguration.DefaultStartRotation, int advanceNextRotorIncrement = EnigmaConfiguration.DefaultPinIncrement)
	{
		CipherSeed = rotorCipherSeed > 0 ? rotorCipherSeed : EnigmaConfiguration.DefaultCipherSeed;
		OriginalStartPosition = rotorStartingPosition is < EnigmaConfiguration.MinRotorIndex or > EnigmaConfiguration.MaxRotorIndex ? EnigmaConfiguration.MinRotorIndex : rotorStartingPosition;
		AdvanceNextRotorIncrement = advanceNextRotorIncrement is > 0 and < EnigmaConfiguration.CharSetCount ? advanceNextRotorIncrement : EnigmaConfiguration.DefaultPinIncrement;

		GenerateRandomCharSet(Characters);
		Reset();
	}

	/// <summary>
	/// Provide a character input to retrieve its associated predictably random character
	/// (e.g. A => Q); changes with rotor position (rotation).
	/// </summary>
	/// <param name="character">Input UTF-16 character</param>
	/// <returns></returns>
	public new char GetGlyph(char character)
	{
		if (character >= EnigmaConfiguration.CharSetStart && character <= EnigmaConfiguration.CharSetEnd)
		{
			var position = (character - EnigmaConfiguration.CharSetStart + Position) % Characters.Count;

			return Characters.ElementAt(position);
		}

		return character;
	}

	/// <summary>
	/// Provide a character input to retrieve its reflected character
	/// (e.g. A => Q, Q => A); changes with rotor position (rotation).
	/// </summary>
	/// <param name="character">Input UTF-16 character</param>
	/// <returns></returns>
	public char GetGlyphReflected(char character)
	{
		if (character >= EnigmaConfiguration.CharSetStart && character <= EnigmaConfiguration.CharSetEnd)
		{
			var position = Characters.IndexOf(character) - Position;

			if (position < EnigmaConfiguration.MinRotorIndex)
			{
				position += EnigmaConfiguration.MaxRotorIndex + 1;
			}

			var value = (char) (position + EnigmaConfiguration.CharSetStart);

			return value;
		}

		return character;
	}

	/// <summary>
	/// Advance (rotate) the rotor one step.
	/// </summary>
	public void Rotate()
	{
		Position++;
		
		if (Position > EnigmaConfiguration.MaxRotorIndex)
		{
			Position = EnigmaConfiguration.MinRotorIndex;
			
			if (AdvanceNextRotorIncrement > 0)
				AdvanceNextRotor = true;
		}

		else
		{
			if (AdvanceNextRotorIncrement > 0)
				AdvanceNextRotor = false;
		}
	}

	/// <summary>
	/// Reset the rotor to its original state.
	/// </summary>
	public void Reset()
	{
		Position = OriginalStartPosition;
		AdvanceRotorCounter = 0;
		AdvanceNextRotor = false;
	}
}
