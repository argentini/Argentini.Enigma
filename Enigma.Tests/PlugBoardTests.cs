using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Enigma.Tests;

public class PlugBoardTests
{
	[Fact]
	public async Task PlugBoardInputOutput()
	{
        var pb = new PlugBoard
        {
            Wires = new Dictionary<char, char>
            {
                { 'A', 'B' },
                { 'C', 'D' }
            }
        };

        await pb.ResetAsync();
        
        Assert.Equal('B', pb.CharacterIn('A'));
        Assert.Equal('D', pb.CharacterIn('C'));

        Assert.Equal('A', pb.CharacterOut('B'));
        Assert.Equal('C', pb.CharacterOut('D'));

        pb = new PlugBoard
        {
            Wires = new Dictionary<char, char>
            {
                { 'A', 'B' },
                { 'C', 'B' }
            }
        };

        await Assert.ThrowsAnyAsync<Exception>(async () => await pb.ResetAsync());

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
