using System;
using System.Runtime.Serialization;

namespace Coins.Core
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message, int entityId) : base($"{message} {entityId}")
        {
        }

        public NotFoundException(object entityId) : base($"No item found with id {entityId}")
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
