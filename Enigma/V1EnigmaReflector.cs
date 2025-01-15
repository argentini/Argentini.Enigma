namespace Enigma;

/// <summary>
/// Virtual reflector used to bounce a character back through the rotors in reverse order.
/// </summary>
public class V1EnigmaReflector : V1EnigmaComponent
{
	/// <summary>
	/// Create a virtual reflector.
	/// </summary>
	/// <param name="reflectorCipherSeed">Determines the predictably random order of the character set.</param>
	public V1EnigmaReflector(long reflectorCipherSeed)
	{
		CipherSeed = reflectorCipherSeed > 0 ? reflectorCipherSeed : V1EnigmaConfiguration.DefaultCipherSeed;
		
		GenerateReflectorWheel();
	}
}
