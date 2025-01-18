using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static Enigma.Reflectors;

namespace Enigma.Tests;

public class ReflectorTests
{
    [Fact]
    public void AsciiReflector()
    {
        var reflector = new Reflector()
            .SetWires(GetReflector(ReflectorType.Ascii));
        
        Assert.Equal(reflector.Wires[' '], reflector.SendCharacter(' '));
        Assert.Equal(reflector.Wires['A'], reflector.SendCharacter('A'));
        Assert.Equal(reflector.Wires['B'], reflector.SendCharacter('B'));
        Assert.Equal(reflector.Wires['~'], reflector.SendCharacter('~'));
        
        Assert.Equal(' ', reflector.SendCharacter('B'));
        Assert.Equal('A', reflector.SendCharacter('J'));
        Assert.Equal('B', reflector.SendCharacter(' '));
        Assert.Equal('~', reflector.SendCharacter('N'));
    }

    [Fact]
    public void GeneratedReflector()
    {
        var reflector = new Reflector()
            .SetWires(GenerateReflector(CharacterSet.Ascii));
        
        Assert.Equal(reflector.Wires[' '], reflector.SendCharacter(' '));
        Assert.Equal(reflector.Wires['A'], reflector.SendCharacter('A'));
        Assert.Equal(reflector.Wires['B'], reflector.SendCharacter('B'));
        Assert.Equal(reflector.Wires['~'], reflector.SendCharacter('~'));
        
        Assert.Equal(' ', reflector.SendCharacter(reflector.Wires.First(w => w.Key == ' ').Value));
        Assert.Equal('A', reflector.SendCharacter(reflector.Wires.First(w => w.Key == 'A').Value));
        Assert.Equal('B', reflector.SendCharacter(reflector.Wires.First(w => w.Key == 'B').Value));
        Assert.Equal('~', reflector.SendCharacter(reflector.Wires.First(w => w.Key == '~').Value));
    }

}
