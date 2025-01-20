namespace Enigma;

/// <summary>
/// Virtual rotor used to encipher a letter.
/// One or more rotors can be used in sequence.
/// Assignments are NOT reciprocal; if A => G, then G => (not) A.
/// </summary>
public sealed class Rotor
{
    public bool IsAtNotch { get; private set; }

    private int Rotation { get; set; }
    private int Notch1 { get; set; } = -1;
    private int Notch2 { get; set; } = -1;
    public RotorConfiguration Configuration { get; set; }
    private IndexedDictionary<char,char> EncipherWheel { get; } = new();

    public Rotor(RotorConfiguration configuration)
    {
        Configuration = configuration;
        
        Initialize();
    }
    
    #region Configuration
    
	private void Initialize()
    {
        Configuration.Initialize();

        if (Configuration.RotorWheel.Count == 0)
            throw new Exception("Rotor => Rotor Wheel is empty");

        #region Notches

        if (Configuration.RotorPreset is not null)
        {
            var notches = Constants.TurnoverNotchPositions[Configuration.RotorPreset.Value].Split(',');

            if (notches.Length != 2)
                throw new Exception("Rotor => Preset notch positions could not be read");
            
            if (int.TryParse(notches[0], out var notch1) == false)
                throw new Exception("Rotor => Preset notch position 1 is not an integer");

            if (int.TryParse(notches[1], out var notch2) == false)
                throw new Exception("Rotor => Preset notch position 2 is not an integer");

            Notch1 = notch1;
            Notch2 = notch2;
        }
        else
        {
            Notch1 = Configuration.NotchPosition1;
            Notch2 = Configuration.NotchPosition2;
        }

        if (Notch1 < 0 || Notch1 >= Configuration.RotorWheel.Count)
            throw new Exception($"Rotor => Rotor notch 1 position is invalid (0-{Configuration.RotorWheel.Count - 1})");

        if (Notch2 >= Configuration.RotorWheel.Count)
            throw new Exception($"Rotor => Rotor notch 2 position is invalid (0-{Configuration.RotorWheel.Count - 1} or -1 if not used)");

        if (Notch2 == Notch1)
            Notch2 = -1;

        #endregion

        #region Ring Position
        
        if (Configuration.RingPosition < 0 || Configuration.RingPosition >= Configuration.RotorWheel.Count)
            Configuration.RingPosition = 0;

        if (Configuration.RingPosition > 0)
        {
            Notch1 += Configuration.RingPosition;

            if (Notch1 >= Configuration.RotorWheel.Count)
                Notch1 -= Configuration.RotorWheel.Count;

            if (Notch2 >= 0)
            {
                Notch2 += Configuration.RingPosition;
                
                if (Notch2 >= Configuration.RotorWheel.Count)
                    Notch2 -= Configuration.RotorWheel.Count;
            }
        }
        
        #endregion
        
        #region Starting Rotation

        if (Configuration.StartingRotation < 0 || Configuration.StartingRotation >= Configuration.RotorWheel.Count)
            Configuration.StartingRotation = 0;

        Rotation = Configuration.StartingRotation;
        
        #endregion
        
        EncipherWheel.Clear();
        EncipherWheel.AddRange(Configuration.RotorWheel, Configuration.RingPosition);
    }
  
    #endregion
    
    #region Actions

    public void ResetRotation()
    {
        Rotation = Configuration.StartingRotation;
    }

    public void Rotate()
    {
        Rotation++;

        if (Rotation >= Configuration.RotorWheel.Count)
            Rotation = 0;

        IsAtNotch = Notch1 == Rotation || Notch2 == Rotation;
    }

	public char SendCharacter(char c)
	{
        if (IsAtNotch)
            IsAtNotch = false;

        if (EncipherWheel.KeyIndex.TryGetValue(c, out var originalIndex) == false)
            return c;

        var rotatedIndex = (originalIndex + Rotation) % EncipherWheel.Count;

        return EncipherWheel.KeyValues[rotatedIndex].Value;
	}

	public char ReflectedCharacter(char c)
	{
        if (EncipherWheel.ValueIndex.TryGetValue(c, out var originalIndex) == false)
            return c;

        var rotatedIndex = (originalIndex - Rotation + EncipherWheel.Count) % EncipherWheel.Count;

        return EncipherWheel.KeyValues[rotatedIndex].Key;
	}
    
    #endregion
}
