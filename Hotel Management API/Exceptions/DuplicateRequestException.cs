using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Hotel_Management_API.Exceptions
{
    public class DuplicateRequestException: Exception
    {
        public DuplicateRequestException()
        {
        }

        public DuplicateRequestException(string message) : base(message)
        {
        }

        public DuplicateRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}