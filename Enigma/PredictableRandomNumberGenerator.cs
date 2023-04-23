namespace Enigma;

/// <summary>
/// Generate random numbers using a linear congruential generator.
/// Generates a predictable sequence when given the same seed.
/// </summary>
public class PredictableRandomNumberGenerator
{
	private const long M = 4294967296; // 2^32
	private const long A = 1664525;
	private const long C = 1013904223;
	private long _last;

	/// <summary>
	/// Instantiate a PRNG using the current ticks as a seed for reasonably unpredictable values.
	/// </summary>
	public PredictableRandomNumberGenerator()
	{
		_last = DateTime.Now.Ticks % M;
	}

	/// <summary>
	/// Instantiate a PRNG using a provided seed for predictable values.
	/// </summary>
	public PredictableRandomNumberGenerator(long seed)
	{
		_last = seed;
	}

	/// <summary>
	/// Gets the next value
	/// </summary>
	/// <returns></returns>
	public long Next()
	{
		_last = ((A * _last) + C) % M;

		return _last;
	}

	/// <summary>
	/// Get the next value from zero to maxValue, inclusive.
	/// </summary>
	/// <param name="maxValue"></param>
	/// <returns></returns>
	public long Next(long maxValue)
	{
		return NextBetween(0, maxValue);
	}
	
	/// <summary>
	/// Get the next value from minValue to maxValue, inclusive.
	/// </summary>
	/// <param name="minValue"></param>
	/// <param name="maxValue"></param>
	/// <returns></returns>
	public long NextBetween(long minValue, long maxValue)
	{
		if (maxValue > minValue)
		{
			var newMaxValue = maxValue - minValue;

			return (Next() % (newMaxValue + 1)) + minValue;
		}

		throw new Exception("Halide.Secrets.PredictableRandomNumberGenerator().NextBetween() => maxValue must be greater than minValue");
	}
}
