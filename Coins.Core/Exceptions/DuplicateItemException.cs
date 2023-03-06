using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core
{
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException(string message, int duplicateItemId) : base($"{message} {duplicateItemId}")
        {
        }

        public DuplicateItemException(int duplicateItemId) : base($"Duplicate items with id {duplicateItemId}")
        {
        }

        public DuplicateItemException(string message) : base(message)
        {
        }

        public DuplicateItemException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
