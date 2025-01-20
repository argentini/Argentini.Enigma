namespace Enigma;

public class RotorConfiguration
{
    public int RingPosition { get; set; }
    public int StartingRotation { get; set; }
    public int NotchPosition1 { get; set; } = -1;
    public int NotchPosition2 { get; set; } = -1;
    public Dictionary<char, char> RotorWheel { get; set; } = [];

    public string CharacterSet => RotorWheel.Count == 26
        ? "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        : @" !""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
}