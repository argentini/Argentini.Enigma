// ReSharper disable InconsistentNaming

namespace Enigma;

public enum CharacterSets
{
    Classic,
    Ascii,
}

public enum MachinePresets
{
    // Commercial Enigma (1924)
    Commercial_1924,

    // Wehrmacht and Kriegsmarine (1930)
    Wehrmacht_Kriegsmarine_1930,

    // Wehrmacht and Kriegsmarine (1938)
    Wehrmacht_Kriegsmarine_1938,

    // Swiss K (1939)
    Swiss_K_1939,

    // Kriegsmarine M3 and M4 (1939)
    Kriegsmarine_M3_1939,

    // German Railway (Rocket; 1941)
    German_Railway_Rocket_1941,
    
    // Kriegsmarine M4 with thin reflectors:
    Kriegsmarine_M4_1941,

    // Standard 95 ASCII characters
    Modern_Ascii,
}

public enum RotorPresets
{
    // Commercial Enigma (1924)
    Commercial_I,
    Commercial_II,
    Commercial_III,

    // Swiss K (1939)
    Swiss_K_I,
    Swiss_K_II,
    Swiss_K_III,

    // German Railway (Rocket; 1941)
    RailwayRocket_I,
    RailwayRocket_II,
    RailwayRocket_III,
    
    // Wehrmacht and Kriegsmarine (1930)
    Wehrmacht_I,
    Wehrmacht_II,
    Wehrmacht_III,

    // Wehrmacht and Kriegsmarine (1938)
    Wehrmacht_IV,
    Wehrmacht_V,

    // Kriegsmarine M3 and M4 only (1939)
    Kriegsmarine_M3_M4_VI,
    Kriegsmarine_M3_M4_VII,
    Kriegsmarine_M3_M4_VIII,
        
    // The special fourth rotors, also called Zusatzwalzen or Greek rotors (1941-42)
    // Used on the Kriegsmarine M4 with thin reflectors only:
    Zusatzwalzen_Beta,
    Zusatzwalzen_Gamma,

    // Standard 95 ASCII characters
    Ascii_I,
    Ascii_II,
    Ascii_III,
}

public enum ReflectorPresets
{
    // Swiss K (1939)
    Swiss_K_UKW,

    // German Railway (Rocket; 1941)
    Railway_Rocket_UKW,

    // Default reflectors used by the Wehrmacht and Luftwaffe (1939)
    Wehrmacht_A,
    Wehrmacht_B,
    Wehrmacht_C,

    // Additional rotors used by Kriegsmarine M4 only (1940)
    Kriegsmarine_M4_B_Thin,
    Kriegsmarine_M4_C_Thin,

    // Standard 95 ASCII character reflector
    Ascii,
}

public enum EntryWheelPresets
{
    Commercial_ETW,
    Swiss_K_ETW,
    Railway_Rocket_ETW,
    Wehrmacht,
    Kriegsmarine,
    Ascii,
}

public static class Constants
{
    public static readonly Dictionary<CharacterSets, string> CharacterSetValues = new Dictionary<CharacterSets, string>
    {
        { CharacterSets.Classic, "ABCDEFGHIJKLMNOPQRSTUVWXYZ" },
        { CharacterSets.Ascii, @" !""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~" },
    };

    public static readonly Dictionary<RotorPresets, string> RotorPresetsCiphers = new Dictionary<RotorPresets, string>
    {
        { RotorPresets.Commercial_I, "DMTWSILRUYQNKFEJCAZBPGXOHV" },
        { RotorPresets.Commercial_II, "HQZGPJTMOBLNCIFDYAWVEUSRKX" },
        { RotorPresets.Commercial_III, "UQNTLSZFMREHDPXKIBVYGJCWOA" },

        { RotorPresets.Swiss_K_I, "PEZUOHXSCVFMTBGLRINQJWAYDK" },
        { RotorPresets.Swiss_K_II, "ZOUESYDKFWPCIQXHMVBLGNJRAT" },
        { RotorPresets.Swiss_K_III, "EHRVXGAOBQUSIMZFLYNWKTPDJC" },

        { RotorPresets.RailwayRocket_I, "JGDQOXUSCAMIFRVTPNEWKBLZYH" },
        { RotorPresets.RailwayRocket_II, "NTZPSFBOKMWRCJDIVLAEYUXHGQ" },
        { RotorPresets.RailwayRocket_III, "JVIUBHTCDYAKEQZPOSGXNRMWFL" },

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

    public static readonly Dictionary<RotorPresets, string> TurnoverNotchPositions = new Dictionary<RotorPresets, string>
    {
        { RotorPresets.Commercial_I, "16,-1" },
        { RotorPresets.Commercial_II, "4,-1" },
        { RotorPresets.Commercial_III, "21,-1" },

        { RotorPresets.Swiss_K_I, "16,-1" },
        { RotorPresets.Swiss_K_II, "4,-1" },
        { RotorPresets.Swiss_K_III, "21,-1" },

        { RotorPresets.RailwayRocket_I, "16,-1" },
        { RotorPresets.RailwayRocket_II, "4,-1" },
        { RotorPresets.RailwayRocket_III, "21,-1" },
        
        { RotorPresets.Wehrmacht_I, "16,-1" },
        { RotorPresets.Wehrmacht_II, "4,-1" },
        { RotorPresets.Wehrmacht_III, "21,-1" },
        { RotorPresets.Wehrmacht_IV, "9,-1" },
        { RotorPresets.Wehrmacht_V, "25,-1" },

        { RotorPresets.Kriegsmarine_M3_M4_VI, "12,25" },
        { RotorPresets.Kriegsmarine_M3_M4_VII, "12,25" },
        { RotorPresets.Kriegsmarine_M3_M4_VIII, "12,25" },
        
        { RotorPresets.Zusatzwalzen_Beta, "0,-1" },
        { RotorPresets.Zusatzwalzen_Gamma, "0,-1" },
        
        { RotorPresets.Ascii_I , "47,94" },
        { RotorPresets.Ascii_II , "33,80" },
        { RotorPresets.Ascii_III , "15,45" },
    };

    public static readonly Dictionary<ReflectorPresets, string> ReflectorPresetsCiphers = new Dictionary<ReflectorPresets, string>
    {
        { ReflectorPresets.Swiss_K_UKW, "IMETCGFRAYSQBZXWLHKDVUPOJN" },

        { ReflectorPresets.Railway_Rocket_UKW, "QYHOGNECVPUZTFDJAXWMKISRBL" },

        { ReflectorPresets.Wehrmacht_A, "EJMZALYXVBWFCRQUONTSPIKHGD" },
        { ReflectorPresets.Wehrmacht_B, "YRUHQSLDPXNGOKMIEBFZCWVJAT" },
        { ReflectorPresets.Wehrmacht_C, "FVPJIAOYEDRZXWGCTKUQSBNMHL" },
        
        { ReflectorPresets.Kriegsmarine_M4_B_Thin, "ENKQAUYWJICOPBLMDXZVFTHRGS" },
        { ReflectorPresets.Kriegsmarine_M4_C_Thin, "RDOBJNTKVEHMLFCWZAXGYIPSUQ" },

        { ReflectorPresets.Ascii , @"BIK}p/h@b58fMo_%i3c1P)sH*?XwCm{9'J <vL|g7!A""E,~T4zSROWZU:tVdjxk.la(2[r+G&0\^`=q-$ne6YuD;]yQ>F#N" },
    };

    public static readonly Dictionary<EntryWheelPresets, string> EntryWheelPresetsCiphers = new Dictionary<EntryWheelPresets, string>
    {
        { EntryWheelPresets.Commercial_ETW, "ABCDEFGHIJKLMNOPQRSTUVWXYZ" },
        { EntryWheelPresets.Swiss_K_ETW, "QWERTZUIOASDFGHJKPYXCVBNML" },
        { EntryWheelPresets.Railway_Rocket_ETW, "QWERTZUIOASDFGHJKPYXCVBNML" },
        { EntryWheelPresets.Wehrmacht, "QWERTZUIOPASDFGHJKLYXCVBNM" },
        { EntryWheelPresets.Kriegsmarine, "JGDQVUBSLAPITKENXWHMFORCZY" },
        { EntryWheelPresets.Ascii, @"t~|MYy26z-@L(uP/vkI_SC"".x0A=<#K%!b[94^U&7TpG')>cHFdR1ae*5?{,+X`lV}\OW:qgisn]Bm$8Z3EJho Dw;rQjfN" },
    };
}