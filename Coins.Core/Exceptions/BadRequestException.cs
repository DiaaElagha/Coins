using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message, int entityId) : base($"{message} {entityId}")
        {
        }

        public BadRequestException(int entityId) : base($"No item found with id {entityId}")
        {
        }

        protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

}
