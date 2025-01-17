namespace Enigma;

/// <summary>
/// Virtual plug board used to send a character to be enciphered.
/// Performs a character swap before sending to the rotors.
/// Performs a final character swap after the enciphered character returns from the rotors.
/// </summary>
public sealed class Rotor
{
    public Dictionary<char,char> Wheel { get; set; } = [];
    public int NotchPosition
    {
        get => _notchPosition;
        set
        {
            if (value >= 0 && value < Wheel.Count)
            {
                if (_notchPosition != value)
                {
                    _notchPosition = value;
                    Reset();
                }
            }
            else
            {
                if (_notchPosition != value)
                {
                    _notchPosition = 0;
                    Reset();
                }
            }
        }
    }

    private int _notchPosition = 0;
    private Dictionary<char,char> EncipherWheel { get; set; } = [];
    private Dictionary<char,char> DecipherWheel { get; set; } = [];

    /// <summary>
    /// Establish IncomingWheel dictionary which inverts the provided Wheel values
    /// so the hashed keys can be used for faster searches when chars return from the reflector.
    /// </summary>
    /// <returns></returns>
	public void Reset()
	{
        if (Wheel.Count == 0)
            return;

        EncipherWheel = Wheel.ToDictionary();
        DecipherWheel.Clear();

        // Process notch

        if (NotchPosition > 0 && NotchPosition < Wheel.Count)
        {
            var i = 0;
            var vi = NotchPosition;

            do
            {
                EncipherWheel[Wheel.ElementAt(i++).Key] = Wheel.ElementAt(vi++).Value;

                if (vi == Wheel.Count)
                    vi = 0;

            } while (vi != NotchPosition);
        }        

        foreach (var w in EncipherWheel.ToDictionary())
            if (DecipherWheel.TryAdd(w.Value, w.Key) == false)
                throw new Exception("Rotor => Duplicate incoming wheel value used.");
	}

	public char EncipherCharacter(char c)
	{
        if (EncipherWheel.TryGetValue(c, out var value))
            return value;
        else
            throw new Exception($"Rotor.EncipherCharacter() => character is invalid ({c}).");
	}

	public char DecipherCharacter(char c)
	{
        if (DecipherWheel.TryGetValue(c, out var value))
            return value;
        else
            throw new Exception($"Rotor.DecipherCharacter() => character is invalid ({c}).");
	}
}
