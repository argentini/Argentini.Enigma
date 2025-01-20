namespace Enigma;

/// <summary>
/// Virtual entry wheel used to encipher a letter before passing it to the rotors.
/// Assignments are NOT reciprocal; if A => G, then G => (not) A.
/// </summary>
public sealed class EntryWheel
{
    public EntryWheelConfiguration Configuration { get; set; }
    private IndexedDictionary<char,char> EncipherWheel { get; } = new();

    public EntryWheel(EntryWheelConfiguration configuration)
    {
        Configuration = configuration;
        
        Initialize();
    }
    
    #region Configuration
    
	private void Initialize()
    {
        Configuration.Initialize();

        if (Configuration.EntryWheel.Count == 0)
            throw new Exception("EntryWheel => Entry Wheel is empty");

        EncipherWheel.Clear();
        EncipherWheel.AddRange(Configuration.EntryWheel);
    }
  
    #endregion
    
    #region Actions

	public char SendCharacter(char c)
	{
        if (EncipherWheel.KeyIndex.TryGetValue(c, out var index) == false)
            return c;

        return EncipherWheel.KeyValues[index].Value;
	}

	public char ReflectedCharacter(char c)
	{
        if (EncipherWheel.ValueIndex.TryGetValue(c, out var index) == false)
            return c;

        return EncipherWheel.KeyValues[index].Key;
	}
    
    #endregion
}
