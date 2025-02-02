// ReSharper disable MemberCanBePrivate.Global

namespace Enigma;

/// <summary>
/// Virtual plug board used to send a character to be enciphered.
/// Performs a character swap before sending to the rotors.
/// Performs a final character swap after the enciphered character returns from the rotors.
/// Assignments are reciprocal; if A => G, then G => A.
/// </summary>
public sealed class PlugBoard
{
    public Dictionary<char,char> Wires => InternalWireswires;

    private Dictionary<char,char> InternalWireswires { get; set; } = [];
    private bool IsInitialized { get; set; }
    private Dictionary<char,char> EncipherWires { get; } = [];

    /// <summary>
    /// Establish reciprocal dictionary entries.
    /// </summary>
    /// <returns></returns>
    private void Initialize()
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

	public PlugBoard SetWires(Dictionary<char,char> value)
	{
        InternalWireswires = value;

        Initialize();

        return this;
	}

    public char SendCharacter(char c)
    {
        if (IsInitialized == false)
            Initialize();
        
        return EncipherWires.GetValueOrDefault(c, c);
    }
}
