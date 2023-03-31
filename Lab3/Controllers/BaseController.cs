using Lab3.ResponceWorker;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult GetResponse(Responce response)
        {
            return response.Code switch
            {
                ResponceCode.Ok => Ok(response),
                ResponceCode.Error => BadRequest(response),
                _ => BadRequest(response)
            };
        }
    }
}
