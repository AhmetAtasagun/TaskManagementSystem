using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TMS.Api.Controllers.v1.Base;
using TMS.Business.Abstracts;
using TMS.Models.Dtos;
using TMS.Models.Dtos.Requests;

namespace TMS.Api.Controllers.v1
{
    [Authorize]
    public class JobsController : BaseApiController
    {
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost("Get")]
        public List<JobDto> Get(JobGetRequestDto jobGetRequest)
        {
            return _jobService.Get(jobGetRequest);
        }

        [HttpPost("Add")]
        public JobDto Add(JobAddRequestDto jobAddRequest)
        {
            return _jobService.Add(jobAddRequest);
        }

        [HttpPost("Delete")]
        public bool Delete(JobDeleteRequestDto jobDeleteRequest)
        {
            return _jobService.Delete(jobDeleteRequest);
        }

        [HttpPost("SetStatus")]
        public JobDto SetStatus(JobSetStatusRequestDto jobSetStatusRequest)
        {
            return _jobService.SetStatus(jobSetStatusRequest);
        }
    }
}
