using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static Enigma.Reflectors;

namespace Enigma.Tests;

public class ReflectorTests
{
    [Fact]
    public void GenerateReflector()
    {
        Assert.Equal(26, Reflectors.GenerateReflector(CharacterSet.Classic).Length);
        Assert.Equal(95, Reflectors.GenerateReflector(CharacterSet.Ascii).Length);
    }
    
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
        var reflector = new Reflector()
            .SetWires(GetReflector(ReflectorType.Ascii));
        
        Assert.Equal('B', reflector.SendCharacter(' '));
        Assert.Equal('J', reflector.SendCharacter('A'));
        Assert.Equal(' ', reflector.SendCharacter('B'));
        Assert.Equal('N', reflector.SendCharacter('~'));
        
        Assert.Equal(' ', reflector.SendCharacter('B'));
        Assert.Equal('A', reflector.SendCharacter('J'));
        Assert.Equal('B', reflector.SendCharacter(' '));
        Assert.Equal('~', reflector.SendCharacter('N'));
    }
}
