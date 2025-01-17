namespace Enigma;

/// <summary>
/// Virtual plug board used to send a character to be enciphered.
/// Performs a character swap before sending to the rotors.
/// Performs a final character swap after the enciphered character returns from the rotors.
/// </summary>
public sealed class PlugBoard
{
    public Dictionary<char,char> Wires { get; set; } = [];
    
    private Dictionary<char,char> EncipherWires { get; set; } = [];
    private Dictionary<char,char> DecipherWires { get; set; } = [];

    /// <summary>
    /// Establish IncomingWires dictionary which inverts the provided Wires values
    /// so the hashed keys can be used for faster searches when chars return for final output.
    /// </summary>
    /// <returns></returns>
	public void Reset()
	{
        EncipherWires = Wires.ToDictionary();
        DecipherWires.Clear();

        foreach (var w in EncipherWires)
            if (DecipherWires.TryAdd(w.Value, w.Key) == false)
                throw new Exception("PlugBoard => Duplicate wire value used.");
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
        if (DecipherWires.TryGetValue(c, out var value))
            return value;
        else
            return c;
	}
}
