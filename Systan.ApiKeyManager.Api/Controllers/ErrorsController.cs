using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Systan.ApiKeyManager.Core.Dtos.Common;
using System.Collections.Generic;

namespace Systan.ApiKeyManager.Api.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public ErrorResponseDto Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context!.Error;

            if (exception.GetType() == typeof(ArgumentException))
                Response.StatusCode = 400;
            else
                Response.StatusCode = 500;
            
            return new ErrorResponseDto(exception); // Your error model
        }
    }
}
