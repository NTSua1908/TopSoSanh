﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TopSoSanh.DTO;

namespace TopSoSanh.Controllers
{
    public class BaseController : ControllerBase
    {
        public DateTime now = DateTime.Now;

        [ApiExplorerSettings(IgnoreApi = true)]
        public void AddErrorsFromResult(IdentityResult result, ref ErrorModel errors)
        {
            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void AddErrorsFromModelState(ref ErrorModel errors)
        {
            foreach (var modelState in ModelState.Values)
            {
                var errorState = modelState.Errors.FirstOrDefault();

                if (errorState != null)
                {
                    string errorMessage = errorState.ErrorMessage;

                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        errors.Add(errorMessage);
                    }
                    else
                    {
                        errors.Add(modelState.Errors.FirstOrDefault()?.Exception.Message);
                    }
                }
            }
        }
    }
}
