using System.Security.Cryptography;
using System.Text;

// ReSharper disable MemberCanBePrivate.Global

namespace Enigma;

/// <summary>
/// Deterministic Random Number Generator using AES in Counter (CTR) mode.
/// Provides cryptographic security and high performance.
/// </summary>
public sealed class AesCtrRandomNumberGenerator : IDisposable
{
    private readonly Aes? _aes;
    private readonly ICryptoTransform? _encryptor;
    private readonly byte[] _counter;
    private readonly byte[] _buffer;
    private int _bufferIndex;
    
    /// <summary>
    /// Initializes the generator with a specified key and nonce.
    /// </summary>
    /// <param name="key">AES key must be 16, 24, or 32 bytes for AES-128, AES-192, or AES-256.</param>
    /// <param name="nonce">Nonce or initial counter value must be 16 bytes.</param>
    public AesCtrRandomNumberGenerator(string key, string nonce)
    {
        if (key is not { Length: 16 or 24 or 32 })
            throw new ArgumentException("AesCtrRandomNumberGenerator => Key must be 16, 24, or 32 bytes long.", nameof(key));

        if (nonce is not { Length: 16 })
            throw new ArgumentException("AesCtrRandomNumberGenerator => Nonce must be 16 bytes long.", nameof(nonce));

        _aes = Aes.Create();
        _aes.Mode = CipherMode.ECB; // Use ECB mode for CTR
        _aes.Padding = PaddingMode.None;
        _encryptor = _aes.CreateEncryptor(Encoding.UTF8.GetBytes(key), null);
        _counter = new byte[16];

        Buffer.BlockCopy(Encoding.UTF8.GetBytes(nonce), 0, _counter, 0, 16);

        _buffer = new byte[16];
        _bufferIndex = 16; // Indicates that buffer is empty
    }

    /// <summary>
    /// Initializes the generator with a specified key and nonce.
    /// </summary>
    /// <param name="key">AES key (must be 16, 24, or 32 bytes for AES-128, AES-192, or AES-256).</param>
    /// <param name="nonce">Nonce or initial counter value (must be 16 bytes).</param>
    public AesCtrRandomNumberGenerator(byte[] key, byte[] nonce)
    {
        if (key is not { Length: 16 or 24 or 32 })
            throw new ArgumentException("AesCtrRandomNumberGenerator => Key must be 16, 24, or 32 bytes long.", nameof(key));

        if (nonce is not { Length: 16 })
            throw new ArgumentException("AesCtrRandomNumberGenerator => Nonce must be 16 bytes long.", nameof(nonce));

        _aes = Aes.Create();
        _aes.Mode = CipherMode.ECB; // Use ECB mode for CTR
        _aes.Padding = PaddingMode.None;
        _encryptor = _aes.CreateEncryptor(key, null);
        _counter = new byte[16];

        Buffer.BlockCopy(nonce, 0, _counter, 0, 16);

        _buffer = new byte[16];
        _bufferIndex = 16; // Indicates that buffer is empty
    }

    /// <summary>
    /// Generates the next byte of random data.
    /// </summary>
    /// <returns>A single random byte.</returns>
    public byte NextByte()
    {
        if (_bufferIndex < 16)
            return _buffer[_bufferIndex++];
        
        _encryptor?.TransformBlock(_counter, 0, 16, _buffer, 0);

        IncrementCounter();
        
        _bufferIndex = 0;

        return _buffer[_bufferIndex++];
    }

    /// <summary>
    /// Generates the next 32-bit unsigned integer.
    /// </summary>
    /// <returns>A random UInt32.</returns>
    public uint NextUInt32()
    {
        var bytes = new byte[4];
        
        for (var i = 0; i < 4; i++)
            bytes[i] = NextByte();
        
        return BitConverter.ToUInt32(bytes, 0);
    }

    /// <summary>
    /// Generates the next 64-bit unsigned integer.
    /// </summary>
    /// <returns>A random UInt64.</returns>
    public ulong NextUInt64()
    {
        var bytes = new byte[8];
        
        for (var i = 0; i < 8; i++)
            bytes[i] = NextByte();
        
        return BitConverter.ToUInt64(bytes, 0);
    }

    /// <summary>
    /// Generates a random double between 0.0 (inclusive) and 1.0 (exclusive).
    /// </summary>
    /// <returns>A random double.</returns>
    public double NextDouble()
    {
        var ulongValue = NextUInt64();
        
        return ulongValue / (double)ulong.MaxValue;
    }

    /// <summary>
    /// Generates a random integer between 0 (inclusive) and maxValue (exclusive).
    /// </summary>
    /// <param name="maxValue">Exclusive upper bound.</param>
    /// <returns>A random integer.</returns>
    public int NextInt32(int maxValue)
    {
        if (maxValue <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxValue), "AesCtrRandomNumberGenerator.NextInt32() => maxValue must be positive.");

        var val = NextUInt32();
        
        return (int)(val % (uint)maxValue);
    }

    /// <summary>
    /// Generates a random integer between minValue (inclusive) and maxValue (exclusive).
    /// </summary>
    /// <param name="minValue">Inclusive lower bound.</param>
    /// <param name="maxValue">Exclusive upper bound.</param>
    /// <returns>A random integer.</returns>
    public int NextInt32(int minValue, int maxValue)
    {
        if (maxValue <= minValue)
            throw new ArgumentException("AesCtrRandomNumberGenerator.NextInt32() => maxValue must be greater than minValue.");

        var range = maxValue - minValue;
        
        return NextInt32(range) + minValue;
    }

    /// <summary>
    /// Generates a random byte array of specified length.
    /// </summary>
    /// <param name="count">Number of random bytes to generate.</param>
    /// <returns>Byte array containing random bytes.</returns>
    public byte[] NextBytes(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), "AesCtrRandomNumberGenerator.NextBytes() => count must be non-negative.");

        var result = new byte[count];
        
        for (var i = 0; i < count; i++)
            result[i] = NextByte();
        
        return result;
    }

    /// <summary>
    /// Increments the counter in big-endian order.
    /// </summary>
    private void IncrementCounter()
    {
        for (var i = 15; i >= 0; i--)
        {
            if (++_counter[i] != 0)
                break;
        }
    }

    /// <summary>
    /// Disposes the AES encryptor and AES instance.
    /// </summary>
    public void Dispose()
    {
        _encryptor?.Dispose();
        _aes?.Dispose();
    }
}
