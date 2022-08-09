using Xunit;

namespace Enigma.Tests;

public class PredictableRandomNumberGeneratorTests
{
	[Fact]
	public void PredictableRandomNumberGenerator()
	{
		var prng = new PredictableRandomNumberGenerator(42);
		var counter = 0;

		while (prng.NextBetween(50, 100) != 50)
		{
			counter++;
		}

		Assert.Equal(29, counter);

		while (prng.NextBetween(50, 100) != 100)
		{
			counter++;
		}

		Assert.Equal(39, counter);

		var rnd = prng.Next();

		Assert.Equal(1931673780, rnd);
	}
}
