using TMS.Core.Abstracts;
using TMS.Models.ComplexTypes;

namespace TMS.Models.Dtos
{
    public class JobDto : IDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string JobContent { get; set; }
        public JobStatus Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
    }
}
