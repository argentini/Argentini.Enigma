using System;
using Xunit;

namespace Enigma.Tests;

public class RotorTests
{
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
        var rotor = new Rotor
        {
            Wheel = Constants.Rotor1
        };

        rotor.Reset();
        
        Assert.Equal('*', rotor.SendCharacter(' '));
        Assert.Equal('E', rotor.SendCharacter('A'));
        Assert.Equal('K', rotor.SendCharacter('B'));
        Assert.Equal('%', rotor.SendCharacter('~'));

        Assert.Equal(' ', rotor.ReflectedCharacter('*'));
        Assert.Equal('A', rotor.ReflectedCharacter('E'));
        Assert.Equal('B', rotor.ReflectedCharacter('K'));
        Assert.Equal('~', rotor.ReflectedCharacter('%'));

        rotor.Rotation = 1;

        Assert.Equal('d', rotor.SendCharacter(' '));
        Assert.Equal('K', rotor.SendCharacter('A'));
        Assert.Equal('M', rotor.SendCharacter('B'));
        Assert.Equal('*', rotor.SendCharacter('~'));

        Assert.Equal(' ', rotor.ReflectedCharacter('d'));
        Assert.Equal('A', rotor.ReflectedCharacter('K'));
        Assert.Equal('B', rotor.ReflectedCharacter('M'));
        Assert.Equal('~', rotor.ReflectedCharacter('*'));

        rotor.Rotation = 0;
        rotor.NotchPosition = 1;

        Assert.Equal('d', rotor.SendCharacter(' '));
        Assert.Equal('K', rotor.SendCharacter('A'));
        Assert.Equal('M', rotor.SendCharacter('B'));
        Assert.Equal('*', rotor.SendCharacter('~'));

        Assert.Equal(' ', rotor.ReflectedCharacter('d'));
        Assert.Equal('A', rotor.ReflectedCharacter('K'));
        Assert.Equal('B', rotor.ReflectedCharacter('M'));
        Assert.Equal('~', rotor.ReflectedCharacter('*'));
	}
}
