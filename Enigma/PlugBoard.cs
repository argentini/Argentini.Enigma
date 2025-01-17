namespace Enigma;

/// <summary>
/// Virtual plug board used to send a character to be enciphered.
/// Performs a character swap before sending to the rotors.
/// Performs a final character swap after the enciphered character returns from the rotors.
/// </summary>
public sealed class PlugBoard
{
    public Dictionary<char,char> Wires { get; set; } = [];
    private Dictionary<char,char> IncomingWires { get; set; } = [];

    /// <summary>
    /// Establish IncomingWires dictionary which inverts the provided Wires values
    /// so the hashed keys can be used for faster searches when chars return for final output.
    /// </summary>
    /// <returns></returns>
	public async ValueTask ResetAsync()
	{
        if (IncomingWires.Count > 0)
            IncomingWires.Clear();

        foreach (var w in Wires.ToDictionary())
            if (IncomingWires.TryAdd(w.Value, w.Key) == false)
                throw new Exception("EnigmaPlugBoard => Duplicate wire value used.");

        await Task.CompletedTask;
	}

	public char CharacterIn(char c)
	{
        if (Wires.TryGetValue(c, out var value))
            return value;
        else
            return c;
	}

	public char CharacterOut(char c)
	{
        if (IncomingWires.TryGetValue(c, out var value))
            return value;
        else
            return c;
	}
}
