using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
                if (i >= 'A' && i <= 'Z')
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

        Assert.True(true);
    }

	[Fact]
	public void RotorOutput()
	{
        var rotor = new Rotor
        {
            Wheel = Constants.Rotor1
        };

        rotor.Reset();
        
        Assert.Equal('*', rotor.EncipherCharacter(' '));
        Assert.Equal('E', rotor.EncipherCharacter('A'));
        Assert.Equal('K', rotor.EncipherCharacter('B'));
        Assert.Equal('%', rotor.EncipherCharacter('~'));

        Assert.Equal(' ', rotor.DecipherCharacter('*'));
        Assert.Equal('A', rotor.DecipherCharacter('E'));
        Assert.Equal('B', rotor.DecipherCharacter('K'));
        Assert.Equal('~', rotor.DecipherCharacter('%'));

        rotor.Rotation = 1;

        Assert.Equal('d', rotor.EncipherCharacter(' '));
        Assert.Equal('K', rotor.EncipherCharacter('A'));
        Assert.Equal('M', rotor.EncipherCharacter('B'));
        Assert.Equal('*', rotor.EncipherCharacter('~'));

        Assert.Equal(' ', rotor.DecipherCharacter('d'));
        Assert.Equal('A', rotor.DecipherCharacter('K'));
        Assert.Equal('B', rotor.DecipherCharacter('M'));
        Assert.Equal('~', rotor.DecipherCharacter('*'));

        rotor.Rotation = 0;
        rotor.NotchPosition = 1;

        Assert.Equal('d', rotor.EncipherCharacter(' '));
        Assert.Equal('K', rotor.EncipherCharacter('A'));
        Assert.Equal('M', rotor.EncipherCharacter('B'));
        Assert.Equal('*', rotor.EncipherCharacter('~'));

        Assert.Equal(' ', rotor.DecipherCharacter('d'));
        Assert.Equal('A', rotor.DecipherCharacter('K'));
        Assert.Equal('B', rotor.DecipherCharacter('M'));
        Assert.Equal('~', rotor.DecipherCharacter('*'));
	}
}
