using Coins.Core.Models.DtoAPI.General;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Api.Utilities
{
    public class General
    {
        public static APIResponse GetValidationErrores(ModelStateDictionary ModelState)
        {
            var response = new APIResponse
            {
                Status = false,
                Message = "Some Filed Required",
                Data = ModelState.Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
            };
            return response;
        }


    }
}
