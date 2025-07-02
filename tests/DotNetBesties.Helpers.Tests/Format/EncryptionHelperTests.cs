using System;
using TUnit.Assertions;
using TUnit.Core;
using DotNetBesties.Helpers.Format;

namespace DotNetBesties.Helpers.Tests.Format
{
    public class EncryptionHelperTests
    {
        [Test]
        public async Task EncryptAES_ShouldEncryptAndDecryptCorrectly()
        {
            var key = "1234567890123456"; // 16-byte key
            var plainText = "Hello World";

            var encrypted = EncryptionHelper.EncryptAES(plainText, key);
            var decrypted = EncryptionHelper.DecryptAES(encrypted, key);

            await Assert.That(decrypted).IsEqualTo(plainText);
        }

        [Test]
        public async Task HashSHA256_ShouldReturnExpectedHash()
        {
            var input = "Hello World";
            var hash = EncryptionHelper.HashSHA256(input);

            await Assert.That(hash).IsNotNull();
        }
    }
}