using Xunit;

namespace Enigma.Tests;

public class AesCtrRandomNumberGeneratorTests
{
	[Fact]
	public void AesCtrRandomNumberGenerator()
	{
        const string key = "ThisIsA32ByteLongSecretKey123456"; // 32 bytes for AES-256
        const string nonce = "UniqueNonce12345"; // 16 bytes

        using var aesCtrRng = new AesCtrRandomNumberGenerator(key, nonce);
        
        Assert.Equal(3701761418U, aesCtrRng.NextUInt32());
        Assert.Equal(1531030081U, aesCtrRng.NextUInt32());
        Assert.Equal(1303783701U, aesCtrRng.NextUInt32());
        Assert.Equal(1483644058U, aesCtrRng.NextUInt32());
        Assert.Equal(1091670316U, aesCtrRng.NextUInt32());

        Assert.Equal(0.48177185394521438D, aesCtrRng.NextDouble());
        Assert.Equal(0.82371742486395771D, aesCtrRng.NextDouble());
        Assert.Equal(0.40951574677230473D, aesCtrRng.NextDouble());
        Assert.Equal(0.29289020891755541D, aesCtrRng.NextDouble());
        Assert.Equal(0.50124318161077952D, aesCtrRng.NextDouble());
	}
}
