using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TMS.Business.Abstracts;
using TMS.Models.ComplexTypes;
using TMS.Models.Dtos;

namespace TMS.Business.Managers
{
    public class SeedDataManager
    {
        public static void SeedJobs(HttpContext context)
        {
            var jobService = context.RequestServices.GetRequiredService<IJobService>();
            if (jobService.HasData()) return;
            var memberCount = typeof(JobStatus).GetMembers().Where(w =>
                w.MemberType == System.Reflection.MemberTypes.Field && !(new string[] { "_", "Int32", "int32" }.Any(a => w.Name.Contains(a))))
                .Count();

            for (int i = 0; i < 30; i++)
            {
                jobService.Add(GetRandomJob(memberCount));
            }
        }

        private static JobDto GetRandomJob(int memberCount)
        {
            var random = new Random();
            #region Random Day
            var createDay = DateTime.UtcNow;
            var startDay = DateTime.UtcNow.AddDays(random.Next(0, 200));
            var finishDay = DateTime.UtcNow.AddDays(random.Next(0, 200));
            #endregion

            #region Random Job
            var job = new JobDto
            {
                Title = $"Title {createDay.Day}",
                JobContent = $"JobContent {createDay}",
            };
            #endregion

            #region Random Status
            var nextIndex = new Random().Next(0, memberCount);
            job.Status = (JobStatus)nextIndex;
            switch (job.Status)
            {
                case JobStatus.Todo: break;
                case JobStatus.Improgress:
                    job.StartDate = startDay; break;
                case JobStatus.Complete:
                    job.StartDate = startDay;
                    job.FinishDate = finishDay; break;
                default: break;
            }
            #endregion

            return job;
        }
    }
}
