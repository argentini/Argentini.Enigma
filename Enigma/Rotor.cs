namespace Enigma;

/// <summary>
/// Virtual plug board used to send a character to be enciphered.
/// Performs a character swap before sending to the rotors.
/// Performs a final character swap after the enciphered character returns from the rotors.
/// Assignments are NOT reciprocal; if A => G, then G => (not) A.
/// </summary>
public sealed class Rotor
{
    public int NotchPosition => _notchPosition;
    public int Rotation => _rotation;
    public Dictionary<char,char> Wheel => _wheel;

    private Dictionary<char,char> _wheel { get; set; } = [];
    private int _notchPosition;
    private int _rotation;
    private bool IsInitialized { get; set; }
    private IndexedDictionary<char,char> EncipherWheel { get; set; } = new();

    /// <summary>
    /// Prepare the rotor for use; done at start and after the notch position changes.
    /// </summary>
    /// <returns></returns>
	private void Initialize()
	{
        EncipherWheel.Clear();

        if (_wheel.Count == 0)
            return;

        EncipherWheel.AddRange(_wheel, NotchPosition);

        IsInitialized = true;
    }

	public Rotor SetWheel(Dictionary<char,char> value)
	{
        _wheel = value;

        Initialize();

        return this;
	}

	public Rotor SetRotation(int value)
	{
        if (value >= 0 && value < _wheel.Count)
            _rotation = value;
        else
            _rotation = 0;

        return this;
	}

	public Rotor Rotate()
	{
        _rotation++;

        if (_rotation >= _wheel.Count)
            _rotation = 0;

        return this;
	}

	public Rotor SetNotchPosition(int value)
	{
        if (value >= 0 && value < _wheel.Count)
            _notchPosition = value;
        else
            _notchPosition = 0;

        Initialize();

        return this;
	}

	public char SendCharacter(char c)
	{
        if (IsInitialized == false)
            Initialize();
        
        if (EncipherWheel.KeyIndex.TryGetValue(c, out var originalIndex) == false)
            return c;

        var rotatedIndex = (originalIndex + Rotation) % EncipherWheel.Count;

        return EncipherWheel.KeyValues[rotatedIndex].Value;
	}

	public char ReflectedCharacter(char c)
	{
        if (IsInitialized == false)
            Initialize();

        if (EncipherWheel.ValueIndex.TryGetValue(c, out var originalIndex) == false)
            return c;

        var rotatedIndex = (originalIndex - Rotation + EncipherWheel.Count) % EncipherWheel.Count;

        return EncipherWheel.KeyValues[rotatedIndex].Key;
	}
}
