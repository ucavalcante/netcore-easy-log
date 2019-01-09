using System;
using System.IO;
using System.Linq;
using Bogus;
using Ncel;
using Xunit;

namespace unitTest
{
    public class LogTest
    {
        Faker faker = new Faker("pt_BR");
        [Fact]
        public void LogGeneratesFile()
        {
            //Given
            var x = new FileInfo(Utilities.DestinationPath());
            x.Delete();
            //When
            Log.Information($"{faker.Random.Number(1, 100)} Testando");
            //Then
            Assert.True(x.Exists);
        }
        [Fact]
        public void LogRecordProperInformationInfile()
        {
            //Given
            var x = $"{faker.Random.Number(1, 100)} Testando";
            //When
            Log.Information(x);
            //Then
            Assert.Contains(x, File.ReadLines(Utilities.DestinationPath()).Last());
        }
        [Fact]
        public void LogRecordProperErrorInfile()
        {
            //Given
            var x = $"{faker.Random.Number(1, 100)} Testando";
            //When
            Log.Error(x);
            //Then
            Assert.Contains(x, File.ReadLines(Utilities.DestinationPath()).Last());
        }
        [Fact]
        public void LogConsoleMessageActivate()
        {
            //Given
            var x = $"{faker.Random.Number(1, 100)} Testando";
            StringWriter y = new StringWriter();
            Console.SetOut(y);
            //When
            LogConfig.WriteConsole = true;
            Log.Information(x);
            //Then
            Assert.Contains(x, y.ToString());
        }
        [Fact]
        public void LogConsoleMessageDeactivated()
        {
            //Given
            var x = $"{faker.Random.Number(1, 100)} Testando";
            StringWriter y = new StringWriter();
            Console.SetOut(y);
            //When
            LogConfig.WriteConsole = false;
            Log.Information(x);
            //Then
            Assert.DoesNotContain(x, y.ToString());
        }
        [Fact]
        public void LogPath()
        {
            //Given
            LogConfig.DirectoryPath = $"{Environment.CurrentDirectory}\\Logs\\{faker.System.DirectoryPath().Remove(0,1).Replace('/','\\')}";
            var x = new FileInfo(Utilities.DestinationPath());
            //When
            Log.Information($"{faker.Random.Number(1, 100)} Testando");
            System.Threading.Thread.Sleep(1000);
            //Then
            Assert.True(x.Exists);
        }

    }
}