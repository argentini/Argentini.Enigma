// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Enigma;

public sealed class ReflectorConfiguration
{
    public string Secret { get; set; } = string.Empty;
    public string Nonce { get; set; } = string.Empty;
    public CharacterSets CharacterSet { get; set; } = CharacterSets.Ascii;
    public AesCtrRandomNumberGenerator? AesGenerator { get; set; }
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
        else if (AesGenerator is not null || (string.IsNullOrEmpty(Secret) == false && string.IsNullOrEmpty(Nonce) == false))
        {
            switch (AesGenerator)
            {
                case null when Secret.Length < 32:
                    throw new Exception("ReflectorConfiguration.Initialize() => key must be at least 32 characters");
                case null when Nonce.Length < 16:
                    throw new Exception("ReflectorConfiguration.Initialize() => nonce must be at least 16 characters");
            }

            var aesCtrRng = AesGenerator ?? new AesCtrRandomNumberGenerator(Secret, Nonce);
            var characters = Constants.CharacterSetValues[CharacterSet];

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