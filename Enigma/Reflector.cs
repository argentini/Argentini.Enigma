namespace Enigma;

/// <summary>
/// Virtual reflector used to route a character back to the light board.
/// Positioned left at the end of the rotors.
/// Assignments are reciprocal; if A => G, then G => A.
/// </summary>
public sealed class Reflector
{
    public Dictionary<char,char> Wires => _wires;

    private Dictionary<char,char> _wires { get; set; } = [];
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
            EncipherWires.TryAdd(c.Key, c.Value);

        IsInitialized = true;
    }

	public Reflector SetWires(Dictionary<char,char> value)
	{
        _wires = value;

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
