using FakeItEasy;
using Xunit;

namespace AirTransit_Core_Tests.Services
{
    public class RSAEncryptionServiceTests
    {
        [Fact]
        public void GenerateSignature_WithGivenPrivateKey_ShouldBeVerifiableWithAssociatedPublicKey()
        {
            //var keySetRepository = A.Fake<IKeySetRepository>();
        }
    }
}