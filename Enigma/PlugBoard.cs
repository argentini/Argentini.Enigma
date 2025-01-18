namespace Enigma;

/// <summary>
/// Virtual plug board used to send a character to be enciphered.
/// Performs a character swap before sending to the rotors.
/// Performs a final character swap after the enciphered character returns from the rotors.
/// Assignments are reciprocal; if A => G, then G => A.
/// </summary>
public sealed class PlugBoard
{
    public Dictionary<char,char> Wires { get; set; } = [];

    private bool IsInitialized { get; set; }
    private Dictionary<char,char> EncipherWires { get; } = new();

    /// <summary>
    /// Establish reciprocal dictionary entries.
    /// </summary>
    /// <returns></returns>
	public void Reset()
	{
        EncipherWires.Clear();

        if (Wires.Count == 0)
            return;

        foreach (var c in Wires)
        {
            EncipherWires.TryAdd(c.Key, c.Value);
            EncipherWires.TryAdd(c.Value, c.Key);
        }

        IsInitialized = true;
    }

	public char SendCharacter(char c)
    {
        if (IsInitialized == false)
            Reset();
        
        return EncipherWires.GetValueOrDefault(c, c);
    }
}
