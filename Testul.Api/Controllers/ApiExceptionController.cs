using System;
using Microsoft.AspNetCore.Mvc;


namespace Testul.Api.Controllers
{
    public abstract class ApiExceptionController : ControllerBase
    {
        protected ActionResult ExceptionHandling<T>(Func<T> action)
        {
            try
            {
                var result = action();

                if (result == null)
                {
                    return NotFound();
                }

                if (result is ActionResult)
                {
                    return result as ActionResult;
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}