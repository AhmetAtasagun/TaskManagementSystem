using TMS.Models.Dtos;
using TMS.Models.Dtos.Requests;

namespace TMS.Business.Abstracts
{
    public interface IJobService
    {
        List<JobDto> GetList(JobGetRequestDto jobGetRequest);
        JobDto Add(JobAddRequestDto jobAddRequest);
        JobDto Add(JobDto job);
        bool Delete(JobDeleteRequestDto jobDeleteRequest);
        JobDto SetStatus(JobSetStatusRequestDto jobSetStatusRequest);
        bool HasData();
    }
}
