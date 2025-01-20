namespace Enigma;

/// <summary>
/// Virtual reflector used to route a character back to the light board.
/// Positioned left at the end of the rotors.
/// Assignments are reciprocal; if A => G, then G => A.
/// </summary>
public sealed class Reflector
{
    public ReflectorConfiguration Configuration { get; set; }

    public Reflector(ReflectorConfiguration configuration)
    {
        Configuration = configuration;
        
        Initialize();
    }

    private void Initialize()
    {
        Configuration.Initialize();
        
        if (Configuration.ReflectorWheel.Count == 0)
            throw new Exception("Reflector => Reflector Wheel is empty");
    }
    
    public char SendCharacter(char c)
    {
        return Configuration.ReflectorWheel.GetValueOrDefault(c, c);
    }
}
