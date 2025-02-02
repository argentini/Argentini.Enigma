# What Is The Enigma Machine?

The Enigma machine is a physical cipher device developed and used in the early- to mid-20th century to protect commercial, diplomatic, and military communication. It was employed extensively by Nazi Germany during World War II, in all branches of the German military. The Enigma machine was considered so secure that it was used to encipher the most top-secret messages.  
  
Learn more about it at [https://en.wikipedia.org/wiki/Enigma_machine](https://en.wikipedia.org/wiki/Enigma_machine).  

# The Project

This project is a high performance *Enigma Machine* emulator that allows you to:  

- Explore historical configurations using the classic 26 letter alphabet (no spaces!)  
- Use for modern quantum-resistant cryptography with the full 95-character ASCII character set.  
  
Just like the physical device, machine state is used to both encipher and decipher text with the same `Encipher()` function (like a text toggle). Machine state had to match on both the sender and receiver machines. Each operator would add specific rotors in a specific order, set rotor notch positions and starting rotations, as well as set plug wire positions. This emulator uses virtual versions of all key machine components by way of a deterministic random number generator using AES in counter (CTR) mode.  

The emulated components include:  

- Plug board
- Entry wheel
- Rotors
- Reflector

Additionally, characters in the source string that do not exist in the cipher character set are kept as-is in the enciphered text. For example, if you encipher a string with line breaks they are maintained in-place in the enciphered text since neither the classic 26 letter character set nor the 95 character ASCII set contain line break characters.

Note that by simply using the full 95-character ASCII character set the cipher strength will be exponentially better than the original machine even without additional rotors or other configuration, and should meet modern quantum-resistant cryptography needs.  

## Performance

**The emulator is FAST!** When using the full 95-character ASCII character set, a large 800KB text string takes about 1 second to encipher. Typical text sizes encipher in a few milliseconds.

## Unit Tests

The included units tests show basic usage across various configurations and verify the code as working properly. They're also a great way to qucikly see the *Enigma Machine* in action!

## Examples

### Example 1: Historical Presets

It's easy to create a new virtual Enigma Machine and encipher your own text by using one of the provided presets based on historical machine configurations:  

- Commercial Enigma (1924)
- Wehrmacht and Kriegsmarine (1930)
- Wehrmacht and Kriegsmarine (1938)
- Swiss K (1939)
- Kriegsmarine M3 and M4 (1939)
- German Railway (Rocket; 1941)
- Kriegsmarine M4 with thin reflectors (1941):

Using one of the presets is easy:  
  
```C#
var message = "FYNYDD IS A SOFTWARE DEVELOPMENT AND HOSTING COMPANY";

var machine = new Machine(new MachineConfiguration
{
    MachinePreset = MachinePresets.Commercial_1924,
    Rotor1RingPosition = 15,
});

var enciphered = machine.Encipher(message.ToString());

Assert.NotEqual(message.ToString(), enciphered);

machine.Reset();

var deciphered = machine.Encipher(enciphered);

Assert.Equal(message.ToString(), deciphered);
```

To more completely customize the preset-based machine, as was done by actual users of the device, you can include additional configuration options:

```C#
var message = "FYNYDD IS A SOFTWARE DEVELOPMENT AND HOSTING COMPANY";

var machine = new Machine(new MachineConfiguration
{
    MachinePreset = MachinePresets.Kriegsmarine_M4_1941,
    Rotor1RingPosition = 5,
    Rotor2RingPosition = 15,
    Rotor3RingPosition = 22,
    Rotor4RingPosition = 3,
    Rotor1StartingRotation = 10,
    Rotor2StartingRotation = 6,
    Rotor3StartingRotation = 17,
    Rotor4StartingRotation = 23,
    PlugBoardWires =
    {
        { 'A', 'T' },
        { 'B', 'V' },
        { 'C', 'M' },
        { 'D', 'O' },
        { 'E', 'Y' },
        { 'F', 'Q' },
        { 'G', 'R' },
        { 'H', 'S' },
        { 'I', 'L' },
        { 'J', 'K' },
    }
});

var enciphered = machine.Encipher(message.ToString());

Assert.NotEqual(message.ToString(), enciphered);

machine.Reset();

var deciphered = machine.Encipher(enciphered);

Assert.Equal(message.ToString(), deciphered);
```

Rotor ring position values are 1 through the number of characters in the character set. In the previous example we're using the original 26 letter character set, which would be a ring position range of 1-26. Rotor starting rotation values are the same as ring position values.  

Plug board wires are a simple map of two letters, like the original machine used by Germany in World War II, which provides another layer of cipher strength.

### Example 2: Fully Custom Machine

You can also manually assemble an *Enigma Machine* from the various components.

```C#
var message = @"
Fynydd is a software development & hosting company.
Fynydd is a Welsh word that means mountain or hill.
";

var machine = new Machine()
    .AddPlugBoard(new Dictionary<char, char>
    {
        { 'A', 'T' },
        { 'B', 'V' },
        { 'C', 'M' },
        { 'D', 'O' },
        { 'E', 'Y' },
        { 'F', 'Q' },
        { 'G', 'R' },
        { 'H', 'S' },
        { 'I', 'L' },
        { 'J', 'K' },
    })
    .AddEntryWheel(new EntryWheelConfiguration
    {
        EntryWheelPreset = EntryWheelPresets.Ascii,
    })
    .AddRotor(new RotorConfiguration
    {
        RotorPreset = RotorPresets.Ascii_I,
        StartingRotation = 15
    })
    .AddRotor(new RotorConfiguration
    {
        RotorPreset = RotorPresets.Ascii_II,
        RingPosition = 10,
        StartingRotation = 5
    })
    .AddRotor(new RotorConfiguration
    {
        RotorPreset = RotorPresets.Ascii_III,
        RingPosition = 65,
        StartingRotation = 45
    })
    .AddReflector(new ReflectorConfiguration
    {
        ReflectorPreset = ReflectorPresets.Ascii
    });

var enciphered = machine.Encipher(message.ToString());

Assert.NotEqual(message.ToString(), enciphered);

machine.Reset();

var deciphered = machine.Encipher(enciphered);

Assert.Equal(message.ToString(), deciphered);
```

### Example 3: Practical Usage

To use the *Enigma Machine* for modern encryption is even easier than using a historical preset, since all you need to provide are a cipher key, nonce, and the number of relevant machine components. There's no need to change rotor ring positions and rotations or set plug board wire pair values since your cipher key and nonce are unique and drive the creation of all machine components.  

Here's an example of using the *Enigma Machine* without a historical preset:  

```C#
/*
    AES key must be 16, 24, or 32 bytes for AES-128, AES-192, or AES-256
    Nonce or initial counter value must be 16 bytes

    This constructor creates a custom machine using the full ASCII character set,
    3 Rotors, and 10 Plug Board wire pairs.
*/

var message = @"
Fynydd is a software development & hosting company.
Fynydd is a Welsh word that means mountain or hill.
";

var machine = new Machine(
    "ThisIsA32ByteLongSecretKey123456",
    "UniqueNonce12345");

var enciphered = machine.Encipher(message.ToString());

Assert.NotEqual(message.ToString(), enciphered);

machine.Reset();

var deciphered = machine.Encipher(enciphered);

Assert.Equal(message.ToString(), deciphered);
```

You can also specify the number of various components to increase cipher strength:

```C#
/*
    AES key must be 16, 24, or 32 bytes for AES-128, AES-192, or AES-256
    Nonce or initial counter value must be 16 bytes
*/

var message = @"
Fynydd is a software development & hosting company.
Fynydd is a Welsh word that means mountain or hill.
";

var machine = new Machine(
    "ThisIsA32ByteLongSecretKey123456",
    "UniqueNonce12345",
    rotorCount: 5,
    plugWires: 13);

var enciphered = machine.Encipher(message.ToString());

Assert.NotEqual(message.ToString(), enciphered);

machine.Reset();

var deciphered = machine.Encipher(enciphered);

Assert.Equal(message.ToString(), deciphered);
```

This project is a .NET library with unit tests, so you can easily play with the virtual Enigma Machine.