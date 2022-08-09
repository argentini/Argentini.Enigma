namespace Enigma;

/// <summary>
/// EnigmaMachine configuration object.
/// </summary>
public class EnigmaConfiguration
{
	#region Constants
	
	/// <summary>
	/// Bottom range UTF-16 character value to include in the character set 
	/// </summary>
	public const int CharSetStart = 1;

	/// <summary>
	/// Top range UTF-16 character value to include in the character set 
	/// </summary>
	public const int CharSetEnd = char.MaxValue;
	
	/// <summary>
	/// Number of supported UTF-16 characters
	/// </summary>
	public const int CharSetCount = CharSetEnd - CharSetStart;

	/// <summary>
	/// Lowest index value for the character set list (always zero)
	/// </summary>
	public const int MinRotorIndex = 0;

	/// <summary>
	/// Highest index value for the character set list
	/// </summary>
	public const int MaxRotorIndex = CharSetCount;

	/// <summary>
	/// Cipher activites start with the current rotor in this position if not specified.
	/// </summary>
	public const int DefaultStartRotation = 0;

	/// <summary>
	/// The first rotor always advances one position for each character that is enciphered.
	/// The current rotor will advance the next rotor on this counter increment if not specified.
	/// </summary>
	public const int DefaultPinIncrement = 0;

	/// <summary>
	/// The cipher seed if not specified
	/// </summary>
	public const long DefaultCipherSeed = 42;

	#endregion

	/// <summary>
	/// Cipher seed used to generate a predictably random character set order for the plug board.
	/// </summary>
	public long PlugBoardCipherSeed { get; set; }

	/// <summary>
	/// Cipher seed used to generate a predictably random character set order for the reflector.
	/// </summary>
	public long ReflectorCipherSeed { get; set; }

	/// <summary>
	/// The collection of rotors. In a real Enigma machine rotors are used right to left
	/// and then reflected back left to right. The rotors list here is used first to last
	/// then reflected back last to first.
	/// </summary>
	public List<EnigmaRotor> Rotors { get; set; } = new();
}
