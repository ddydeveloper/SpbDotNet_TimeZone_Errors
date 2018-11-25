
using System;
using TimeZoneConvert;
using Xunit;

namespace TimeZoneConverterTest
{
    public class ConverterTest
    {
        [Fact]
        public void ConvertToTimeZone_PST()
        {
            var date = DateTime.Now;
            var success =
                Converter.TryConvertToTimeZone(date, "Pacific Standard Time", out var result, out var errorMsg);

            if (errorMsg != null)
            {
                throw new Exception(errorMsg);
            }

            Assert.True(success);
        }

        [Fact]
        public void ConvertToTimeZone_CST()
        {
            var date = DateTime.Now;
            var success =
                Converter.TryConvertToTimeZone(date, "Central Standard Time", out var result, out var errorMsg);

            if (errorMsg != null)
            {
                throw new Exception(errorMsg);
            }

            Assert.True(success);
        }

        [Fact]
        public void ConvertToTimeZone_NotFound()
        {
            var date = DateTime.Now;
            var success = Converter.TryConvertToTimeZone(date, "Central121221 Standard Time", out var result, out var errorMsg);
            Assert.False(success);
            Assert.NotNull(errorMsg);
            Assert.Contains("The registry does not define the Central121221 Standard Time zone", errorMsg);
        }
    }
}
