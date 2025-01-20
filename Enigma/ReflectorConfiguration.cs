namespace Enigma;

public sealed class ReflectorConfiguration
{
    public string Secret { get; set; } = string.Empty;
    public string Nonce { get; set; } = string.Empty;
    public CharacterSets CharacterSets { get; set; } = CharacterSets.Ascii;
    public AesCtrRandomNumberGenerator? Acrn { get; set; } = null;    
    public Dictionary<char, char> ReflectorWheel { get; } = [];
    public ReflectorPresets? ReflectorPreset { get; set; }
    
    public void Initialize()
    {
        if (ReflectorPreset is not null)
        {        
            var charSet = ReflectorPreset is ReflectorPresets.Ascii ? Constants.CharacterSetValues[CharacterSets.Ascii] : Constants.CharacterSetValues[CharacterSets.Classic];
            var cipher = Constants.ReflectorPresetsCiphers[ReflectorPreset.Value];
            
            ReflectorWheel.Clear();

            foreach (var c in charSet)
                ReflectorWheel.TryAdd(c, cipher[charSet.IndexOf(c)]);
        }
        else if (Acrn is not null || (string.IsNullOrEmpty(Secret) == false && string.IsNullOrEmpty(Nonce) == false))
        {
            if (Acrn is null && Secret.Length < 32)
                throw new Exception("ReflectorConfiguration.Initialize() => key must be at least 32 characters");

            if (Acrn is null && Nonce.Length < 16)
                throw new Exception("ReflectorConfiguration.Initialize() => nonce must be at least 16 characters");

            var aesCtrRng = Acrn ?? new AesCtrRandomNumberGenerator(Secret, Nonce);
            var characters = CharacterSets is CharacterSets.Ascii ? Constants.CharacterSetValues[CharacterSets.Ascii] : Constants.CharacterSetValues[CharacterSets.Classic];

            ReflectorWheel.Clear();

            foreach (var c in characters)
                ReflectorWheel.Add(c, '\0');

            foreach (var c in characters)
            {
                if (ReflectorWheel[c] != '\0')
                    continue;
            
                var ii = aesCtrRng.NextInt32(0, characters.Length);
                var cc = characters[ii];

                while (ReflectorWheel[cc] != '\0')
                {
                    ii = aesCtrRng.NextInt32(0, characters.Length);
                    cc = characters[ii];
                }

                ReflectorWheel[c] = cc;
                ReflectorWheel[cc] = c;
            }
        }
    }
}