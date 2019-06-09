using Microsoft.AspNetCore.Mvc;
using Testul.Services;

namespace Testul.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ApiExceptionController
    {
        [HttpPost]
        public ActionResult<string> Post(string expression)
        {
            return ExceptionHandling(() => OperatorProcess.Evaluate(expression));
        }
    }
}
