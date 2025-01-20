using System.Text;
using MethodTimer;
using Microsoft.Extensions.ObjectPool;

namespace Enigma;

/// <summary>
/// Enigma Machine entry point for encipher/decipher.
/// </summary>
public class Machine
{
    #region Configuration

    public PlugBoard PlugBoard { get; set; } = new();
    public Dictionary<int,Rotor> Rotors { get; set; } = [];
    public Reflector? Reflector { get; set; }
    public AesCtrRandomNumberGenerator? Acrn { get; set; }

    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();
    
    public Machine()
    {
    }

    public Machine(string secret, string nonce, CharacterSets charSet = CharacterSets.Ascii, int rotorCount = 3, int plugWires = 10)
    {
        if (string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(nonce))
            throw new Exception("Machine => invalid secret or nonce");

        if (plugWires > Constants.CharacterSetValues[charSet].Length / 2)
            throw new Exception("Machine => too many plug wires specified");

        Acrn = new AesCtrRandomNumberGenerator(secret, nonce);

        if (plugWires > 0)
        {
            var wires = new Dictionary<char, char>();
            var reflector = new Reflector(new ReflectorConfiguration
            {
                CharacterSets = charSet,
                Acrn = Acrn
            });

            for (var i = 0; i < plugWires; i++)
            {
                var pair = reflector.Configuration.ReflectorWheel.ElementAt(i);

                wires.Add(pair.Key, pair.Value);
            }

            AddPlugBoard(wires);
        }
        
        for (var i = 0; i < rotorCount; i++)
        {
            Rotors.Add(i, new Rotor(new RotorConfiguration
            {
                CharacterSets = charSet,
                Acrn = Acrn
            }));
        }

        AddReflector(new ReflectorConfiguration
        {
            CharacterSets = charSet,
            Acrn = Acrn
        });
    }

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
    
    [Time]
    public string Encipher(string text)
    {
        if (Rotors.Count == 0)
            throw new Exception("Enigma Machine => No rotors configured");

        if (Reflector is null)
            throw new Exception("Enigma Machine => No reflector configured");

        var enciphered = StringBuilderPool.Get();

        try
        {
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
        finally
        {
            StringBuilderPool.Return(enciphered);
        }
    }
}
