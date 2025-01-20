namespace Enigma;

/// <summary>
/// Virtual rotor used to encipher a letter.
/// One or more rotors can be used in sequence.
/// Assignments are NOT reciprocal; if A => G, then G => (not) A.
/// </summary>
public sealed class Rotor
{
    public int RingPosition { get; private set; }
    public int Rotation { get; private set; }
    public int Notch1 { get; set; } = -1;
    public int Notch2 { get; set; } = -1;
    public bool IsAtNotch { get; private set; }
    public Dictionary<char,char> Wheel => _wheel;

    private Dictionary<char,char> _wheel { get; set; } = [];
    private IndexedDictionary<char,char> EncipherWheel { get; } = new();
    private bool IsInitialized { get; set; }

    #region Configuration
    
	private void Initialize()
	{
        EncipherWheel.Clear();

        if (_wheel.Count == 0)
            return;

        EncipherWheel.AddRange(_wheel, RingPosition);

        IsInitialized = true;
    }

	public Rotor SetWheel(Dictionary<char,char> value, int notch1 = -1, int notch2 = -1)
	{
        if (value.Count == 0)
            throw new Exception("Rotor.SetWheel() => Value is empty");

        _wheel = value;

        if (notch1 >= 0 && notch1 < _wheel.Count)
            Notch1 = notch1;
        else
            Notch1 = 0;

        if (notch2 >= 0 && notch2 < _wheel.Count && notch2 != Notch1)
            Notch2 = notch2;
        else
            Notch2 = -1;
        
        Initialize();

        return this;
	}

    public Rotor SetRingPosition(int value)
    {
        if (value >= 0 && value < _wheel.Count)
            RingPosition = value;
        else
            RingPosition = 0;

        if (RingPosition > 0)
        {
            Notch1 += RingPosition;

            if (Notch1 >= _wheel.Count)
                Notch1 -= _wheel.Count;

            if (Notch2 >= 0)
            {
                Notch2 += RingPosition;
                
                if (Notch2 >= _wheel.Count)
                    Notch2 -= _wheel.Count;
            }
        }
        
        Initialize();

        return this;
    }

	public Rotor SetRotation(int value)
	{
        if (value >= 0 && value < _wheel.Count)
            Rotation = value;
        else
            Rotation = 0;

        return this;
	}
    
    #endregion
    
    #region Actions
    
    public void Rotate()
    {
        Rotation++;

        if (Rotation >= _wheel.Count)
            Rotation = 0;

        IsAtNotch = Notch1 == Rotation || Notch2 == Rotation;
    }

	public char SendCharacter(char c)
	{
        if (IsInitialized == false)
            Initialize();

        IsAtNotch = false;

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
    
    #endregion
}
