using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // endpoint will be localhost.../api/<something> (last part of this is the first part of the name of the controller)
    public class BaseApiController : ControllerBase
    { }
}