using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using IranSystemConvertor;

namespace UnitTests
{
    [TestFixture]
    public class IranSystemTests
    {

        [Test]
        public void Arabic_To_IranSystem_Normal_Test()
        {
            var input = "محمد";
            var s = Convert(input);
            Assert.True(s.SequenceEqual(new byte[] { 162, 245, 159, 245 }));

            input = "123";
            s = Convert(input);
            Assert.True(s.SequenceEqual(new byte[] { 49, 50, 51 }));

            input = "کهف";
            s = Convert(input);
            Assert.True(s.SequenceEqual(new byte[] { 234, 250, 238 }));

            input = "سلام";
            s = Convert(input);
            Assert.True(s.SequenceEqual(new byte[] { 245, 242, 168 }));

            input = "سلی";
            s = Convert(input);
            Assert.True(s.SequenceEqual(new byte[] { 252, 243, 168 }));

            input = "سلیا";
            s = Convert(input);
            Assert.True(s.SequenceEqual(new byte[] { 145, 254, 243, 168 }));

        }

        private static byte[] Convert(string input)
        {
            var bytes1256 = Encoding.Convert(Encoding.Unicode, Encoding.GetEncoding(1256), Encoding.Unicode.GetBytes(input));
            var string1256 = Encoding.GetEncoding(1256).GetString(bytes1256);
            IEnumerable<byte> iranSystem = ConvertToIranSystem.IranSystem(string1256);
            return iranSystem.ToArray();
        }
    }
}
