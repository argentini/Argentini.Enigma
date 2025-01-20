namespace Enigma;

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

public enum ReflectorType
{
    // Default reflectors used by the Wehrmacht and Luftwaffe
    Wehrmacht_B,
    Wehrmacht_C,

    // Additional rotors used by Kriegsmarine M4 only
    Kriegsmarine_M4_B_Thin,
    Kriegsmarine_M4_C_Thin,

    // Standard 95 ASCII character reflector
    Ascii,
}

public static class Constants
{
    public static readonly Dictionary<RotorType, string> RotorTypeCiphers = new()
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

    public static readonly Dictionary<CharacterSet, string> CharacterSetValues = new()
    {
        { CharacterSet.Classic, "ABCDEFGHIJKLMNOPQRSTUVWXYZ" },
        { CharacterSet.Ascii, @" !""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~" },
    };
    
    public static readonly Dictionary<ReflectorType, string> ReflectorValues = new()
    {
        { ReflectorType.Wehrmacht_B, "YRUHQSLDPXNGOKMIEBFZCWVJAT" },
        { ReflectorType.Wehrmacht_C, "FVPJIAOYEDRZXWGCTKUQSBNMHL" },
        { ReflectorType.Kriegsmarine_M4_B_Thin, "ENKQAUYWJICOPBLMDXZVFTHRGS" },
        { ReflectorType.Kriegsmarine_M4_C_Thin, "RDOBJNTKVEHMLFCWZAXGYIPSUQ" },
        { ReflectorType.Ascii , @"BIK}p/h@b58fMo_%i3c1P)sH*?XwCm{9'J <vL|g7!A""E,~T4zSROWZU:tVdjxk.la(2[r+G&0\^`=q-$ne6YuD;]yQ>F#N" },
    };
    
    
    
    
    
    
    
}