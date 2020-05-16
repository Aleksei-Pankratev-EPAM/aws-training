using BookChest.Domain.Services;
using BookChest.Infrastructure.Services;
using NUnit.Framework;

namespace BookChest.Tests.Services
{
    public class IsbnValidatorTests
    {
        [TestCase("80-85892-15-4", Description = "ISBN 10, with hyphens")]
        [TestCase("ISBN 80-85892-15-4", Description = "ISBN 10, prefix, hyphens")]
        [TestCase("978-80-86056-31-9", Description = "ISBN 13, with hyphens")]
        [TestCase("ISBN 978-80-86056-31-9", Description = "ISBN 13, prefix, hyphens")]
        public void CheckValidIsbns(string isbnString)
        {
            var validator = CreateService();

            var actual = validator.IsValidIsbn(isbnString);

            Assert.True(actual);
        }

        [TestCase("80-8892-15-4", Description = "Too little digits")]
        [TestCase("80-858892-15-4", Description = "Too many digits")]
        [TestCase("80-85892-15-6", Description = "Wrong checksum")]
        [TestCase("IBSN 80-85892-15-4", Description = "Wrong prefix")]
        [TestCase("80-85-892-15-4", Description = "Too many groups")]
        public void CheckInvalidIsbns(string isbnString)
        {
            var validator = CreateService();

            var actual = validator.IsValidIsbn(isbnString);

            Assert.False(actual);
        }

        [TestCase("ISBN     978-80-86056-31-9", "978-80-86056-31-9")]
        [TestCase("ISBN-13: 978-80-86056-31-9", "978-80-86056-31-9")]
        [TestCase("ISBN     80-85892-15-4", "80-85892-15-4")]
        [TestCase("ISBN-10  80-85892-15-4", "80-85892-15-4")]
        [TestCase("ISBN-10: 80-85892-15-4", "80-85892-15-4")]
        public void CanSanitizeIsbn(string isbnString, string expectedResult)
        {
            var validator = CreateService();

            var actualResult = validator.Sanitize(isbnString);

            Assert.AreEqual(expectedResult, actualResult);
        }

        private IsbnValidator CreateService()
        {
            return new IsbnValidator();
        }
    }
}
