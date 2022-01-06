using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Constants.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message = DefaultErrorDescription.NOT_FOUND) : base(message) { }
    }

    public class BadRequestException : Exception
    {
        public BadRequestException(string message = DefaultErrorDescription.BAD_REQUEST) : base(message) { }
    }

    public class UpdatingFailedException : Exception
    {
        public UpdatingFailedException(string message = DefaultErrorDescription.UPDATING_FAILED) : base(message) { }
    }

    public class CreatingFailedException : Exception
    {
        public CreatingFailedException(string message = DefaultErrorDescription.CREATIONG_FAILED) : base(message) { }
    }

    public class DeletionFailedException : Exception
    {
        public DeletionFailedException(string message = DefaultErrorDescription.DELETION_FAILED) : base(message) { }
    }

    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException(string message = DefaultErrorDescription.INVALID_CREDENTIALS) : base(message) { }
    }

    public class GettingFailedException : Exception
    {
        public GettingFailedException(string message = DefaultErrorDescription.GETTING_FAILED) : base(message) { }
    }
}
