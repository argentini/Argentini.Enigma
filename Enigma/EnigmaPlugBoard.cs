namespace Enigma;

/// <summary>
/// Virtual plug board used to send a character to be enciphered.
/// Performs a character swap before sending to the rotors.
/// Performs a final character swap after the enciphered character
/// returns from the rotors.
/// </summary>
public class EnigmaPlugBoard : EnigmaComponent
{
	/// <summary>
	/// Create a virtual plug board.
	/// </summary>
	/// <param name="plugBoardCipherSeed">Determines the predictably random order of the character set.</param>
	public EnigmaPlugBoard(long plugBoardCipherSeed)
	{
		CipherSeed = plugBoardCipherSeed > 0 ? plugBoardCipherSeed : EnigmaConfiguration.DefaultCipherSeed;
		
		GenerateReflectorWheel();
	}
}
