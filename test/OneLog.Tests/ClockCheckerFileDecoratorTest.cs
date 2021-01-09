using Microsoft.Extensions.Options;
using OneLog.Infrastructure;
using OneLog.Models;
using OneLog.Options;
using System;
using System.IO;
using System.Text;
using Xunit;

namespace Tests
{
    public class ClockCheckerFileDecoratorTest
    {
        private string url = "/api/test";

        /// <summary>
        /// Test to check whether new file will be created when new day comes
        /// </summary>
        /// <param name="clockDay">clockDay has to be less than 0 in order to create new file</param>
        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public void WriteToFileWhenNewDayComes(int clockDay)
        {
            //Arrange
            var clock = new Clock(DateTime.Now.AddDays(clockDay));
            LoggerOptions options = CreateOptions();

            string oldFilePath = $"{options.LoggerFolder}{options.FileName}_{clock.Now:yyyy-MM-dd}.log";
            string newFilePath = $"{options.LoggerFolder}{options.FileName}_{DateTime.Now:yyyy-MM-dd}.log";

            string expectedFileNameOld = $"{options.FileName}_{clock.Now:yyyy-MM-dd}.log";
            string expectedFileNameNew = $"{options.FileName}_{DateTime.Now:yyyy-MM-dd}.log";

            System.IO.File.Delete(newFilePath);
            System.IO.File.Delete(oldFilePath);

            var file = new ClockCheckerFileDecorator(Options.Create(options), clock);

            Request request = CreateRequest();


            //Act
            file.Write(request);
            file.Dispose();

            string fileContent = ReadFile(newFilePath);

            //Assert
            Assert.Equal(expectedFileNameNew, Path.GetFileName(newFilePath));
            Assert.Equal(expectedFileNameOld, Path.GetFileName(oldFilePath));
            Assert.Contains(url, fileContent);
        }

        /// <summary>
        /// Test writing to file
        /// </summary>
        /// <param name="clockDay">clockDay has to be 0 not to create new file</param>
        [Theory]
        [InlineData(0)]
        public void WriteToFileWithoutDateChanging(int clockDay)
        {
            //Arrange
            var clock = new Clock(DateTime.Now.AddDays(clockDay));
            LoggerOptions options = CreateOptions();

            string filePath = $"{options.LoggerFolder}{options.FileName}_{clock.Now:yyyy-MM-dd}.log";
            string fileName = $"{options.FileName}_{clock.Now:yyyy-MM-dd}.log";

            System.IO.File.Delete(filePath);

            var file = new ClockCheckerFileDecorator(Options.Create(options), clock);

            Request request = CreateRequest();

            //Act
            file.Write(request);
            file.Dispose();

            string fileContent = ReadFile(filePath);

            //Assert
            Assert.Equal(fileName, Path.GetFileName(filePath));
            Assert.Contains(url, fileContent);
        }

        private Request CreateRequest()
        {
            var request = new Request(url);
            request.AddEvent("TEST_EVENT", "1", EventCategory.Information);

            try
            {
                throw new Exception("TEST_EXCEPTION");
            }
            catch (Exception ex)
            {
                request.AddException(ex);
            }

            return request;
        }

        private string ReadFile(string filePath)
        {
            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                byte[] array = new byte[stream.Length];
                stream.Read(array, 0, array.Length);
                return Encoding.UTF8.GetString(array);
            }
        }

        private LoggerOptions CreateOptions()
        {
            LoggerOptions options = new LoggerOptions();
            options.LoggerFolder = @"C:\Users\Shevchenko\source\repos\OneLog\Logs\";
            options.FileName = "Requests";
            return options;
        }
    }
}
