using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Enigma.Tests;

public class PlugBoardTests
{
	[Fact]
	public void PlugBoardInputOutput()
	{
        var pb = new PlugBoard
        {
            Wires = new Dictionary<char, char>
            {
                { 'A', 'B' },
                { 'C', 'D' }
            }
        };

        pb.Reset();
        
        Assert.Equal('B', pb.EncipherCharacter('A'));
        Assert.Equal('D', pb.EncipherCharacter('C'));

        Assert.Equal('A', pb.DecipherCharacter('B'));
        Assert.Equal('C', pb.DecipherCharacter('D'));

        pb = new PlugBoard
        {
            Wires = new Dictionary<char, char>
            {
                { 'A', 'B' },
                { 'C', 'B' }
            }
        };

        Assert.ThrowsAny<Exception>(() => pb.Reset());

        Assert.ThrowsAny<Exception>(() => pb = new PlugBoard
        {
            Wires = new Dictionary<char, char>
            {
                { 'A', 'B' },
                { 'A', 'D' }
            }
        });
	}
}
