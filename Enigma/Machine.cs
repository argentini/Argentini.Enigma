using System.Text;
using MethodTimer;
using Microsoft.Extensions.ObjectPool;

// ReSharper disable MemberCanBePrivate.Global

namespace Enigma;

/// <summary>
/// Enigma Machine entry point for encipher/decipher.
/// </summary>
public class Machine
{
    #region Configuration

    public PlugBoard PlugBoard { get; set; } = new();
    public EntryWheel? EntryWheel { get; set; }
    public Dictionary<int,Rotor> Rotors { get; set; } = [];
    public Reflector? Reflector { get; set; }
    public AesCtrRandomNumberGenerator? AesGenerator { get; set; }

    private ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPoolProvider().CreateStringBuilderPool();

    /// <summary>
    /// Constructor used when manually building a machine with chained (fluent-style) .Add...() methods.
    /// </summary>
    public Machine()
    {
    }

    /// <summary>
    /// Used to create a historical machine from a preset.
    /// </summary>
    /// <param name="configuration"></param>
    public Machine(MachineConfiguration configuration)
    {
        AddPlugBoard(configuration.PlugBoardWires);

        switch (configuration.MachinePreset)
        {
            case MachinePresets.Commercial_1924:

                AddEntryWheel(new EntryWheelConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    EntryWheelPreset = EntryWheelPresets.Commercial_ETW
                });

                Rotors.Add(0, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Commercial_I,
                    RingPosition = configuration.Rotor1RingPosition,
                    StartingRotation = configuration.Rotor1StartingRotation
                }));

                Rotors.Add(1, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Commercial_II,
                    RingPosition = configuration.Rotor2RingPosition,
                    StartingRotation = configuration.Rotor2StartingRotation
                }));

                Rotors.Add(2, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Commercial_III,
                    RingPosition = configuration.Rotor3RingPosition,
                    StartingRotation = configuration.Rotor3StartingRotation
                }));

                AddReflector(new ReflectorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    ReflectorPreset = ReflectorPresets.Wehrmacht_A
                });

                break;

            case MachinePresets.Wehrmacht_Kriegsmarine_1930:
                
                AddEntryWheel(new EntryWheelConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    EntryWheelPreset = EntryWheelPresets.Wehrmacht
                });

                Rotors.Add(0, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Wehrmacht_I,
                    RingPosition = configuration.Rotor1RingPosition,
                    StartingRotation = configuration.Rotor1StartingRotation
                }));

                Rotors.Add(1, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Wehrmacht_II,
                    RingPosition = configuration.Rotor2RingPosition,
                    StartingRotation = configuration.Rotor2StartingRotation
                }));

                Rotors.Add(2, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Wehrmacht_III,
                    RingPosition = configuration.Rotor3RingPosition,
                    StartingRotation = configuration.Rotor3StartingRotation
                }));

                AddReflector(new ReflectorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    ReflectorPreset = ReflectorPresets.Wehrmacht_A
                });

                break;

            case MachinePresets.Wehrmacht_Kriegsmarine_1938:
                
                AddEntryWheel(new EntryWheelConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    EntryWheelPreset = EntryWheelPresets.Wehrmacht
                });

                Rotors.Add(0, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Wehrmacht_III,
                    RingPosition = configuration.Rotor1RingPosition,
                    StartingRotation = configuration.Rotor1StartingRotation
                }));

                Rotors.Add(1, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Wehrmacht_IV,
                    RingPosition = configuration.Rotor2RingPosition,
                    StartingRotation = configuration.Rotor2StartingRotation
                }));

                Rotors.Add(2, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Wehrmacht_V,
                    RingPosition = configuration.Rotor3RingPosition,
                    StartingRotation = configuration.Rotor3StartingRotation
                }));

                AddReflector(new ReflectorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    ReflectorPreset = ReflectorPresets.Wehrmacht_B
                });

                break;

            case MachinePresets.Swiss_K_1939:
                
                AddEntryWheel(new EntryWheelConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    EntryWheelPreset = EntryWheelPresets.Swiss_K_ETW
                });

                Rotors.Add(0, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Swiss_K_I,
                    RingPosition = configuration.Rotor1RingPosition,
                    StartingRotation = configuration.Rotor1StartingRotation
                }));

                Rotors.Add(1, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Swiss_K_II,
                    RingPosition = configuration.Rotor2RingPosition,
                    StartingRotation = configuration.Rotor2StartingRotation
                }));

                Rotors.Add(2, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Swiss_K_III,
                    RingPosition = configuration.Rotor3RingPosition,
                    StartingRotation = configuration.Rotor3StartingRotation
                }));

                AddReflector(new ReflectorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    ReflectorPreset = ReflectorPresets.Swiss_K_UKW
                });

                break;

            case MachinePresets.Kriegsmarine_M3_1939:

                AddEntryWheel(new EntryWheelConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    EntryWheelPreset = EntryWheelPresets.Kriegsmarine
                });

                Rotors.Add(0, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Wehrmacht_I,
                    RingPosition = configuration.Rotor1RingPosition,
                    StartingRotation = configuration.Rotor1StartingRotation
                }));

                Rotors.Add(1, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Kriegsmarine_M3_M4_VI,
                    RingPosition = configuration.Rotor2RingPosition,
                    StartingRotation = configuration.Rotor2StartingRotation
                }));

                Rotors.Add(2, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Kriegsmarine_M3_M4_VII,
                    RingPosition = configuration.Rotor3RingPosition,
                    StartingRotation = configuration.Rotor3StartingRotation
                }));

                Rotors.Add(3, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Kriegsmarine_M3_M4_VIII,
                    RingPosition = configuration.Rotor4RingPosition,
                    StartingRotation = configuration.Rotor4StartingRotation
                }));

                AddReflector(new ReflectorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    ReflectorPreset = ReflectorPresets.Kriegsmarine_M4_B_Thin
                });

                break;
            
            case MachinePresets.German_Railway_Rocket_1941:

                AddEntryWheel(new EntryWheelConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    EntryWheelPreset = EntryWheelPresets.Railway_Rocket_ETW
                });

                Rotors.Add(0, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.RailwayRocket_I,
                    RingPosition = configuration.Rotor1RingPosition,
                    StartingRotation = configuration.Rotor1StartingRotation
                }));

                Rotors.Add(1, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.RailwayRocket_II,
                    RingPosition = configuration.Rotor2RingPosition,
                    StartingRotation = configuration.Rotor2StartingRotation
                }));

                Rotors.Add(2, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.RailwayRocket_III,
                    RingPosition = configuration.Rotor3RingPosition,
                    StartingRotation = configuration.Rotor3StartingRotation
                }));

                AddReflector(new ReflectorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    ReflectorPreset = ReflectorPresets.Railway_Rocket_UKW
                });

                break;
            
            case MachinePresets.Kriegsmarine_M4_1941:
                
                AddEntryWheel(new EntryWheelConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    EntryWheelPreset = EntryWheelPresets.Kriegsmarine
                });

                Rotors.Add(0, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Wehrmacht_III,
                    RingPosition = configuration.Rotor1RingPosition,
                    StartingRotation = configuration.Rotor1StartingRotation
                }));

                Rotors.Add(1, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Kriegsmarine_M3_M4_VI,
                    RingPosition = configuration.Rotor2RingPosition,
                    StartingRotation = configuration.Rotor2StartingRotation
                }));

                Rotors.Add(2, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Kriegsmarine_M3_M4_VII,
                    RingPosition = configuration.Rotor3RingPosition,
                    StartingRotation = configuration.Rotor3StartingRotation
                }));

                Rotors.Add(3, new Rotor(new RotorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    RotorPreset = RotorPresets.Kriegsmarine_M3_M4_VIII,
                    RingPosition = configuration.Rotor4RingPosition,
                    StartingRotation = configuration.Rotor4StartingRotation
                }));

                AddReflector(new ReflectorConfiguration
                {
                    CharacterSet = CharacterSets.Classic, 
                    ReflectorPreset = ReflectorPresets.Kriegsmarine_M4_C_Thin
                });
                
                break;

            case MachinePresets.Modern_Ascii:
            
                AddEntryWheel(new EntryWheelConfiguration
                {
                    EntryWheelPreset = EntryWheelPresets.Ascii
                });

                Rotors.Add(0, new Rotor(new RotorConfiguration
                {
                    RotorPreset = RotorPresets.Ascii_I,
                    RingPosition = configuration.Rotor1RingPosition,
                    StartingRotation = configuration.Rotor1StartingRotation
                }));

                Rotors.Add(1, new Rotor(new RotorConfiguration
                {
                    RotorPreset = RotorPresets.Ascii_II,
                    RingPosition = configuration.Rotor2RingPosition,
                    StartingRotation = configuration.Rotor2StartingRotation
                }));

                Rotors.Add(2, new Rotor(new RotorConfiguration
                {
                    RotorPreset = RotorPresets.Ascii_III,
                    RingPosition = configuration.Rotor3RingPosition,
                    StartingRotation = configuration.Rotor3StartingRotation
                }));

                AddReflector(new ReflectorConfiguration
                {
                    ReflectorPreset = ReflectorPresets.Ascii
                });
            
                break;

            default:
                throw new Exception("Machine => Must specify a valid machine preset");
        }
    }

    /// <summary>
    /// Used to create a custom machine with a specified AES key and nonce.
    /// </summary>
    /// <param name="key">AES key must be 16, 24, or 32 bytes for AES-128, AES-192, or AES-256.</param>
    /// <param name="nonce">Nonce or initial counter value must be 16 bytes.</param>
    /// <param name="charSet"></param>
    /// <param name="rotorCount"></param>
    /// <param name="plugWires"></param>
    /// <exception cref="Exception"></exception>
    public Machine(string key, string nonce, CharacterSets charSet = CharacterSets.Ascii, int rotorCount = 3, int plugWires = 10)
    {
        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(nonce))
            throw new Exception("Machine => invalid secret or nonce");

        if (rotorCount < 1)
            throw new Exception("Machine => must specify 1 or more rotors");

        if (plugWires < 0)
            plugWires = 0;
        
        if (plugWires > Constants.CharacterSetValues[charSet].Length / 2)
            throw new Exception("Machine => too many plug wires specified");

        AesGenerator = new AesCtrRandomNumberGenerator(key, nonce);

        if (plugWires > 0)
        {
            var wires = new Dictionary<char, char>();
            var tempReflector = new Reflector(new ReflectorConfiguration
            {
                CharacterSet = charSet,
                AesGenerator = AesGenerator
            });

            for (var i = 0; i < plugWires; i++)
            {
                var pair = tempReflector.Configuration.ReflectorWheel.ElementAt(i);

                wires.Add(pair.Key, pair.Value);
            }

            AddPlugBoard(wires);
        }

        AddEntryWheel(new EntryWheelConfiguration
        {
            CharacterSet = charSet,
            AesGenerator = AesGenerator
        });

        for (var i = 0; i < rotorCount; i++)
        {
            Rotors.Add(i, new Rotor(new RotorConfiguration
            {
                CharacterSet = charSet,
                AesGenerator = AesGenerator
            }));
        }

        AddReflector(new ReflectorConfiguration
        {
            CharacterSet = charSet,
            AesGenerator = AesGenerator
        });
    }

    public Machine AddPlugBoard(Dictionary<char,char> wires)
    {
        PlugBoard.SetWires(wires);

        return this;
    }

    public Machine AddEntryWheel(EntryWheelConfiguration configuration)
    {
        EntryWheel = new EntryWheel(configuration);

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

    #region Actions
    
    /// <summary>
    /// Call this to reset the machine for another encipher operation.
    /// Effectively clears rotor rotations to their starting positions.
    /// </summary>
    public void Reset()
    {
        foreach (var t in Rotors)
            t.Value.ResetRotation();
    }
    
    /// <summary>
    /// Encipher/decipher a provided string.
    /// Call Reset() between each encipher and decipher call.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
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

                if (EntryWheel is not null)
                    cc = EntryWheel.SendCharacter(cc);

                cc = Rotors.OrderBy(r => r.Key).Aggregate(cc, (current, t) => t.Value.SendCharacter(current));
                cc = Reflector.SendCharacter(cc);
                cc = Rotors.OrderByDescending(r => r.Key).Aggregate(cc, (current, t) => t.Value.ReflectedCharacter(current));

                if (EntryWheel is not null)
                    cc = EntryWheel.ReflectedCharacter(cc);

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
    
    #endregion
}
