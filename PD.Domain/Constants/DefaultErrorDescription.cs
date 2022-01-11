using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Constants.Exceptions
{
    class DefaultErrorDescription
    {
        // Exceptions related to requests
        public const string NOT_FOUND = "The requested data was not found.";
        public const string BAD_REQUEST = "Bad request.";

        // Exceptions related to server errors
        public const string UPDATING_FAILED = "An error occurred during updating the data.";
        public const string CREATIONG_FAILED = "An error occurred during creation.";
        public const string DELETION_FAILED = "An error occurred during deletion.";
        public const string GETTING_FAILED = "An error occured during getting requested data.";

        public const string INVALID_CREDENTIALS = "The provided password or email is incorrect.";

        public const string UNKNOWN_ERROR = "An unknown error has occurred.";
    }
}
