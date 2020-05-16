using BookChest.Domain.Models;
using NUnit.Framework;

namespace BookChest.Tests.Models
{
    public class IsbnTests
    {
        [TestCase("123-456", IsbnFormat.None, "123456")]
        [TestCase("123-456", IsbnFormat.IncludeHyphens, "123-456")]
        [TestCase("123-456", IsbnFormat.IncludePrefix, "ISBN 123456")]
        [TestCase("123-456", IsbnFormat.IncludePrefix | IsbnFormat.IncludeHyphens, "ISBN 123-456")]
        public void TestToString(string inputString, IsbnFormat format, string expectedResult)
        {
            var isbn = new Isbn(inputString);

            var actualResult = isbn.ToString(format);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
