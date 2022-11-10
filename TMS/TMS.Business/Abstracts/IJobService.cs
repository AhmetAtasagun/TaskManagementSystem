using TMS.Models.Dtos;
using TMS.Models.Dtos.Requests;

namespace TMS.Business.Abstracts
{
    public interface IJobService
    {
        List<JobDto> Get(JobGetRequestDto jobGetRequest);
        JobDto Add(JobAddRequestDto jobAddRequest);
        bool Delete(JobDeleteRequestDto jobDeleteRequest);
        JobDto SetStatus(JobSetStatusRequestDto jobSetStatusRequest);
    }
}
