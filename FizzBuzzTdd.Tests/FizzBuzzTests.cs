using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace FizzBuzzTdd.Tests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [TestCase(1, ExpectedResult = "1")]
        [TestCase(2, ExpectedResult = "2")]
        [TestCase(3, ExpectedResult = "Fizz")]
        [TestCase(4, ExpectedResult = "4")]
        [TestCase(5, ExpectedResult = "Buzz")]
        [TestCase(15, ExpectedResult = "FizzBuzz")]
        public string FizzBuzz_Can_Return_Correct_Result(int input)
        {
            // Arrange
            var mockLogService = new Mock<ILogService>();
            mockLogService.Setup(x => x.Log(It.IsAny<string>())).Callback( () => Console.WriteLine("mock logger called")).Verifiable();
            var fizzBuzzService = new FizzBuzzService(mockLogService.Object);

            // Act
            var result = fizzBuzzService.Calculate(input);
            
            // Assert
            mockLogService.Verify(x => x.Log(It.IsAny<string>()), Times.Exactly(2));

            return result;
        }
    }

    public class LogService : ILogService
    {
        public void Log(string s)
        {
            Console.WriteLine(s);
        }
    }

    public interface ILogService
    {
        void Log(string s);
    }

    public class FizzBuzzService
    {
        private readonly ILogService _logService;

        public FizzBuzzService(ILogService logService)
        {
            _logService = logService;
        }

        public string Calculate(int input)
        {
            _logService.Log($"input : {input}");
            string result = String.Empty;
            if (input%3 == 0)
            {
                result = "Fizz";
            }
            if (input%5 == 0)
            {
                result += "Buzz";
            }
            if (result == String.Empty)
            {
                result = input.ToString();
            }
            _logService.Log($"result : {result}");
            return result;
        }
    }
}
