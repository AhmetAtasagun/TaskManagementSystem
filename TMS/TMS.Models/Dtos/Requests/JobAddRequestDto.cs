using TMS.Core.Abstracts;

namespace TMS.Models.Dtos.Requests
{
    public class JobAddRequestDto : IDto
    {
        public string Title { get; set; }
        public string JobContent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
    }
}
