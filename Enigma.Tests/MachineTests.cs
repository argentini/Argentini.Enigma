using Xunit;

namespace Enigma.Tests;

public class MachineTests
{
	[Fact]
	public void CipherTest()
    {
        const string Message = "This is a test";

        var rotor1 = new Rotor
        {
            Wheel = Constants.Rotor1
        };
        
        var reflector = new Reflector
        {
            Wires = Constants.Reflector1
        };
        
        var enciphered = string.Empty;

        foreach (var c in Message)
        {
            var e = rotor1.ReflectedCharacter(reflector.SendCharacter(rotor1.SendCharacter(c)));

            enciphered += e;

            rotor1.Rotation++;
        }
        
        Assert.Equal("V[iX[z_eTw}wbV", enciphered);
        
        var deciphered = string.Empty;

        rotor1.Rotation = 0;

        foreach (var c in enciphered)
        {
            var e = rotor1.ReflectedCharacter(reflector.SendCharacter(rotor1.SendCharacter(c)));

            deciphered += e;

            rotor1.Rotation++;
        }
        
        Assert.Equal(Message, deciphered);
    }
}
