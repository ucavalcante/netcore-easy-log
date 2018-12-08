using System;
using System.IO;
using System.Linq;
using Ncel;
using Xunit;

namespace unitTest
{
    public class LogTest
    {
        [Fact]
        public void LogGeneratesFile()
        {
            //Given
            var x = new FileInfo(Utilities.DestinationPath());
            //When
            Log.Information("123 Testando");
            //Then
            Assert.True(x.Exists);
        }
        [Fact]
        public void LogRecordProperInformationInfile()
        {
            //Given
            var x = File.ReadLines(Utilities.DestinationPath()).Last();
            var y = "123 Testando";
            //When
            Log.Information(y);
            //Then
            Assert.True(x.Contains(y));
        }
    }
}