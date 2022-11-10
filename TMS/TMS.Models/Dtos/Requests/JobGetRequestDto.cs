using TMS.Core.Abstracts;
using TMS.Models.ComplexTypes;

namespace TMS.Models.Dtos.Requests
{
    public class JobGetRequestDto : IDto
    {
        public JobStatus Status { get; set; }
    }
}
