namespace Enigma;

public static class Rotors
{
    public enum RotorType
    {
        // Default rotors used by the Wehrmacht and Kriegsmarine
        Wehrmacht_I,
        Wehrmacht_II,
        Wehrmacht_III,
        Wehrmacht_IV,
        Wehrmacht_V,

        // Additional rotors used by Kriegsmarine M3 and M4 only
        Kriegsmarine_M3_M4_VI,
        Kriegsmarine_M3_M4_VII,
        Kriegsmarine_M3_M4_VIII,
        
        // The special fourth rotors, also called Zusatzwalzen or Greek rotors.
        // Used on the Kriegsmarine M4 with thin reflectors only:
        Zusatzwalzen_Beta,
        Zusatzwalzen_Gamma,

        // Standard 95 ASCII characters
        Ascii_I,
        Ascii_II,
        Ascii_III,
    }

    public enum CharacterSet
    {
        Classic,
        Ascii,
    }
    private static readonly Dictionary<CharacterSet, string> CharacterSetValues = new()
    {
        { CharacterSet.Classic, "ABCDEFGHIJKLMNOPQRSTUVWXYZ" },
        { CharacterSet.Ascii, @" !""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~" },
    };

    private static readonly Dictionary<RotorType, string> RotorValues = new()
    {
        { RotorType.Wehrmacht_I, "EKMFLGDQVZNTOWYHXUSPAIBRCJ" },
        { RotorType.Wehrmacht_II, "AJDKSIRUXBLHWTMCQGZNPYFVOE" },
        { RotorType.Wehrmacht_III, "BDFHJLCPRTXVZNYEIWGAKMUSQO" },
        { RotorType.Wehrmacht_IV, "ESOVPZJAYQUIRHXLNFTGKDCMWB" },
        { RotorType.Wehrmacht_V, "VZBRGITYUPSDNHLXAWMJQOFECK" },
        { RotorType.Kriegsmarine_M3_M4_VI, "JPGVOUMFYQBENHZRDKASXLICTW" },
        { RotorType.Kriegsmarine_M3_M4_VII, "NZJHGRCXMYSWBOUFAIVLPEKQDT" },
        { RotorType.Kriegsmarine_M3_M4_VIII, "FKQHTLXOCBJSPDZRAMEWNIUYGV" },
        { RotorType.Zusatzwalzen_Beta, "LEYJVCNIXWPBQMDRTAKZGFUHOS" },
        { RotorType.Zusatzwalzen_Gamma, "FSOKANUERHMBTIYCWLQPZXVGJD" },
        { RotorType.Ascii_I , @"nr.Fc[l;/vm+8M`'2=KG, -$ig?Q57V|saz~B_N19bL#Ap(WEOPZqH>""DUI}3uSTk&*^:<Xj{e@!C\hY]w60fyJ%)xt4Rdo" },
        { RotorType.Ascii_II , @"IHiapwjB#5n\*!|G?~l0P XehW&['N{YL<""f1oT7-2t_M694z/A>s(u+)JbEc@$%CZy:qrQK}d8vmR`Fx]S;kOV=g,U3^.D" },
        { RotorType.Ascii_III , @"':6/w`o8(fA5$)e!YDW>c%vb_P<SilB@L,kG;V^+jH7E&1pyM=FRm-hatz|#{rQq U.4""g~?NT3ZKn2\C9XIJ}d[Ous]0x*" },
    };
    
    public static Dictionary<char, char> GetRotor(RotorType type)
    {
        var rotor = new Dictionary<char, char>();
        var charSet = type is RotorType.Ascii_I or RotorType.Ascii_II or RotorType.Ascii_III ? CharacterSetValues[CharacterSet.Ascii] : CharacterSetValues[CharacterSet.Classic];

        for (var i = 0; i < charSet.Length; i++)
            rotor.Add(charSet[i], RotorValues[type][i]);

        return rotor;
    }
}
