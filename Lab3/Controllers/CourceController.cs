using DomainLab3.Models.Dtos;
using Lab3.Converters;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourceController : BaseController
    {
        private readonly ICourceApiService _courceApiService;
        
        public CourceController(ICourceApiService cource)
        {
            _courceApiService = cource;
        }

        [HttpPost, Route("add")]
        public IActionResult SaveCource([FromBody] SaveCourceParamsDto courceParams)
        {
            return GetResponse(_courceApiService.SaveCource(courceParams.ConvertToCource()));
        }

        [HttpPost, Route("delete")]
        public IActionResult DeleteCource([FromBody] Guid courceId)
        {
            return GetResponse(_courceApiService.DeleteCource(courceId));
        }

        [HttpPost, Route("get")]
        public IActionResult GetCourceStatus([FromBody] GetCourceStatusParamsDto matherialParams)
        {
            return GetResponse(_courceApiService.GetCourceStatus(matherialParams));
        }

        [HttpPost, Route("enrollment/save")]
        public IActionResult SaveEnrollment([FromBody] SaveEnrollmentParamsDto enrollmentParams)
        {
            return GetResponse(_courceApiService.SaveEnrollment(enrollmentParams));
        }

        [HttpPost, Route("matherial/save")]
        public IActionResult SaveMatherial([FromBody] SaveMatherialStatusParamsDto matherialParams)
        {
            return GetResponse(_courceApiService.SaveMatherial(matherialParams));
        }
    }
}
