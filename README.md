# What is the Enigma Machine?

The Enigma machine is a cipher device developed and used in the early- to mid-20th century to protect commercial, diplomatic, and military communication. It was employed extensively by Nazi Germany during World War II, in all branches of the German military. The Enigma machine was considered so secure that it was used to encipher the most top-secret messages.  
  
Learn more about it at [https://en.wikipedia.org/wiki/Enigma_machine](https://en.wikipedia.org/wiki/Enigma_machine).  

# The Project

This project is a virtual *Enigma Machine* that supports the entire UTF-16 character set, whereas the original German hardware only supported 26 uppercase letters (no spaces!).  
  
Just like the physical device, machine state is used to both encipher and decipher text with the same functions (like a text toggle). This virtual version works with UTF-16 strings using a keyless algorythm based on the inner workings of the original hardware. To accomplish this, it uses a custom predictable random number generator (PRNG) to ensure the cipher will work on any supported platform.  
  
Unlike the original hardware, you can add as many virtual rotors as you like, set the cipher seeds for all machine components (effectively randomizing the character sets they use), set starting rotor positions, and more, which will improve the overall cipher strength. Because the entire UTF-16 character set is used, the cipher strength is already exponentially better even without additional rotors, and should meet modern keyless cryptography needs.  
  
It's easy to create a new EnigmaMachine object and encipher/decipher your own text:  
  
```C#
var enigmaMachine = new EnigmaMachine(new EnigmaConfiguration
{
    PlugBoardCipherSeed = 1234567,
    ReflectorCipherSeed = 6789101,
    Rotors = new List<EnigmaRotor> // Rotors work top to bottom; original hardware has them right to left
    {
        new (rotorCipherSeed: 9876543210, rotorStartingPosition: 0, advanceNextRotorIncrement: 50),
        new (rotorCipherSeed: 1234567890, rotorStartingPosition: 0, advanceNextRotorIncrement: 25),
        new (rotorCipherSeed: 1029384756, rotorStartingPosition: 0) // No neighbor for the last rotor to advance
    }
});

var message = @"
Fynydd is a software development & hosting company.
Fynydd is a Welsh word (prounounced: /ˈvənɨ̞ð/) that means mountain or hill.
";

var enciphered = enigma.RunCipher(message);
var deciphered = enigma.RunCipher(enciphered);

Assert.Equal(message, deciphered);
```

This project is a .NET library with xUnit tests, so you can easily play with the virtual Enigma Machine.
