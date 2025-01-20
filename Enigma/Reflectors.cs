namespace Enigma;

public static class Reflectors
{
    private static readonly Dictionary<ReflectorType, string> ReflectorValues = new()
    {
        { ReflectorType.Wehrmacht_B, "YRUHQSLDPXNGOKMIEBFZCWVJAT" },
        { ReflectorType.Wehrmacht_C, "FVPJIAOYEDRZXWGCTKUQSBNMHL" },
        { ReflectorType.Kriegsmarine_M4_B_Thin, "ENKQAUYWJICOPBLMDXZVFTHRGS" },
        { ReflectorType.Kriegsmarine_M4_C_Thin, "RDOBJNTKVEHMLFCWZAXGYIPSUQ" },
        { ReflectorType.Ascii , @"BIK}p/h@b58fMo_%i3c1P)sH*?XwCm{9'J <vL|g7!A""E,~T4zSROWZU:tVdjxk.la(2[r+G&0\^`=q-$ne6YuD;]yQ>F#N" },
    };

    private static readonly Dictionary<CharacterSet, string> CharacterSetValues = new()
    {
        { CharacterSet.Classic, "ABCDEFGHIJKLMNOPQRSTUVWXYZ" },
        { CharacterSet.Ascii, @" !""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~" },
    };

    public static Dictionary<char, char> GetReflector(ReflectorType type)
    {
        var reflector = new Dictionary<char, char>();
        var charSet = type is ReflectorType.Ascii ? CharacterSetValues[CharacterSet.Ascii] : CharacterSetValues[CharacterSet.Classic];
        var cipher = ReflectorValues[type];

        foreach (var c in charSet)
            reflector.TryAdd(c, cipher[charSet.IndexOf(c)]);

        return reflector;
    }
    
    public static Dictionary<char,char> GenerateReflector(CharacterSet charSet)
    {
        var characterSetValue = charSet is CharacterSet.Ascii ? CharacterSetValues[CharacterSet.Ascii] : CharacterSetValues[CharacterSet.Classic];
        var reflector = new Dictionary<char,char>();

        foreach (var c in characterSetValue)
            reflector.Add(c, '\0');

        foreach (var c in characterSetValue)
        {
            if (reflector[c] != '\0')
                continue;
            
            var ii = Random.Shared.Next(0, characterSetValue.Length);
            var cc = characterSetValue[ii];

            while (reflector[cc] != '\0')
            {
                ii = Random.Shared.Next(0, characterSetValue.Length);
                cc = characterSetValue[ii];
            }

            reflector[c] = cc;
            reflector[cc] = c;
        }

        return reflector;
    }
}
