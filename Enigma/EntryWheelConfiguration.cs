// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Enigma;

public sealed class EntryWheelConfiguration
{
    public string Secret { get; set; } = string.Empty;
    public string Nonce { get; set; } = string.Empty;
    public AesCtrRandomNumberGenerator? AesGenerator { get; set; }
    public CharacterSets CharacterSet { get; set; } = CharacterSets.Ascii;
    
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
        else if (AesGenerator is not null || (string.IsNullOrEmpty(Secret) == false && string.IsNullOrEmpty(Nonce) == false))
        {
            switch (AesGenerator)
            {
                case null when Secret.Length < 32:
                    throw new Exception("EntryWheelConfiguration.Initialize() => key must be at least 32 characters");
                case null when Nonce.Length < 16:
                    throw new Exception("EntryWheelConfiguration.Initialize() => nonce must be at least 16 characters");
            }

            var aesCtrRng = AesGenerator ?? new AesCtrRandomNumberGenerator(Secret, Nonce);
            var characters = Constants.CharacterSetValues[CharacterSet];
            var cipher = new string(characters.OrderBy(_ => aesCtrRng.NextUInt32()).ToArray());

            EntryWheel.Clear();

            for (var i = 0; i < characters.Length; i++)
                EntryWheel.TryAdd(characters[i], cipher[i]);
        }
    }
}