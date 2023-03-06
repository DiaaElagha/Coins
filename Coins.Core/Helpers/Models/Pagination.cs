using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coins.Core.Helpers.Models
{
    public class Pagination
    {
        [JsonIgnore]
        public int TotalItems { get; private set; }
        [JsonIgnore]
        public int PageSize { get; set; }
        public int TotalPages { get; private set; } // this is the only property that is assigned
        public Pagination(int _pageSize, int _totalItems)
        {
            TotalItems = _totalItems;
            PageSize = _pageSize;
            TotalPages = (int)Math.Ceiling(_totalItems / (decimal)_pageSize);
        }
    }
}
