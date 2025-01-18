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
            var characters = Constants.CharacterSet;

            sb += 
                $$"""
                    public static Dictionary<char,char> Reflector{{Array.IndexOf(Constants.DefaultReflectors, cs) + 1}} = new()
                    {

                """;

            while (characters.Length > 1)
            {
                var c = characters[0];
                
                if (c is >= 'A' and <= 'Z')
                {
                    sb += 
                        $$"""
                                {'{{c}}', '{{cs[c - 65]}}'},

                        """;

                    characters = characters.Replace($"{c}", string.Empty);
                }
                else
                {
                    var ii = Random.Shared.Next(0, characters.Length);
                    var cc = characters[ii];

                    while (cc is >= 'A' and <= 'Z' || cc == c)
                    {
                        ii = Random.Shared.Next(0, characters.Length);
                        cc = characters[ii];
                    }

                    characters = characters.Replace($"{c}", string.Empty);
                    characters = characters.Replace($"{cc}", string.Empty);

                    sb += 
                        $$"""
                                {'{{(c == '\'' ? "\\'" : c == '\\' ? @"\\" : c)}}', '{{(cc == '\'' ? "\\'" : cc == '\\' ? @"\\" : cc)}}'},
                                {'{{(cc == '\'' ? "\\'" : cc == '\\' ? @"\\" : cc)}}', '{{(c == '\'' ? "\\'" : c == '\\' ? @"\\" : c)}}'},
                        
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
            Wires = Constants.Reflector1
        };
        
        Assert.Equal('s', reflector.SendCharacter(' '));
        Assert.Equal('Y', reflector.SendCharacter('A'));
        Assert.Equal('R', reflector.SendCharacter('B'));
        Assert.Equal('!', reflector.SendCharacter('~'));
        
        Assert.Equal(' ', reflector.SendCharacter('s'));
        Assert.Equal('A', reflector.SendCharacter('Y'));
        Assert.Equal('B', reflector.SendCharacter('R'));
        Assert.Equal('~', reflector.SendCharacter('!'));
    }
}
