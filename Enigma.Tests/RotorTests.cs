using System;
using System.Linq;
using Xunit;

namespace Enigma.Tests;

public class RotorTests
{
    [Fact]
    public void RandomizeCharacterAsciiSet()
    {
        const string characterSet = @" !""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~";

        var sb = string.Empty;
        var characters = characterSet;

        foreach (var _ in characters)
        {
            var ci = characters.Length > 0 ? Random.Shared.Next(0, characters.Length) : -1;

            if (ci < 0)
                break;
            
            var cc = characters[ci];

            characters = characters.Replace($"{cc}", string.Empty);

            sb += cc;
        }

        Assert.Equal(characterSet.Length, sb.Length);
    }

	[Fact]
	public void GenerateRotorsClasses()
	{
        var sb = string.Empty;

        foreach (var cs in Constants.DefaultRotors)
        {
            var characters = Constants.CharacterSet.Replace("ABCDEFGHIJKLMNOPQRSTUVWXYZ", string.Empty);

            sb += 
                $$"""
                    public static Dictionary<char,char> Rotor{{Array.IndexOf(Constants.DefaultRotors, cs) + 1}} = new()
                    {

                """;

            for (var i = 32; i < 127; i++)
            {
                if (i is >= 'A' and <= 'Z')
                {
                    sb += 
                        $$"""
                                {'{{(char)i}}', '{{cs[i - 65]}}'},

                        """;
                }
                else
                {
                    var ci = characters.Length > 0 ? Random.Shared.Next(0, characters.Length) : 0;
                    var c = characters[ci];

                    characters = characters.Replace($"{c}", string.Empty);

                    sb += 
                        $$"""
                                {'{{(i == '\'' ? "\\'" : i == '\\' ? "\\\\" : (char)i)}}', '{{(c == '\'' ? "\\'" : c == '\\' ? "\\\\" : c)}}'},

                        """;
                }
            }

            sb +=
                """
                    };

                """;
        }

        Assert.NotEmpty(sb);
    }

	[Fact]
	public void RotorOutput()
    {
        var rotorWheel = Rotors.GetRotor(Rotors.RotorType.Ascii_I);
        var rotor = new Rotor()
            .SetWheel(rotorWheel);
        
        Assert.Equal(rotorWheel[' '], rotor.SendCharacter(' '));
        Assert.Equal(rotorWheel['A'], rotor.SendCharacter('A'));
        Assert.Equal(rotorWheel['B'], rotor.SendCharacter('B'));
        Assert.Equal(rotorWheel['~'], rotor.SendCharacter('~'));

        Assert.Equal(rotorWheel.First(r => r.Value == '*').Key, rotor.ReflectedCharacter('*'));
        Assert.Equal(rotorWheel.First(r => r.Value == 'E').Key, rotor.ReflectedCharacter('E'));
        Assert.Equal(rotorWheel.First(r => r.Value == 'K').Key, rotor.ReflectedCharacter('K'));
        Assert.Equal(rotorWheel.First(r => r.Value == '%').Key, rotor.ReflectedCharacter('%'));

        rotor.SetRotation(1);

        Assert.Equal('r', rotor.SendCharacter(' '));
        Assert.Equal('z', rotor.SendCharacter('A'));
        Assert.Equal('~', rotor.SendCharacter('B'));
        Assert.Equal('n', rotor.SendCharacter('~'));

        Assert.Equal('|', rotor.ReflectedCharacter('d'));
        Assert.Equal('1', rotor.ReflectedCharacter('K'));
        Assert.Equal(',', rotor.ReflectedCharacter('M'));
        Assert.Equal('a', rotor.ReflectedCharacter('*'));

        rotor.SetRotation(0);
        rotor.SetNotchPosition(1);

        Assert.Equal('r', rotor.SendCharacter(' '));
        Assert.Equal('z', rotor.SendCharacter('A'));
        Assert.Equal('~', rotor.SendCharacter('B'));
        Assert.Equal('n', rotor.SendCharacter('~'));

        Assert.Equal('|', rotor.ReflectedCharacter('d'));
        Assert.Equal('1', rotor.ReflectedCharacter('K'));
        Assert.Equal(',', rotor.ReflectedCharacter('M'));
        Assert.Equal('a', rotor.ReflectedCharacter('*'));
	}
}
