using System.Text;

namespace Enigma;

/// <summary>
/// Engima Machine entry point for encipher/decipher.
/// </summary>
public class Machine
{
    #region Configuration

    public PlugBoard PlugBoard { get; set; } = new();
    public Dictionary<int,Rotor> Rotors { get; set; } = [];
    public Reflector? Reflector { get; set; }

    public Machine AddPlugBoard(Dictionary<char,char> wires)
    {
        PlugBoard.SetWires(wires);

        return this;
    }

    public Machine AddRotor(RotorConfiguration configuration)
    {
        Rotors.Add(Rotors.Count, new Rotor(configuration));

        return this;
    }

    public Machine AddReflector(ReflectorConfiguration configuration)
    {
        Reflector = new Reflector(configuration);

        return this;
    }
    
    #endregion

    public void Reset()
    {
        foreach (var t in Rotors)
        {
            t.Value.ResetRotation();
        }
    }
    
    public string Encipher(string text)
    {
        if (Rotors.Count == 0)
            throw new Exception("Enigma Machine => No rotors configured");

        if (Reflector is null)
            throw new Exception("Enigma Machine => No reflector configured");

        var enciphered = new StringBuilder();

        foreach (var c in text)
        {
            Rotors[0].Rotate();

            if (Rotors.Count > 1 && Rotors[0].IsAtNotch)
            {
                for (var i = 1; i < Rotors.Count; i++)
                {
                    if (Rotors[i - 1].IsAtNotch)
                        Rotors[i].Rotate();
                }
            }

            var cc = PlugBoard.SendCharacter(c);

            foreach (var t in Rotors.OrderBy(r => r.Key))
            {
                cc = t.Value.SendCharacter(cc);
            }
            
            cc = Reflector.SendCharacter(cc);

            foreach (var t in Rotors.OrderByDescending(r => r.Key))
            {
                cc = t.Value.ReflectedCharacter(cc);
            }

            cc = PlugBoard.SendCharacter(cc);
            
            enciphered.Append(cc);
        }

        return enciphered.ToString();
    }
}
