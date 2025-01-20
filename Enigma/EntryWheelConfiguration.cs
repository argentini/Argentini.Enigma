namespace Enigma;

public sealed class EntryWheelConfiguration
{
    public string Secret { get; set; } = string.Empty;
    public string Nonce { get; set; } = string.Empty;
    public AesCtrRandomNumberGenerator? Acrn { get; set; }
    public CharacterSets CharacterSets { get; set; } = CharacterSets.Ascii;
    
    public Dictionary<char, char> EntryWheel { get; } = [];
    public EntryWheelPresets? EntryWheelPreset { get; set; }
    
    public void Initialize()
    {
        if (EntryWheelPreset is not null)
        {        
            var charSet = EntryWheelPreset is EntryWheelPresets.Ascii ? Constants.CharacterSetValues[CharacterSets.Ascii] : Constants.CharacterSetValues[CharacterSets.Classic];
            
            EntryWheel.Clear();
        
            for (var i = 0; i < charSet.Length; i++)
                EntryWheel.Add(charSet[i], Constants.EntryWheelPresetsCiphers[EntryWheelPreset.Value][i]);
        }
        else if (Acrn is not null || (string.IsNullOrEmpty(Secret) == false && string.IsNullOrEmpty(Nonce) == false))
        {
            if (Acrn is null && Secret.Length < 32)
                throw new Exception("EntryWheelConfiguration.Initialize() => key must be at least 32 characters");

            if (Acrn is null && Nonce.Length < 16)
                throw new Exception("EntryWheelConfiguration.Initialize() => nonce must be at least 16 characters");

            var aesCtrRng = Acrn ?? new AesCtrRandomNumberGenerator(Secret, Nonce);
            var characters = CharacterSets is CharacterSets.Ascii ? Constants.CharacterSetValues[CharacterSets.Ascii] : Constants.CharacterSetValues[CharacterSets.Classic];
            var cipher = new string(characters.OrderBy(_ => aesCtrRng.NextUInt32()).ToArray());

            EntryWheel.Clear();

            for (var i = 0; i < characters.Length; i++)
                EntryWheel.TryAdd(characters[i], cipher[i]);
        }
    }
}