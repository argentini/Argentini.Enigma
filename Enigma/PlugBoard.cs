namespace Enigma;

/// <summary>
/// Virtual plug board used to send a character to be enciphered.
/// Performs a character swap before sending to the rotors.
/// Performs a final character swap after the enciphered character returns from the rotors.
/// </summary>
public sealed class PlugBoard
{
    public Dictionary<char,char> Wires { get; set; } = [];
    
    private IndexedDictionary<char,char> EncipherWires { get; set; } = new();

    /// <summary>
    /// Establish IncomingWires dictionary which inverts the provided Wires values
    /// so the hashed keys can be used for faster searches when chars return for final output.
    /// </summary>
    /// <returns></returns>
	public void Reset()
	{
        EncipherWires.Clear();

        if (Wires.Count == 0)
            return;

        EncipherWires.AddRange(Wires);
	}

	public char EncipherCharacter(char c)
	{
        if (EncipherWires.TryGetValue(c, out var value))
            return value;
        else
            return c;
	}

	public char DecipherCharacter(char c)
	{
        if (EncipherWires.TryGetKey(c, out var value))
            return value;
        else
            return c;
	}
}
