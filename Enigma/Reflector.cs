namespace Enigma;

/// <summary>
/// Virtual reflector used to route a character back to the light board.
/// Positioned left at the end of the rotors.
/// </summary>
public sealed class Reflector
{
    public Dictionary<char,char> Wheel { get; set; } = [];
    
    private IndexedDictionary<char,char> EncipherWheel { get; set; } = new();

    /// <summary>
    /// Establish IncomingWires dictionary which inverts the provided Wires values
    /// so the hashed keys can be used for faster searches when chars return for final output.
    /// </summary>
    /// <returns></returns>
	public void Reset()
	{
        EncipherWheel.Clear();

        if (Wheel.Count == 0)
            return;

        EncipherWheel.AddRange(Wheel);
	}

	public char EncipherCharacter(char c)
	{
        if (EncipherWheel.TryGetValue(c, out var value))
            return value;

        return c;
	}

	public char DecipherCharacter(char c)
	{
        if (EncipherWheel.TryGetKey(c, out var value))
            return value;

        return c;
	}
}
