using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dsian.TwinCAT.AdsViewer.CapParser.Tests.MoqExtensions
{
    public static class MoqExtensions
    {
        /// <summary>
        /// Check log messages and levels as well as how often, 
        /// <a href="https://adamstorr.azurewebsites.net/blog/mocking-ilogger-with-moq">kudos</a>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logger"></param>
        /// <param name="expectedMessage"></param>
        /// <param name="expectedLogLevel"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public static Mock<ILogger<T>> VerifyLogging<T>(this Mock<ILogger<T>> logger, string expectedMessage, LogLevel expectedLogLevel = LogLevel.Debug, Times? times = null)
        {
            times ??= Times.Once();

            Func<object, Type, bool> state = (v, t) => v.ToString().CompareTo(expectedMessage) == 0;

            logger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == expectedLogLevel),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => state(v, t)),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), (Times)times);

            return logger;
        }

        /// <inheritdoc cref="VerifyLogging{T}(Mock{ILogger{T}}, string, LogLevel, Times?)"/>
        public static Mock<ILogger<T>> VerifyLoggingException<T>(this Mock<ILogger<T>> logger, string expectedMessage, Exception expectedException, LogLevel expectedLogLevel = LogLevel.Error, Times? times = null)
        {
            times ??= Times.Once();

            Func<object, Type, bool> state = (v, t) => v.ToString().CompareTo(expectedMessage) == 0;

            logger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == expectedLogLevel),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => state(v, t)),
                    It.Is<Exception>(ex => ex.Message.Equals(expectedException.Message) && ex.GetType()==expectedException.GetType()),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), (Times)times);

            return logger;
        }
    }
}
