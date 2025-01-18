namespace Enigma;

public static class Constants
{
    // Entire supported character set
    public static string CharacterSet => @" !""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~";

    #region Rotors
    
    // Default rotors used by the Wehrmacht and Kriegsmarine
    public static string[] DefaultRotors => [
        "EKMFLGDQVZNTOWYHXUSPAIBRCJ",
        "AJDKSIRUXBLHWTMCQGZNPYFVOE",
        "BDFHJLCPRTXVZNYEIWGAKMUSQO",
        "ESOVPZJAYQUIRHXLNFTGKDCMWB",
        "VZBRGITYUPSDNHLXAWMJQOFECK",

        // Additional rotors used by Kriegsmarine M3 and M4 only
        "JPGVOUMFYQBENHZRDKASXLICTW",
        "NZJHGRCXMYSWBOUFAIVLPEKQDT",
        "FKQHTLXOCBJSPDZRAMEWNIUYGV",
        
        // The special fourth rotors, also called Zusatzwalzen or Greek rotors.
        // Used on the Kriegsmarine M4 with thin reflectors only:
        "LEYJVCNIXWPBQMDRTAKZGFUHOS",
        "FSOKANUERHMBTIYCWLQPZXVGJD"
    ];

    public static Dictionary<char,char> ClassicRotor1 = new()
    {
        {'A', 'E'},
        {'B', 'K'},
        {'C', 'M'},
        {'D', 'F'},
        {'E', 'L'},
        {'F', 'G'},
        {'G', 'D'},
        {'H', 'Q'},
        {'I', 'V'},
        {'J', 'Z'},
        {'K', 'N'},
        {'L', 'T'},
        {'M', 'O'},
        {'N', 'W'},
        {'O', 'Y'},
        {'P', 'H'},
        {'Q', 'X'},
        {'R', 'U'},
        {'S', 'S'},
        {'T', 'P'},
        {'U', 'A'},
        {'V', 'I'},
        {'W', 'B'},
        {'X', 'R'},
        {'Y', 'C'},
        {'Z', 'J'},
    };

    public static Dictionary<char,char> Rotor1 = new()
    {
        {' ', '*'},
        {'!', 'd'},
        {'"', 'l'},
        {'#', ';'},
        {'$', 'i'},
        {'%', '-'},
        {'&', '_'},
        {'\'', '3'},
        {'(', '1'},
        {')', '4'},
        {'*', 'm'},
        {'+', ' '},
        {',', '\''},
        {'-', 'w'},
        {'.', '\\'},
        {'/', '!'},
        {'0', 'j'},
        {'1', '#'},
        {'2', '+'},
        {'3', 'v'},
        {'4', 's'},
        {'5', 'y'},
        {'6', 'f'},
        {'7', 'q'},
        {'8', 'u'},
        {'9', '0'},
        {':', 'n'},
        {';', '&'},
        {'<', 'p'},
        {'=', '^'},
        {'>', '('},
        {'?', '`'},
        {'@', '@'},
        {'A', 'E'},
        {'B', 'K'},
        {'C', 'M'},
        {'D', 'F'},
        {'E', 'L'},
        {'F', 'G'},
        {'G', 'D'},
        {'H', 'Q'},
        {'I', 'V'},
        {'J', 'Z'},
        {'K', 'N'},
        {'L', 'T'},
        {'M', 'O'},
        {'N', 'W'},
        {'O', 'Y'},
        {'P', 'H'},
        {'Q', 'X'},
        {'R', 'U'},
        {'S', 'S'},
        {'T', 'P'},
        {'U', 'A'},
        {'V', 'I'},
        {'W', 'B'},
        {'X', 'R'},
        {'Y', 'C'},
        {'Z', 'J'},
        {'[', '8'},
        {'\\', 'e'},
        {']', 'z'},
        {'^', '6'},
        {'_', 'c'},
        {'`', '}'},
        {'a', '2'},
        {'b', 'x'},
        {'c', '='},
        {'d', '>'},
        {'e', '<'},
        {'f', '/'},
        {'g', 'k'},
        {'h', 'o'},
        {'i', '9'},
        {'j', 'a'},
        {'k', '{'},
        {'l', ')'},
        {'m', '~'},
        {'n', 'h'},
        {'o', '['},
        {'p', 't'},
        {'q', '?'},
        {'r', ']'},
        {'s', ':'},
        {'t', '.'},
        {'u', '7'},
        {'v', 'g'},
        {'w', 'b'},
        {'x', ','},
        {'y', '5'},
        {'z', 'r'},
        {'{', '"'},
        {'|', '$'},
        {'}', '|'},
        {'~', '%'},
    };
    
    public static Dictionary<char,char> Rotor2 = new()
    {
        {' ', '~'},
        {'!', '#'},
        {'"', '`'},
        {'#', ','},
        {'$', '2'},
        {'%', '>'},
        {'&', ']'},
        {'\'', 'a'},
        {'(', '.'},
        {')', '6'},
        {'*', 'w'},
        {'+', 'h'},
        {',', 'x'},
        {'-', 'd'},
        {'.', '['},
        {'/', '4'},
        {'0', '7'},
        {'1', '&'},
        {'2', 's'},
        {'3', 'r'},
        {'4', '*'},
        {'5', '$'},
        {'6', 't'},
        {'7', '9'},
        {'8', '%'},
        {'9', '1'},
        {':', '='},
        {';', 'q'},
        {'<', 'k'},
        {'=', '8'},
        {'>', 'b'},
        {'?', '-'},
        {'@', '^'},
        {'A', 'A'},
        {'B', 'J'},
        {'C', 'D'},
        {'D', 'K'},
        {'E', 'S'},
        {'F', 'I'},
        {'G', 'R'},
        {'H', 'U'},
        {'I', 'X'},
        {'J', 'B'},
        {'K', 'L'},
        {'L', 'H'},
        {'M', 'W'},
        {'N', 'T'},
        {'O', 'M'},
        {'P', 'C'},
        {'Q', 'Q'},
        {'R', 'G'},
        {'S', 'Z'},
        {'T', 'N'},
        {'U', 'P'},
        {'V', 'Y'},
        {'W', 'F'},
        {'X', 'V'},
        {'Y', 'O'},
        {'Z', 'E'},
        {'[', '_'},
        {'\\', '\\'},
        {']', 'e'},
        {'^', 'i'},
        {'_', 'p'},
        {'`', 'j'},
        {'a', 'l'},
        {'b', 'u'},
        {'c', 'c'},
        {'d', '|'},
        {'e', '5'},
        {'f', 'n'},
        {'g', ':'},
        {'h', '@'},
        {'i', 'o'},
        {'j', 'f'},
        {'k', '+'},
        {'l', '!'},
        {'m', '?'},
        {'n', '}'},
        {'o', '/'},
        {'p', '<'},
        {'q', 'z'},
        {'r', 'v'},
        {'s', ')'},
        {'t', '3'},
        {'u', '('},
        {'v', 'm'},
        {'w', ' '},
        {'x', '"'},
        {'y', ';'},
        {'z', 'y'},
        {'{', '0'},
        {'|', 'g'},
        {'}', '\''},
        {'~', '{'},
    };
    
    public static Dictionary<char,char> Rotor3 = new()
    {
        {' ', 'm'},
        {'!', '_'},
        {'"', 'e'},
        {'#', '-'},
        {'$', 'j'},
        {'%', '\\'},
        {'&', '('},
        {'\'', '5'},
        {'(', '7'},
        {')', 'q'},
        {'*', 'r'},
        {'+', '`'},
        {',', 'o'},
        {'-', 'h'},
        {'.', 'y'},
        {'/', 'b'},
        {'0', '='},
        {'1', '.'},
        {'2', 'l'},
        {'3', '~'},
        {'4', ']'},
        {'5', 'f'},
        {'6', 'c'},
        {'7', '8'},
        {'8', '>'},
        {'9', '2'},
        {':', 'i'},
        {';', 'x'},
        {'<', '"'},
        {'=', 'u'},
        {'>', '}'},
        {'?', '\''},
        {'@', 'w'},
        {'A', 'B'},
        {'B', 'D'},
        {'C', 'F'},
        {'D', 'H'},
        {'E', 'J'},
        {'F', 'L'},
        {'G', 'C'},
        {'H', 'P'},
        {'I', 'R'},
        {'J', 'T'},
        {'K', 'X'},
        {'L', 'V'},
        {'M', 'Z'},
        {'N', 'N'},
        {'O', 'Y'},
        {'P', 'E'},
        {'Q', 'I'},
        {'R', 'W'},
        {'S', 'G'},
        {'T', 'A'},
        {'U', 'K'},
        {'V', 'M'},
        {'W', 'U'},
        {'X', 'S'},
        {'Y', 'Q'},
        {'Z', 'O'},
        {'[', '#'},
        {'\\', '%'},
        {']', '/'},
        {'^', 'v'},
        {'_', '{'},
        {'`', '@'},
        {'a', '1'},
        {'b', '?'},
        {'c', ' '},
        {'d', 'z'},
        {'e', '^'},
        {'f', '<'},
        {'g', '*'},
        {'h', '4'},
        {'i', '&'},
        {'j', 'k'},
        {'k', 'd'},
        {'l', '['},
        {'m', '9'},
        {'n', ';'},
        {'o', '+'},
        {'p', ':'},
        {'q', 't'},
        {'r', '3'},
        {'s', '0'},
        {'t', 'a'},
        {'u', 'g'},
        {'v', '!'},
        {'w', ')'},
        {'x', 'n'},
        {'y', '6'},
        {'z', 's'},
        {'{', ','},
        {'|', '|'},
        {'}', 'p'},
        {'~', '$'},
    };
    
    public static Dictionary<char,char> Rotor4 = new()
    {
        {' ', ':'},
        {'!', '^'},
        {'"', '\''},
        {'#', '['},
        {'$', 'p'},
        {'%', '\\'},
        {'&', '_'},
        {'\'', '/'},
        {'(', 'k'},
        {')', '!'},
        {'*', 'u'},
        {'+', '%'},
        {',', '='},
        {'-', '$'},
        {'.', 'z'},
        {'/', '+'},
        {'0', '>'},
        {'1', '7'},
        {'2', 't'},
        {'3', ']'},
        {'4', '|'},
        {'5', 'a'},
        {'6', 'b'},
        {'7', '"'},
        {'8', 'n'},
        {'9', '{'},
        {':', '*'},
        {';', '<'},
        {'<', '('},
        {'=', 'd'},
        {'>', '.'},
        {'?', ')'},
        {'@', '1'},
        {'A', 'E'},
        {'B', 'S'},
        {'C', 'O'},
        {'D', 'V'},
        {'E', 'P'},
        {'F', 'Z'},
        {'G', 'J'},
        {'H', 'A'},
        {'I', 'Y'},
        {'J', 'Q'},
        {'K', 'U'},
        {'L', 'I'},
        {'M', 'R'},
        {'N', 'H'},
        {'O', 'X'},
        {'P', 'L'},
        {'Q', 'N'},
        {'R', 'F'},
        {'S', 'T'},
        {'T', 'G'},
        {'U', 'K'},
        {'V', 'D'},
        {'W', 'C'},
        {'X', 'M'},
        {'Y', 'W'},
        {'Z', 'B'},
        {'[', 'c'},
        {'\\', '8'},
        {']', '2'},
        {'^', 'v'},
        {'_', '9'},
        {'`', 'f'},
        {'a', '#'},
        {'b', '`'},
        {'c', '?'},
        {'d', '0'},
        {'e', '&'},
        {'f', 'h'},
        {'g', '6'},
        {'h', '5'},
        {'i', 'x'},
        {'j', 'l'},
        {'k', 'r'},
        {'l', 'w'},
        {'m', '~'},
        {'n', 'g'},
        {'o', 'y'},
        {'p', 'i'},
        {'q', 'e'},
        {'r', ';'},
        {'s', 'm'},
        {'t', '@'},
        {'u', '3'},
        {'v', ','},
        {'w', ' '},
        {'x', '}'},
        {'y', 's'},
        {'z', '4'},
        {'{', 'o'},
        {'|', 'q'},
        {'}', 'j'},
        {'~', '-'},
    };
    
    public static Dictionary<char,char> Rotor5 = new()
    {
        {' ', '7'},
        {'!', '9'},
        {'"', '>'},
        {'#', '_'},
        {'$', 'q'},
        {'%', '.'},
        {'&', '6'},
        {'\'', 's'},
        {'(', '{'},
        {')', '+'},
        {'*', '2'},
        {'+', 'x'},
        {',', ';'},
        {'-', 'm'},
        {'.', 'w'},
        {'/', 'z'},
        {'0', 'b'},
        {'1', '1'},
        {'2', '#'},
        {'3', 'd'},
        {'4', '^'},
        {'5', '<'},
        {'6', '*'},
        {'7', '('},
        {'8', '='},
        {'9', 'h'},
        {':', '`'},
        {';', 'a'},
        {'<', 'e'},
        {'=', 'y'},
        {'>', ','},
        {'?', 'c'},
        {'@', 'f'},
        {'A', 'V'},
        {'B', 'Z'},
        {'C', 'B'},
        {'D', 'R'},
        {'E', 'G'},
        {'F', 'I'},
        {'G', 'T'},
        {'H', 'Y'},
        {'I', 'U'},
        {'J', 'P'},
        {'K', 'S'},
        {'L', 'D'},
        {'M', 'N'},
        {'N', 'H'},
        {'O', 'L'},
        {'P', 'X'},
        {'Q', 'A'},
        {'R', 'W'},
        {'S', 'M'},
        {'T', 'J'},
        {'U', 'Q'},
        {'V', 'O'},
        {'W', 'F'},
        {'X', 'E'},
        {'Y', 'C'},
        {'Z', 'K'},
        {'[', '3'},
        {'\\', 'j'},
        {']', 'r'},
        {'^', 'k'},
        {'_', 'n'},
        {'`', '/'},
        {'a', '~'},
        {'b', '\''},
        {'c', '['},
        {'d', 't'},
        {'e', '-'},
        {'f', 'g'},
        {'g', '@'},
        {'h', '%'},
        {'i', '8'},
        {'j', '$'},
        {'k', 'o'},
        {'l', 'l'},
        {'m', ' '},
        {'n', '}'},
        {'o', ')'},
        {'p', '&'},
        {'q', '4'},
        {'r', ':'},
        {'s', '|'},
        {'t', ']'},
        {'u', 'v'},
        {'v', '0'},
        {'w', '?'},
        {'x', 'i'},
        {'y', '\\'},
        {'z', 'u'},
        {'{', '!'},
        {'|', 'p'},
        {'}', '"'},
        {'~', '5'},
    };

    // Additional rotors used by Kriegsmarine M3 and M4 only

    public static Dictionary<char,char> Rotor6 = new()
    {
        {' ', '7'},
        {'!', '2'},
        {'"', '|'},
        {'#', 'c'},
        {'$', 'z'},
        {'%', 't'},
        {'&', ':'},
        {'\'', ')'},
        {'(', '='},
        {')', '/'},
        {'*', '6'},
        {'+', '_'},
        {',', 'y'},
        {'-', 'a'},
        {'.', '\\'},
        {'/', '8'},
        {'0', 'f'},
        {'1', 'i'},
        {'2', 'o'},
        {'3', '"'},
        {'4', '%'},
        {'5', 'q'},
        {'6', '@'},
        {'7', 'j'},
        {'8', '`'},
        {'9', ','},
        {':', '#'},
        {';', '+'},
        {'<', 'u'},
        {'=', '['},
        {'>', 'e'},
        {'?', '{'},
        {'@', 'r'},
        {'A', 'J'},
        {'B', 'P'},
        {'C', 'G'},
        {'D', 'V'},
        {'E', 'O'},
        {'F', 'U'},
        {'G', 'M'},
        {'H', 'F'},
        {'I', 'Y'},
        {'J', 'Q'},
        {'K', 'B'},
        {'L', 'E'},
        {'M', 'N'},
        {'N', 'H'},
        {'O', 'Z'},
        {'P', 'R'},
        {'Q', 'D'},
        {'R', 'K'},
        {'S', 'A'},
        {'T', 'S'},
        {'U', 'X'},
        {'V', 'L'},
        {'W', 'I'},
        {'X', 'C'},
        {'Y', 'T'},
        {'Z', 'W'},
        {'[', '0'},
        {'\\', '>'},
        {']', '$'},
        {'^', 'h'},
        {'_', '1'},
        {'`', '.'},
        {'a', 'x'},
        {'b', 'p'},
        {'c', 'm'},
        {'d', ' '},
        {'e', '?'},
        {'f', 'b'},
        {'g', '^'},
        {'h', ']'},
        {'i', 'g'},
        {'j', ';'},
        {'k', 'w'},
        {'l', 'd'},
        {'m', '<'},
        {'n', '\''},
        {'o', '3'},
        {'p', '&'},
        {'q', 'n'},
        {'r', 'l'},
        {'s', '5'},
        {'t', 's'},
        {'u', '~'},
        {'v', 'v'},
        {'w', '9'},
        {'x', '-'},
        {'y', '!'},
        {'z', '}'},
        {'{', '4'},
        {'|', 'k'},
        {'}', '*'},
        {'~', '('},
    };
    
    public static Dictionary<char,char> Rotor7 = new()
    {
        {' ', 'a'},
        {'!', '^'},
        {'"', '['},
        {'#', '%'},
        {'$', '='},
        {'%', '.'},
        {'&', '8'},
        {'\'', '\\'},
        {'(', '2'},
        {')', 'r'},
        {'*', ';'},
        {'+', '5'},
        {',', 'c'},
        {'-', '4'},
        {'.', 'f'},
        {'/', '('},
        {'0', 'q'},
        {'1', '?'},
        {'2', ':'},
        {'3', ','},
        {'4', '3'},
        {'5', '*'},
        {'6', '}'},
        {'7', '#'},
        {'8', 'n'},
        {'9', 'l'},
        {':', '6'},
        {';', 'g'},
        {'<', 'u'},
        {'=', '1'},
        {'>', ' '},
        {'?', '7'},
        {'@', 'i'},
        {'A', 'N'},
        {'B', 'Z'},
        {'C', 'J'},
        {'D', 'H'},
        {'E', 'G'},
        {'F', 'R'},
        {'G', 'C'},
        {'H', 'X'},
        {'I', 'M'},
        {'J', 'Y'},
        {'K', 'S'},
        {'L', 'W'},
        {'M', 'B'},
        {'N', 'O'},
        {'O', 'U'},
        {'P', 'F'},
        {'Q', 'A'},
        {'R', 'I'},
        {'S', 'V'},
        {'T', 'L'},
        {'U', 'P'},
        {'V', 'E'},
        {'W', 'K'},
        {'X', 'Q'},
        {'Y', 'D'},
        {'Z', 'T'},
        {'[', 's'},
        {'\\', '{'},
        {']', '@'},
        {'^', '0'},
        {'_', 'v'},
        {'`', ')'},
        {'a', 'w'},
        {'b', 'p'},
        {'c', 'x'},
        {'d', 't'},
        {'e', ']'},
        {'f', 'k'},
        {'g', 'o'},
        {'h', 'b'},
        {'i', '\''},
        {'j', '_'},
        {'k', '+'},
        {'l', 'j'},
        {'m', '`'},
        {'n', 'm'},
        {'o', '~'},
        {'p', 'e'},
        {'q', 'z'},
        {'r', '<'},
        {'s', 'd'},
        {'t', '!'},
        {'u', 'h'},
        {'v', '|'},
        {'w', '/'},
        {'x', '-'},
        {'y', '&'},
        {'z', 'y'},
        {'{', '$'},
        {'|', '9'},
        {'}', '>'},
        {'~', '"'},
    };

    public static Dictionary<char,char> Rotor8 = new()
    {
        {' ', 'x'},
        {'!', '6'},
        {'"', '-'},
        {'#', 'v'},
        {'$', '('},
        {'%', 'i'},
        {'&', '|'},
        {'\'', 'd'},
        {'(', '$'},
        {')', ','},
        {'*', 'l'},
        {'+', '2'},
        {',', '#'},
        {'-', 'c'},
        {'.', 'p'},
        {'/', 'u'},
        {'0', ')'},
        {'1', ';'},
        {'2', '}'},
        {'3', 's'},
        {'4', 'b'},
        {'5', 'r'},
        {'6', '_'},
        {'7', 'g'},
        {'8', '@'},
        {'9', 'w'},
        {':', 'f'},
        {';', '^'},
        {'<', '='},
        {'=', '3'},
        {'>', 'j'},
        {'?', 't'},
        {'@', 'e'},
        {'A', 'F'},
        {'B', 'K'},
        {'C', 'Q'},
        {'D', 'H'},
        {'E', 'T'},
        {'F', 'L'},
        {'G', 'X'},
        {'H', 'O'},
        {'I', 'C'},
        {'J', 'B'},
        {'K', 'J'},
        {'L', 'S'},
        {'M', 'P'},
        {'N', 'D'},
        {'O', 'Z'},
        {'P', 'R'},
        {'Q', 'A'},
        {'R', 'M'},
        {'S', 'E'},
        {'T', 'W'},
        {'U', 'N'},
        {'V', 'I'},
        {'W', 'U'},
        {'X', 'Y'},
        {'Y', 'G'},
        {'Z', 'V'},
        {'[', '!'},
        {'\\', '&'},
        {']', '{'},
        {'^', '\\'},
        {'_', ']'},
        {'`', '?'},
        {'a', '\''},
        {'b', '%'},
        {'c', '>'},
        {'d', '.'},
        {'e', '+'},
        {'f', '0'},
        {'g', '*'},
        {'h', '/'},
        {'i', 'n'},
        {'j', 'q'},
        {'k', '"'},
        {'l', 'a'},
        {'m', ' '},
        {'n', '['},
        {'o', '~'},
        {'p', 'y'},
        {'q', '4'},
        {'r', '`'},
        {'s', '7'},
        {'t', '8'},
        {'u', 'o'},
        {'v', 'm'},
        {'w', ':'},
        {'x', 'k'},
        {'y', '1'},
        {'z', 'z'},
        {'{', '9'},
        {'|', 'h'},
        {'}', '<'},
        {'~', '5'},
    };

    // The special fourth rotors, also called Zusatzwalzen or Greek rotors.
    // Used on the Kriegsmarine M4 with thin reflectors only:

    public static Dictionary<char,char> RotorBeta = new()
    {
        {' ', 'g'},
        {'!', '"'},
        {'"', '{'},
        {'#', '.'},
        {'$', 'f'},
        {'%', '\''},
        {'&', 'r'},
        {'\'', 'w'},
        {'(', '!'},
        {')', 'k'},
        {'*', 'i'},
        {'+', '_'},
        {',', '7'},
        {'-', 'y'},
        {'.', 't'},
        {'/', 'e'},
        {'0', '@'},
        {'1', '('},
        {'2', '5'},
        {'3', '>'},
        {'4', 'j'},
        {'5', '^'},
        {'6', '-'},
        {'7', 'p'},
        {'8', ']'},
        {'9', '<'},
        {':', 'q'},
        {';', '3'},
        {'<', 'a'},
        {'=', '#'},
        {'>', '6'},
        {'?', '~'},
        {'@', '\\'},
        {'A', 'L'},
        {'B', 'E'},
        {'C', 'Y'},
        {'D', 'J'},
        {'E', 'V'},
        {'F', 'C'},
        {'G', 'N'},
        {'H', 'I'},
        {'I', 'X'},
        {'J', 'W'},
        {'K', 'P'},
        {'L', 'B'},
        {'M', 'Q'},
        {'N', 'M'},
        {'O', 'D'},
        {'P', 'R'},
        {'Q', 'T'},
        {'R', 'A'},
        {'S', 'K'},
        {'T', 'Z'},
        {'U', 'G'},
        {'V', 'F'},
        {'W', 'U'},
        {'X', 'H'},
        {'Y', 'O'},
        {'Z', 'S'},
        {'[', 'b'},
        {'\\', ' '},
        {']', ';'},
        {'^', '+'},
        {'_', '1'},
        {'`', 'h'},
        {'a', 's'},
        {'b', '['},
        {'c', '*'},
        {'d', 'x'},
        {'e', 'v'},
        {'f', 'o'},
        {'g', ','},
        {'h', '2'},
        {'i', 'd'},
        {'j', '|'},
        {'k', '`'},
        {'l', 'm'},
        {'m', '?'},
        {'n', 'z'},
        {'o', 'c'},
        {'p', 'n'},
        {'q', ')'},
        {'r', '/'},
        {'s', '0'},
        {'t', '='},
        {'u', '9'},
        {'v', '8'},
        {'w', '}'},
        {'x', '4'},
        {'y', 'u'},
        {'z', '%'},
        {'{', ':'},
        {'|', '$'},
        {'}', '&'},
        {'~', 'l'},
    };
    public static Dictionary<char,char> RotorGamma = new()
    {
        {' ', 'z'},
        {'!', ' '},
        {'"', 'h'},
        {'#', 'p'},
        {'$', 'm'},
        {'%', '/'},
        {'&', '\\'},
        {'\'', '?'},
        {'(', 'f'},
        {')', '&'},
        {'*', '\''},
        {'+', 'b'},
        {',', '%'},
        {'-', ','},
        {'.', 'k'},
        {'/', 'q'},
        {'0', '`'},
        {'1', 'e'},
        {'2', 'y'},
        {'3', 'v'},
        {'4', '+'},
        {'5', '*'},
        {'6', ':'},
        {'7', 'a'},
        {'8', ';'},
        {'9', '@'},
        {':', '6'},
        {';', '2'},
        {'<', '9'},
        {'=', 'n'},
        {'>', '-'},
        {'?', '['},
        {'@', '('},
        {'A', 'F'},
        {'B', 'S'},
        {'C', 'O'},
        {'D', 'K'},
        {'E', 'A'},
        {'F', 'N'},
        {'G', 'U'},
        {'H', 'E'},
        {'I', 'R'},
        {'J', 'H'},
        {'K', 'M'},
        {'L', 'B'},
        {'M', 'T'},
        {'N', 'I'},
        {'O', 'Y'},
        {'P', 'C'},
        {'Q', 'W'},
        {'R', 'L'},
        {'S', 'Q'},
        {'T', 'P'},
        {'U', 'Z'},
        {'V', 'X'},
        {'W', 'V'},
        {'X', 'G'},
        {'Y', 'J'},
        {'Z', 'D'},
        {'[', 'r'},
        {'\\', 'o'},
        {']', '7'},
        {'^', '"'},
        {'_', '|'},
        {'`', 'c'},
        {'a', 'g'},
        {'b', '8'},
        {'c', ')'},
        {'d', 'x'},
        {'e', '$'},
        {'f', '.'},
        {'g', '5'},
        {'h', '!'},
        {'i', 'i'},
        {'j', '>'},
        {'k', 'u'},
        {'l', '='},
        {'m', 'w'},
        {'n', 't'},
        {'o', 'j'},
        {'p', 'l'},
        {'q', '4'},
        {'r', '1'},
        {'s', '<'},
        {'t', '_'},
        {'u', 'd'},
        {'v', '}'},
        {'w', '~'},
        {'x', '3'},
        {'y', '^'},
        {'z', '{'},
        {'{', '0'},
        {'|', ']'},
        {'}', 's'},
        {'~', '#'},
    };  
    
    #endregion

    #region Reflectors

    public static string[] DefaultReflectors => [
        "YRUHQSLDPXNGOKMIEBFZCWVJAT",
        "FVPJIAOYEDRZXWGCTKUQSBNMHL",

        // Thin reflectors, Kriegsmarine M4 only:
        "ENKQAUYWJICOPBLMDXZVFTHRGS",
        "RDOBJNTKVEHMLFCWZAXGYIPSUQ"
    ];

    public static Dictionary<char,char> ClassicReflector1 = new()
    {
        {'A', 'Y'},
        {'B', 'R'},
        {'C', 'U'},
        {'D', 'H'},
        {'E', 'Q'},
        {'F', 'S'},
        {'G', 'L'},
        {'H', 'D'},
        {'I', 'P'},
        {'J', 'X'},
        {'K', 'N'},
        {'L', 'G'},
        {'M', 'O'},
        {'N', 'K'},
        {'O', 'M'},
        {'P', 'I'},
        {'Q', 'E'},
        {'R', 'B'},
        {'S', 'F'},
        {'T', 'Z'},
        {'U', 'C'},
        {'V', 'W'},
        {'W', 'V'},
        {'X', 'J'},
        {'Y', 'A'},
        {'Z', 'T'},
    };

    public static Dictionary<char,char> Reflector1 = new()
    {
        {' ', 's'},
        {'s', ' '},
        {'!', '~'},
        {'~', '!'},
        {'"', 'x'},
        {'x', '"'},
        {'#', '0'},
        {'0', '#'},
        {'$', '|'},
        {'|', '$'},
        {'%', '1'},
        {'1', '%'},
        {'&', '('},
        {'(', '&'},
        {'\'', '2'},
        {'2', '\''},
        {')', '3'},
        {'3', ')'},
        {'*', 'h'},
        {'h', '*'},
        {'+', 'o'},
        {'o', '+'},
        {',', 'w'},
        {'w', ','},
        {'-', '>'},
        {'>', '-'},
        {'.', 'y'},
        {'y', '.'},
        {'/', ']'},
        {']', '/'},
        {'4', 'd'},
        {'d', '4'},
        {'5', '<'},
        {'<', '5'},
        {'6', '^'},
        {'^', '6'},
        {'7', 'v'},
        {'v', '7'},
        {'8', 'g'},
        {'g', '8'},
        {'9', 'e'},
        {'e', '9'},
        {':', 'm'},
        {'m', ':'},
        {';', 't'},
        {'t', ';'},
        {'=', 'l'},
        {'l', '='},
        {'?', '_'},
        {'_', '?'},
        {'@', '`'},
        {'`', '@'},
        {'A', 'Y'},
        {'B', 'R'},
        {'C', 'U'},
        {'D', 'H'},
        {'E', 'Q'},
        {'F', 'S'},
        {'G', 'L'},
        {'H', 'D'},
        {'I', 'P'},
        {'J', 'X'},
        {'K', 'N'},
        {'L', 'G'},
        {'M', 'O'},
        {'N', 'K'},
        {'O', 'M'},
        {'P', 'I'},
        {'Q', 'E'},
        {'R', 'B'},
        {'S', 'F'},
        {'T', 'Z'},
        {'U', 'C'},
        {'V', 'W'},
        {'W', 'V'},
        {'X', 'J'},
        {'Y', 'A'},
        {'Z', 'T'},
        {'[', 'k'},
        {'k', '['},
        {'\\', 'p'},
        {'p', '\\'},
        {'a', 'z'},
        {'z', 'a'},
        {'b', 'r'},
        {'r', 'b'},
        {'c', 'i'},
        {'i', 'c'},
        {'f', 'j'},
        {'j', 'f'},
        {'n', '}'},
        {'}', 'n'},
        {'q', 'u'},
        {'u', 'q'},
    };

    public static Dictionary<char,char> Reflector2 = new()
    {
        {' ', '@'},
        {'@', ' '},
        {'!', '^'},
        {'^', '!'},
        {'"', 'f'},
        {'f', '"'},
        {'#', 'h'},
        {'h', '#'},
        {'$', '['},
        {'[', '$'},
        {'%', 'p'},
        {'p', '%'},
        {'&', '6'},
        {'6', '&'},
        {'\'', ':'},
        {':', '\''},
        {'(', 'l'},
        {'l', '('},
        {')', '*'},
        {'*', ')'},
        {'+', '5'},
        {'5', '+'},
        {',', '~'},
        {'~', ','},
        {'-', 'r'},
        {'r', '-'},
        {'.', 'x'},
        {'x', '.'},
        {'/', 'q'},
        {'q', '/'},
        {'0', '7'},
        {'7', '0'},
        {'1', '_'},
        {'_', '1'},
        {'2', '8'},
        {'8', '2'},
        {'3', '4'},
        {'4', '3'},
        {'9', 'j'},
        {'j', '9'},
        {';', 'b'},
        {'b', ';'},
        {'<', '>'},
        {'>', '<'},
        {'=', 'c'},
        {'c', '='},
        {'?', '}'},
        {'}', '?'},
        {'A', 'F'},
        {'B', 'V'},
        {'C', 'P'},
        {'D', 'J'},
        {'E', 'I'},
        {'F', 'A'},
        {'G', 'O'},
        {'H', 'Y'},
        {'I', 'E'},
        {'J', 'D'},
        {'K', 'R'},
        {'L', 'Z'},
        {'M', 'X'},
        {'N', 'W'},
        {'O', 'G'},
        {'P', 'C'},
        {'Q', 'T'},
        {'R', 'K'},
        {'S', 'U'},
        {'T', 'Q'},
        {'U', 'S'},
        {'V', 'B'},
        {'W', 'N'},
        {'X', 'M'},
        {'Y', 'H'},
        {'Z', 'L'},
        {'\\', 'k'},
        {'k', '\\'},
        {']', 's'},
        {'s', ']'},
        {'`', 'd'},
        {'d', '`'},
        {'a', 'z'},
        {'z', 'a'},
        {'e', '|'},
        {'|', 'e'},
        {'g', 'y'},
        {'y', 'g'},
        {'i', 'o'},
        {'o', 'i'},
        {'m', 'v'},
        {'v', 'm'},
        {'n', 't'},
        {'t', 'n'},
        {'u', '{'},
        {'{', 'u'},
    };
    
    public static Dictionary<char,char> Reflector3 = new()
    {
        {' ', 'c'},
        {'c', ' '},
        {'!', '&'},
        {'&', '!'},
        {'"', '+'},
        {'+', '"'},
        {'#', '{'},
        {'{', '#'},
        {'$', 'w'},
        {'w', '$'},
        {'%', 'f'},
        {'f', '%'},
        {'\'', 's'},
        {'s', '\''},
        {'(', 'o'},
        {'o', '('},
        {')', '='},
        {'=', ')'},
        {'*', '2'},
        {'2', '*'},
        {',', 'k'},
        {'k', ','},
        {'-', '@'},
        {'@', '-'},
        {'.', '7'},
        {'7', '.'},
        {'/', '>'},
        {'>', '/'},
        {'0', 'h'},
        {'h', '0'},
        {'1', '3'},
        {'3', '1'},
        {'4', ']'},
        {']', '4'},
        {'5', 'u'},
        {'u', '5'},
        {'6', 'b'},
        {'b', '6'},
        {'8', '`'},
        {'`', '8'},
        {'9', 'p'},
        {'p', '9'},
        {':', '['},
        {'[', ':'},
        {';', 'j'},
        {'j', ';'},
        {'<', 'i'},
        {'i', '<'},
        {'?', '}'},
        {'}', '?'},
        {'A', 'E'},
        {'B', 'N'},
        {'C', 'K'},
        {'D', 'Q'},
        {'E', 'A'},
        {'F', 'U'},
        {'G', 'Y'},
        {'H', 'W'},
        {'I', 'J'},
        {'J', 'I'},
        {'K', 'C'},
        {'L', 'O'},
        {'M', 'P'},
        {'N', 'B'},
        {'O', 'L'},
        {'P', 'M'},
        {'Q', 'D'},
        {'R', 'X'},
        {'S', 'Z'},
        {'T', 'V'},
        {'U', 'F'},
        {'V', 'T'},
        {'W', 'H'},
        {'X', 'R'},
        {'Y', 'G'},
        {'Z', 'S'},
        {'\\', 'z'},
        {'z', '\\'},
        {'^', '|'},
        {'|', '^'},
        {'_', 'm'},
        {'m', '_'},
        {'a', 't'},
        {'t', 'a'},
        {'d', 'x'},
        {'x', 'd'},
        {'e', 'r'},
        {'r', 'e'},
        {'g', 'y'},
        {'y', 'g'},
        {'l', 'v'},
        {'v', 'l'},
        {'n', '~'},
        {'~', 'n'},
    };
    
    public static Dictionary<char,char> Reflector4 = new()
    {
        {' ', 'd'},
        {'d', ' '},
        {'!', '\\'},
        {'\\', '!'},
        {'"', 't'},
        {'t', '"'},
        {'#', 'z'},
        {'z', '#'},
        {'$', 'i'},
        {'i', '$'},
        {'%', '+'},
        {'+', '%'},
        {'&', '>'},
        {'>', '&'},
        {'\'', 'q'},
        {'q', '\''},
        {'(', ']'},
        {']', '('},
        {')', 'b'},
        {'b', ')'},
        {'*', '}'},
        {'}', '*'},
        {',', '='},
        {'=', ','},
        {'-', '/'},
        {'/', '-'},
        {'.', '<'},
        {'<', '.'},
        {'0', 'x'},
        {'x', '0'},
        {'1', 'l'},
        {'l', '1'},
        {'2', '_'},
        {'_', '2'},
        {'3', ';'},
        {';', '3'},
        {'4', '~'},
        {'~', '4'},
        {'5', 'c'},
        {'c', '5'},
        {'6', 'k'},
        {'k', '6'},
        {'7', 'y'},
        {'y', '7'},
        {'8', '@'},
        {'@', '8'},
        {'9', 'u'},
        {'u', '9'},
        {':', '{'},
        {'{', ':'},
        {'?', 's'},
        {'s', '?'},
        {'A', 'R'},
        {'B', 'D'},
        {'C', 'O'},
        {'D', 'B'},
        {'E', 'J'},
        {'F', 'N'},
        {'G', 'T'},
        {'H', 'K'},
        {'I', 'V'},
        {'J', 'E'},
        {'K', 'H'},
        {'L', 'M'},
        {'M', 'L'},
        {'N', 'F'},
        {'O', 'C'},
        {'P', 'W'},
        {'Q', 'Z'},
        {'R', 'A'},
        {'S', 'X'},
        {'T', 'G'},
        {'U', 'Y'},
        {'V', 'I'},
        {'W', 'P'},
        {'X', 'S'},
        {'Y', 'U'},
        {'Z', 'Q'},
        {'[', 'j'},
        {'j', '['},
        {'^', 'e'},
        {'e', '^'},
        {'`', 'g'},
        {'g', '`'},
        {'a', 'r'},
        {'r', 'a'},
        {'f', 'n'},
        {'n', 'f'},
        {'h', 'w'},
        {'w', 'h'},
        {'m', 'o'},
        {'o', 'm'},
        {'p', '|'},
        {'|', 'p'},
    };
    
    #endregion
}