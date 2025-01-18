using System;
using Xunit;

namespace Enigma.Tests;

public class ReflectorTests
{
	[Fact]
	public void GenerateReflectorsClasses()
	{
        var sb = string.Empty;

        foreach (var cs in Constants.DefaultReflectors)
        {
            var characters = Constants.CharacterSet.Replace("ABCDEFGHIJKLMNOPQRSTUVWXYZ", string.Empty);

            sb += 
                $$"""
                    public static Dictionary<char,char> Reflector{{Array.IndexOf(Constants.DefaultReflectors, cs) + 1}} = new()
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
    public void ReflectorOutput()
    {
        var reflector = new Reflector
        {
            Wheel = Constants.Reflector1
        };
        
        reflector.Reset();
        
        Assert.Equal('=', reflector.EncipherCharacter(' '));
        Assert.Equal('Y', reflector.EncipherCharacter('A'));
        Assert.Equal('R', reflector.EncipherCharacter('B'));
        Assert.Equal('w', reflector.EncipherCharacter('~'));
        
        Assert.Equal(' ', reflector.DecipherCharacter('='));
        Assert.Equal('A', reflector.DecipherCharacter('Y'));
        Assert.Equal('B', reflector.DecipherCharacter('R'));
        Assert.Equal('~', reflector.DecipherCharacter('w'));
    }
}
