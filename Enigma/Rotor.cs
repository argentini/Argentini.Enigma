namespace Enigma;

/// <summary>
/// Virtual plug board used to send a character to be enciphered.
/// Performs a character swap before sending to the rotors.
/// Performs a final character swap after the enciphered character returns from the rotors.
/// Assignments are NOT reciprocal; if A => G, then G => (not) A.
/// </summary>
public sealed class Rotor
{
    public Dictionary<char,char> Wheel { get; set; } = [];

    private int _notchPosition = 0;
    public int NotchPosition
    {
        get => _notchPosition;
        set
        {
            if (value >= 0 && value < Wheel.Count)
            {
                if (_notchPosition != value)
                {
                    _notchPosition = value;
                    Reset();
                }
            }
            else
            {
                if (_notchPosition != value)
                {
                    _notchPosition = 0;
                    Reset();
                }
            }
        }
    }

    private int _rotation = 0;
    public int Rotation
    {
        get => _rotation;
        set
        {
            if (value >= 0 && value < Wheel.Count)
                _rotation = value;
            else
                _rotation = 0;
        }
    }

    private IndexedDictionary<char,char> EncipherWheel { get; set; } = new();

    /// <summary>
    /// Establish IncomingWheel dictionary which inverts the provided Wheel values
    /// so the hashed keys can be used for faster searches when chars return from the reflector.
    /// </summary>
    /// <returns></returns>
	public void Reset()
	{
        EncipherWheel.Clear();

        if (Wheel.Count == 0)
            return;

        EncipherWheel.AddRange(Wheel, NotchPosition);
	}

	public char EncipherCharacter(char c)
	{
        var originalIndex = EncipherWheel.KeyIndex[c];

        if (originalIndex == -1)
            throw new Exception($"Rotor.EncipherCharacter() => character is invalid ({c}).");

        int rotatedIndex = (originalIndex + Rotation) % EncipherWheel.Count;

        return EncipherWheel.KeyValues[rotatedIndex].Value;
	}

	public char DecipherCharacter(char c)
	{
        var originalIndex = EncipherWheel.ValueIndex[c];

        if (originalIndex == -1)
            throw new Exception($"Rotor.DecipherCharacter() => character is invalid ({c}).");

        int rotatedIndex = (originalIndex - Rotation + EncipherWheel.Count) % EncipherWheel.Count;

        return EncipherWheel.KeyValues[rotatedIndex].Key;
	}
}
