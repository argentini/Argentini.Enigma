namespace Enigma;

public enum RotorPresets
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

public enum CharacterSets
{
    Classic,
    Ascii,
}

public enum ReflectorPresets
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
    public static readonly Dictionary<RotorPresets, string> RotorPresetsCiphers = new()
    {
        { RotorPresets.Wehrmacht_I, "EKMFLGDQVZNTOWYHXUSPAIBRCJ" },
        { RotorPresets.Wehrmacht_II, "AJDKSIRUXBLHWTMCQGZNPYFVOE" },
        { RotorPresets.Wehrmacht_III, "BDFHJLCPRTXVZNYEIWGAKMUSQO" },
        { RotorPresets.Wehrmacht_IV, "ESOVPZJAYQUIRHXLNFTGKDCMWB" },
        { RotorPresets.Wehrmacht_V, "VZBRGITYUPSDNHLXAWMJQOFECK" },
        { RotorPresets.Kriegsmarine_M3_M4_VI, "JPGVOUMFYQBENHZRDKASXLICTW" },
        { RotorPresets.Kriegsmarine_M3_M4_VII, "NZJHGRCXMYSWBOUFAIVLPEKQDT" },
        { RotorPresets.Kriegsmarine_M3_M4_VIII, "FKQHTLXOCBJSPDZRAMEWNIUYGV" },
        { RotorPresets.Zusatzwalzen_Beta, "LEYJVCNIXWPBQMDRTAKZGFUHOS" },
        { RotorPresets.Zusatzwalzen_Gamma, "FSOKANUERHMBTIYCWLQPZXVGJD" },
        { RotorPresets.Ascii_I , @"nr.Fc[l;/vm+8M`'2=KG, -$ig?Q57V|saz~B_N19bL#Ap(WEOPZqH>""DUI}3uSTk&*^:<Xj{e@!C\hY]w60fyJ%)xt4Rdo" },
        { RotorPresets.Ascii_II , @"IHiapwjB#5n\*!|G?~l0P XehW&['N{YL<""f1oT7-2t_M694z/A>s(u+)JbEc@$%CZy:qrQK}d8vmR`Fx]S;kOV=g,U3^.D" },
        { RotorPresets.Ascii_III , @"':6/w`o8(fA5$)e!YDW>c%vb_P<SilB@L,kG;V^+jH7E&1pyM=FRm-hatz|#{rQq U.4""g~?NT3ZKn2\C9XIJ}d[Ous]0x*" },
    };

    public static readonly Dictionary<CharacterSets, string> CharacterSetValues = new()
    {
        { CharacterSets.Classic, "ABCDEFGHIJKLMNOPQRSTUVWXYZ" },
        { CharacterSets.Ascii, @" !""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~" },
    };
    
    public static readonly Dictionary<ReflectorPresets, string> ReflectorPresetsValues = new()
    {
        { ReflectorPresets.Wehrmacht_B, "YRUHQSLDPXNGOKMIEBFZCWVJAT" },
        { ReflectorPresets.Wehrmacht_C, "FVPJIAOYEDRZXWGCTKUQSBNMHL" },
        { ReflectorPresets.Kriegsmarine_M4_B_Thin, "ENKQAUYWJICOPBLMDXZVFTHRGS" },
        { ReflectorPresets.Kriegsmarine_M4_C_Thin, "RDOBJNTKVEHMLFCWZAXGYIPSUQ" },
        { ReflectorPresets.Ascii , @"BIK}p/h@b58fMo_%i3c1P)sH*?XwCm{9'J <vL|g7!A""E,~T4zSROWZU:tVdjxk.la(2[r+G&0\^`=q-$ne6YuD;]yQ>F#N" },
    };
    
    
    
    
    
    
    
}