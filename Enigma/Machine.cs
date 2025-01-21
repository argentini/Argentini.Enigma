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
    public EntryWheel? EntryWheel { get; set; }
    public Dictionary<int,Rotor> Rotors { get; set; } = [];
    public Reflector? Reflector { get; set; }
    public AesCtrRandomNumberGenerator? Acrn { get; set; }

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
        
        if (configuration.MachinePreset == MachinePresets.Commercial_1924)
        {
            AddEntryWheel(new EntryWheelConfiguration
            {
                EntryWheelPreset = EntryWheelPresets.Commercial_ETW
            });

            Rotors.Add(0, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Commercial_I,
                RingPosition = configuration.Rotor1RingPosition,
                StartingRotation = configuration.Rotor1StartingRotation
            }));

            Rotors.Add(1, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Commercial_II,
                RingPosition = configuration.Rotor2RingPosition,
                StartingRotation = configuration.Rotor2StartingRotation
            }));

            Rotors.Add(2, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Commercial_III,
                RingPosition = configuration.Rotor3RingPosition,
                StartingRotation = configuration.Rotor3StartingRotation
            }));

            AddReflector(new ReflectorConfiguration
            {
                ReflectorPreset = ReflectorPresets.Wehrmacht_A
            });
        }
        else if (configuration.MachinePreset == MachinePresets.Wehrmacht_Kriegsmarine_1930)
        {
            AddEntryWheel(new EntryWheelConfiguration
            {
                EntryWheelPreset = EntryWheelPresets.Wehrmacht
            });

            Rotors.Add(0, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Wehrmacht_I,
                RingPosition = configuration.Rotor1RingPosition,
                StartingRotation = configuration.Rotor1StartingRotation
            }));

            Rotors.Add(1, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Wehrmacht_II,
                RingPosition = configuration.Rotor2RingPosition,
                StartingRotation = configuration.Rotor2StartingRotation
            }));

            Rotors.Add(2, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Wehrmacht_III,
                RingPosition = configuration.Rotor3RingPosition,
                StartingRotation = configuration.Rotor3StartingRotation
            }));

            AddReflector(new ReflectorConfiguration
            {
                ReflectorPreset = ReflectorPresets.Wehrmacht_A
            });
        }
        else if (configuration.MachinePreset == MachinePresets.Wehrmacht_Kriegsmarine_1938)
        {
            AddEntryWheel(new EntryWheelConfiguration
            {
                EntryWheelPreset = EntryWheelPresets.Wehrmacht
            });

            Rotors.Add(0, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Wehrmacht_III,
                RingPosition = configuration.Rotor1RingPosition,
                StartingRotation = configuration.Rotor1StartingRotation
            }));

            Rotors.Add(1, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Wehrmacht_IV,
                RingPosition = configuration.Rotor2RingPosition,
                StartingRotation = configuration.Rotor2StartingRotation
            }));

            Rotors.Add(2, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Wehrmacht_V,
                RingPosition = configuration.Rotor3RingPosition,
                StartingRotation = configuration.Rotor3StartingRotation
            }));

            AddReflector(new ReflectorConfiguration
            {
                ReflectorPreset = ReflectorPresets.Wehrmacht_B
            });
        }
        else if (configuration.MachinePreset == MachinePresets.Swiss_K_1939)
        {
            AddEntryWheel(new EntryWheelConfiguration
            {
                EntryWheelPreset = EntryWheelPresets.Swiss_K_ETW
            });

            Rotors.Add(0, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Swiss_K_I,
                RingPosition = configuration.Rotor1RingPosition,
                StartingRotation = configuration.Rotor1StartingRotation
            }));

            Rotors.Add(1, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Swiss_K_II,
                RingPosition = configuration.Rotor2RingPosition,
                StartingRotation = configuration.Rotor2StartingRotation
            }));

            Rotors.Add(2, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Swiss_K_III,
                RingPosition = configuration.Rotor3RingPosition,
                StartingRotation = configuration.Rotor3StartingRotation
            }));

            AddReflector(new ReflectorConfiguration
            {
                ReflectorPreset = ReflectorPresets.Swiss_K_UKW
            });
        }
        else if (configuration.MachinePreset == MachinePresets.Kriegsmarine_M3_1939)
        {
            AddEntryWheel(new EntryWheelConfiguration
            {
                EntryWheelPreset = EntryWheelPresets.Kriegsmarine
            });

            Rotors.Add(0, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Wehrmacht_I,
                RingPosition = configuration.Rotor1RingPosition,
                StartingRotation = configuration.Rotor1StartingRotation
            }));

            Rotors.Add(1, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Kriegsmarine_M3_M4_VI,
                RingPosition = configuration.Rotor2RingPosition,
                StartingRotation = configuration.Rotor2StartingRotation
            }));

            Rotors.Add(2, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Kriegsmarine_M3_M4_VII,
                RingPosition = configuration.Rotor3RingPosition,
                StartingRotation = configuration.Rotor3StartingRotation
            }));

            Rotors.Add(3, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Kriegsmarine_M3_M4_VIII,
                RingPosition = configuration.Rotor4RingPosition,
                StartingRotation = configuration.Rotor4StartingRotation
            }));

            AddReflector(new ReflectorConfiguration
            {
                ReflectorPreset = ReflectorPresets.Kriegsmarine_M4_B_Thin
            });
        }
        else if (configuration.MachinePreset == MachinePresets.German_Railway_Rocket_1941)
        {
            AddEntryWheel(new EntryWheelConfiguration
            {
                EntryWheelPreset = EntryWheelPresets.Railway_Rocket_ETW
            });

            Rotors.Add(0, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Railway_Rocket_I,
                RingPosition = configuration.Rotor1RingPosition,
                StartingRotation = configuration.Rotor1StartingRotation
            }));

            Rotors.Add(1, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Railway_Rocket_II,
                RingPosition = configuration.Rotor2RingPosition,
                StartingRotation = configuration.Rotor2StartingRotation
            }));

            Rotors.Add(2, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Railway_Rocket_III,
                RingPosition = configuration.Rotor3RingPosition,
                StartingRotation = configuration.Rotor3StartingRotation
            }));

            AddReflector(new ReflectorConfiguration
            {
                ReflectorPreset = ReflectorPresets.Railway_Rocket_UKW
            });
        }
        else if (configuration.MachinePreset == MachinePresets.Kriegsmarine_M4_1941)
        {
            AddEntryWheel(new EntryWheelConfiguration
            {
                EntryWheelPreset = EntryWheelPresets.Kriegsmarine
            });

            Rotors.Add(0, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Wehrmacht_III,
                RingPosition = configuration.Rotor1RingPosition,
                StartingRotation = configuration.Rotor1StartingRotation
            }));

            Rotors.Add(1, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Kriegsmarine_M3_M4_VI,
                RingPosition = configuration.Rotor2RingPosition,
                StartingRotation = configuration.Rotor2StartingRotation
            }));

            Rotors.Add(2, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Kriegsmarine_M3_M4_VII,
                RingPosition = configuration.Rotor3RingPosition,
                StartingRotation = configuration.Rotor3StartingRotation
            }));

            Rotors.Add(3, new Rotor(new RotorConfiguration
            {
                RotorPreset = RotorPresets.Kriegsmarine_M3_M4_VIII,
                RingPosition = configuration.Rotor4RingPosition,
                StartingRotation = configuration.Rotor4StartingRotation
            }));

            AddReflector(new ReflectorConfiguration
            {
                ReflectorPreset = ReflectorPresets.Kriegsmarine_M4_C_Thin
            });
        }
        else if (configuration.MachinePreset == MachinePresets.Modern_Ascii)
        {
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
        }
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

        AddEntryWheel(new EntryWheelConfiguration
        {
            CharacterSets = charSet,
            Acrn = Acrn
        });

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
        {
            t.Value.ResetRotation();
        }
    }
    
    /// <summary>
    /// Encipher a provided string.
    /// Call Reset() between each successive call.
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
                
                foreach (var t in Rotors.OrderBy(r => r.Key))
                {
                    cc = t.Value.SendCharacter(cc);
                }

                cc = Reflector.SendCharacter(cc);

                foreach (var t in Rotors.OrderByDescending(r => r.Key))
                {
                    cc = t.Value.ReflectedCharacter(cc);
                }

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
