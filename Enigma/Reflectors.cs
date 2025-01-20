namespace Enigma;

public static class Reflectors
{
    public static Dictionary<char, char> GetReflector(ReflectorType type)
    {
        var reflector = new Dictionary<char, char>();
        var charSet = type is ReflectorType.Ascii ? Constants.CharacterSetValues[CharacterSet.Ascii] : Constants.CharacterSetValues[CharacterSet.Classic];
        var cipher = Constants.ReflectorValues[type];

        foreach (var c in charSet)
            reflector.TryAdd(c, cipher[charSet.IndexOf(c)]);

        return reflector;
    }
    
    public static Dictionary<char,char> GenerateReflector(CharacterSet charSet)
    {
        var characterSetValue = charSet is CharacterSet.Ascii ? Constants.CharacterSetValues[CharacterSet.Ascii] : Constants.CharacterSetValues[CharacterSet.Classic];
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
