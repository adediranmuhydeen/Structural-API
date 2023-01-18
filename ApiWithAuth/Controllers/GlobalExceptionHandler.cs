using ApiWithAuth.Core.Domain;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ApiWithAuth.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    public class GlobalExceptionHandler : ControllerBase
    {

        public IActionResult HandleError()
        {
            var exceptionHandleFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionHandleFeature!.Error;
            if (exception is NotFoundException notFoundException)
            {
                return Problem(notFoundException.Message, null, 404);
            }
            return Problem("Ooops");
        }
    }
}
