using System;
using NUnit.Framework;
using SharpArch.Testing.NUnit;
using SharpArchContrib.Core.Logging;

namespace Tests.SharpArchContrib.Core.Logging
{
    [TestFixture]
    public class ExceptionHandlerAttributeTests
    {
        [ExceptionHandler]
        private void ThrowException()
        {
            throw new NotImplementedException();
        }

        [ExceptionHandler(IsSilent = true, ReturnValue = 6f)]
        private float ThrowExceptionSilentWithReturn()
        {
            throw new NotImplementedException();
            return 7f;
        }

        [ExceptionHandler(IsSilent = true, ReturnValue = 6f)]
        private void ThrowExceptionSilent()
        {
            throw new NotImplementedException();
        }

        [ExceptionHandler(IsSilent = true, ReturnValue = 6f, AspectPriority = 1)]
        [Log(ExceptionLevel = LoggingLevel.Error, AspectPriority = 2)]
        private float ThrowExceptionSilentWithReturnWithLogAttribute()
        {
            throw new NotImplementedException();
            return 7f;
        }

        [Test]
        public void LoggedExceptionDoesNotRethrow()
        {
            Assert.DoesNotThrow(() => ThrowExceptionSilent());
        }

        [Test]
        public void LoggedExceptionDoesNotRethrowWithReturn()
        {
            ThrowExceptionSilentWithReturn().ShouldEqual(6f);
        }

        [Test]
        public void LoggedExceptionDoesNotRethrowWithReturnWithLogAttribute()
        {
            ThrowExceptionSilentWithReturnWithLogAttribute().ShouldEqual(6f);
        }

        [Test]
        public void LoggedExceptionRethrows()
        {
            Assert.Throws<NotImplementedException>(() => ThrowException());
        }
    }
}