using System.Collections.Generic;
using Xunit;

namespace Enigma.Tests;

public class PlugBoardTests
{
	[Fact]
	public void PlugBoardInputOutput()
	{
        var pb = new PlugBoard()
            .SetWires(new Dictionary<char, char>
            {
                { 'A', 'B' },
                { 'C', 'D' }
            });

        Assert.Equal('B', pb.SendCharacter('A'));
        Assert.Equal('A', pb.SendCharacter('B'));

        Assert.Equal('D', pb.SendCharacter('C'));
        Assert.Equal('C', pb.SendCharacter('D'));
	}
}
