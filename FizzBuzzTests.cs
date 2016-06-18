using NUnit.Framework;
using System;
using Moq;

namespace FizzBuzzTdd
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
        public string Test_FizzBuzz_Calculated_Result(int input)
        {
            //Assert
            var mockLogger = new Mock<ILogService>();
            mockLogger.Setup(x => x.Log(It.IsAny<string>())).Verifiable();
            var fizzBuzzService = new FizzBuzzService(mockLogger.Object);

            //Act
            var result = fizzBuzzService.Calculate(input);

            //Assert
            mockLogger.Verify(x => x.Log(It.IsAny<string>()), Times.Exactly(2));
            return result;
        }
    }

    public interface ILogService
    {
        void Log(string s);
    }

    // Delete implementation to prove that London Style is not using Concrete classes at all
    public class LogService : ILogService
    {
        public void Log(string s)
        {
            Console.WriteLine(s);
        }
    }

    public class FizzBuzzService
    {
        private readonly ILogService _logger;

        public FizzBuzzService(ILogService logService)
        {
            _logger = logService;
        }

        public string Calculate(int input)
        {
            _logger.Log($"Input : {input}");
            string result;
            if (input % 3 == 0 && input%5 == 0)
            {
                result = "FizzBuzz";
            }
            else if (input % 3 == 0)
            {
                result = "Fizz";
            }
            else if (input%5 == 0)
            {
                result = "Buzz";
            }
            else
            {
                result = input.ToString();
            }
            _logger.Log($"Result : {result}");
            return result;
        }
    }
}
