using TMS.Business.Abstracts;
using TMS.Core.Abstracts;
using TMS.Core.Helpers.EasyMapper;
using TMS.Models.Dtos;
using TMS.Models.Dtos.Requests;
using TMS.Models.Entites;

namespace TMS.Business.Services
{
    public class JobService : IJobService
    {
        private readonly IRepository<Job> _jobRepository;

        public JobService(IRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public List<JobDto> Get(JobGetRequestDto jobGetRequest)
        {
            return _jobRepository.GetList(x => x.Status == jobGetRequest.Status).ToMap<JobDto>().ToList();
        }

        public JobDto Add(JobAddRequestDto jobAddRequest)
        {
            return _jobRepository.Add(jobAddRequest.ToMap<Job>()).ToMap<JobDto>();
        }

        public bool Delete(JobDeleteRequestDto jobDeleteRequest)
        {
            return _jobRepository.Delete(jobDeleteRequest.Id);
        }

        public JobDto SetStatus(JobSetStatusRequestDto jobSetStatusRequest)
        {
            var task = _jobRepository.Table.Find(jobSetStatusRequest.Id);
            task.Status = jobSetStatusRequest.Status;
            return _jobRepository.Update(task).ToMap<JobDto>();
        }
    }
}
