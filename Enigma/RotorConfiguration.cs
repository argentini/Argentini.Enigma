namespace Enigma;

public class RotorConfiguration
{
    public int RingPosition { get; set; }
    public int StartingRotation { get; set; }
    public int NotchPosition1 { get; set; } = -1;
    public int NotchPosition2 { get; set; } = -1;

    public string Secret { get; set; } = string.Empty;
    public string Nonce { get; set; } = string.Empty;
    public CharacterSet CharacterSet { get; set; } = CharacterSet.Ascii;
    
    public Dictionary<char, char> RotorWheel { get; set; } = [];
    public RotorType? RotorPreset { get; set; }
    
    public void Initialize()
    {
        if (RotorPreset is not null)
        {        
            var charSet = RotorPreset is RotorType.Ascii_I or RotorType.Ascii_II or RotorType.Ascii_III ? Constants.CharacterSetValues[CharacterSet.Ascii] : Constants.CharacterSetValues[CharacterSet.Classic];
            
            RotorWheel.Clear();
        
            for (var i = 0; i < charSet.Length; i++)
                RotorWheel.Add(charSet[i], Constants.RotorTypeCiphers[RotorPreset.Value][i]);
        }
        else if (string.IsNullOrEmpty(Secret) == false && string.IsNullOrEmpty(Nonce) == false)
        {
            if (Secret.Length < 32)
                throw new Exception("RotorConfiguration.GenerateRotor() => key must be at least 32 characters");

            if (Nonce.Length < 16)
                throw new Exception("RotorConfiguration.GenerateRotor() => nonce must be at least 16 characters");

            var aesCtrRng = new AesCtrRandomNumberGenerator(Secret, Nonce);
            var characters = CharacterSet is CharacterSet.Ascii ? Constants.CharacterSetValues[CharacterSet.Ascii] : Constants.CharacterSetValues[CharacterSet.Classic];
            var cipher = new string(characters.OrderBy(_ => aesCtrRng.NextUInt32()).ToArray());

            RotorWheel.Clear();

            for (var i = 0; i < characters.Length; i++)
                RotorWheel.TryAdd(characters[i], cipher[i]);
        }
    }
}