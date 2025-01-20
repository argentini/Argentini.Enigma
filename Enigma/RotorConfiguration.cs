namespace Enigma;

public sealed class RotorConfiguration
{
    public int RingPosition { get; set; }
    public int StartingRotation { get; set; }
    public int NotchPosition1 { get; set; } = 0;
    public int NotchPosition2 { get; set; } = -1;

    public string Secret { get; set; } = string.Empty;
    public string Nonce { get; set; } = string.Empty;
    public CharacterSets CharacterSets { get; set; } = CharacterSets.Ascii;
    
    public Dictionary<char, char> RotorWheel { get; } = [];
    public RotorPresets? RotorPreset { get; set; }
    
    public void Initialize()
    {
        if (RotorPreset is not null)
        {        
            var charSet = RotorPreset is RotorPresets.Ascii_I or RotorPresets.Ascii_II or RotorPresets.Ascii_III ? Constants.CharacterSetValues[CharacterSets.Ascii] : Constants.CharacterSetValues[CharacterSets.Classic];
            
            RotorWheel.Clear();
        
            for (var i = 0; i < charSet.Length; i++)
                RotorWheel.Add(charSet[i], Constants.RotorPresetsCiphers[RotorPreset.Value][i]);
        }
        else if (string.IsNullOrEmpty(Secret) == false && string.IsNullOrEmpty(Nonce) == false)
        {
            if (Secret.Length < 32)
                throw new Exception("RotorConfiguration.GenerateRotor() => key must be at least 32 characters");

            if (Nonce.Length < 16)
                throw new Exception("RotorConfiguration.GenerateRotor() => nonce must be at least 16 characters");

            var aesCtrRng = new AesCtrRandomNumberGenerator(Secret, Nonce);
            var characters = CharacterSets is CharacterSets.Ascii ? Constants.CharacterSetValues[CharacterSets.Ascii] : Constants.CharacterSetValues[CharacterSets.Classic];
            var cipher = new string(characters.OrderBy(_ => aesCtrRng.NextUInt32()).ToArray());

            RotorWheel.Clear();

            for (var i = 0; i < characters.Length; i++)
                RotorWheel.TryAdd(characters[i], cipher[i]);
        }
    }
}