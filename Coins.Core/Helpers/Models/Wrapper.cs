using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Helpers.Models
{
    public class Wrapper<T>
    {
        public T Data { get; set; }
        public Pagination Pagination { get; set; }

        public static implicit operator Wrapper<T>(T data)
            => new Wrapper<T> { Data = data };
    }
}
