using System.Linq.Expressions;
using TMS.Business.Abstracts;
using TMS.Core.Abstracts;
using TMS.Core.Helpers.EasyMapper;
using TMS.Models.ComplexTypes;
using TMS.Models.Dtos;
using TMS.Models.Dtos.Requests;
using TMS.Models.Entites;
using TMS.Models.Exceptions;

namespace TMS.Business.Services
{
    public class JobService : IJobService
    {
        private readonly IRepository<Job> _jobRepository;

        public JobService(IRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public List<JobDto> GetList(JobGetRequestDto jobGetRequest)
        {
            var now = DateTime.UtcNow;
            var nowDayStart = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            var nowDayEnd = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            IQueryable<Job> jobQueryable = jobGetRequest.Timeline switch
            {
                Timeline.All => _jobRepository.GetQueryable(),
                Timeline.DailyByCreateTime => _jobRepository.GetQueryable(x => x.CreateDate >= nowDayStart && x.CreateDate <= nowDayEnd.AddDays(1), true),
                Timeline.DailyByStartTime => _jobRepository.GetQueryable(x => x.StartDate != null && x.StartDate >= nowDayStart && x.StartDate <= nowDayEnd.AddDays(1), true),
                Timeline.DailyByFinishTime => _jobRepository.GetQueryable(x => x.FinishDate != null && x.FinishDate >= nowDayStart && x.FinishDate <= nowDayEnd.AddDays(1), true),
                Timeline.WeeklyByCreateTime => _jobRepository.GetQueryable(x => x.CreateDate >= nowDayStart && x.CreateDate <= nowDayEnd.AddDays(7), true),
                Timeline.WeeklyByStartTime => _jobRepository.GetQueryable(x => x.StartDate != null && x.StartDate >= nowDayStart && x.StartDate <= nowDayEnd.AddDays(7), true),
                Timeline.WeeklyByFinishTime => _jobRepository.GetQueryable(x => x.FinishDate != null && x.FinishDate >= nowDayStart && x.FinishDate <= nowDayEnd.AddDays(7), true),
                Timeline.MonthlyByCreateTime => _jobRepository.GetQueryable(x => x.CreateDate >= nowDayStart && x.CreateDate <= nowDayEnd.AddMonths(1), true),
                Timeline.MonthlyByStartTime => _jobRepository.GetQueryable(x => x.StartDate != null && x.StartDate >= nowDayStart && x.StartDate <= nowDayEnd.AddMonths(1), true),
                Timeline.MonthlyByFinishTime => _jobRepository.GetQueryable(x => x.FinishDate != null && x.FinishDate >= nowDayStart && x.FinishDate <= nowDayEnd.AddMonths(1), true),
                _ => throw new AppException("Bilinmeyen sorgu biçimi")
            };
            jobQueryable = jobGetRequest.Status switch
            {
                JobStatus.Todo => jobQueryable.Where(x => x.Status == JobStatus.Todo),
                JobStatus.Improgress => jobQueryable.Where(x => x.Status == JobStatus.Improgress),
                JobStatus.Complete => jobQueryable.Where(x => x.Status == JobStatus.Complete),
            };
            return jobQueryable.ToMap<JobDto>().ToList();

            #region Alternatif Yaklaşım
            Expression<Func<Job, bool>> expression = jobGetRequest.Timeline switch
            {
                Timeline.All => x => true,
                Timeline.DailyByCreateTime => x => x.CreateDate >= nowDayStart && x.CreateDate <= nowDayEnd.AddDays(1),
                Timeline.DailyByStartTime => x => x.StartDate != null && x.StartDate >= nowDayStart && x.StartDate <= nowDayEnd.AddDays(1),
                Timeline.DailyByFinishTime => x => x.FinishDate != null && x.FinishDate >= nowDayStart && x.FinishDate <= nowDayEnd.AddDays(1),
                Timeline.WeeklyByCreateTime => x => x.CreateDate >= nowDayStart && x.CreateDate <= nowDayEnd.AddDays(7),
                Timeline.WeeklyByStartTime => x => x.StartDate != null && x.StartDate >= nowDayStart && x.StartDate <= nowDayEnd.AddDays(7),
                Timeline.WeeklyByFinishTime => x => x.FinishDate != null && x.FinishDate >= nowDayStart && x.FinishDate <= nowDayEnd.AddDays(7),
                Timeline.MonthlyByCreateTime => x => x.CreateDate >= nowDayStart && x.CreateDate <= nowDayEnd.AddMonths(1),
                Timeline.MonthlyByStartTime => x => x.StartDate != null && x.StartDate >= nowDayStart && x.StartDate <= nowDayEnd.AddMonths(1),
                Timeline.MonthlyByFinishTime => x => x.FinishDate != null && x.FinishDate >= nowDayStart && x.FinishDate <= nowDayEnd.AddMonths(1),
                _ => x => false
            };
            return _jobRepository.GetList(expression, true).ToMap<JobDto>().ToList();
            #endregion
        }

        public JobDto Add(JobAddRequestDto jobAddRequest)
        {
            return _jobRepository.Add(jobAddRequest.ToMap<Job>()).ToMap<JobDto>();
        }

        public JobDto Add(JobDto job)
        {
            return _jobRepository.Add(job.ToMap<Job>()).ToMap<JobDto>();
        }

        public bool Delete(JobDeleteRequestDto jobDeleteRequest)
        {
            return _jobRepository.Delete(jobDeleteRequest.Id);
        }

        public JobDto SetStatus(JobSetStatusRequestDto jobSetStatusRequest)
        {
            var task = _jobRepository.Get(x => x.Id == jobSetStatusRequest.Id);
            if (task.Status == JobStatus.Todo && jobSetStatusRequest.Status == JobStatus.Improgress) task.StartDate = DateTime.UtcNow;
            else if (task.Status == JobStatus.Todo && jobSetStatusRequest.Status == JobStatus.Complete) task.FinishDate = DateTime.UtcNow;
            else if (task.Status == JobStatus.Improgress && jobSetStatusRequest.Status == JobStatus.Todo) task.StartDate = null;
            else if (task.Status == JobStatus.Improgress && jobSetStatusRequest.Status == JobStatus.Complete) task.FinishDate = DateTime.UtcNow;
            else if (task.Status == JobStatus.Complete && jobSetStatusRequest.Status == JobStatus.Improgress) task.FinishDate = null;
            else if (task.Status == JobStatus.Complete && jobSetStatusRequest.Status == JobStatus.Todo)
            {
                task.StartDate = null;
                task.FinishDate = null;
            }
            task.Status = jobSetStatusRequest.Status;
            return _jobRepository.Update(task).ToMap<JobDto>();
        }

        public bool HasData()
        {
            return _jobRepository.GetQueryable().Any();
        }
    }
}
