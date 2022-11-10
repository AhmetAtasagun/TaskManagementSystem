using TMS.Models.ComplexTypes;

namespace TMS.Models.Dtos.Requests
{
    public class JobSetStatusRequestDto
    {
        public int Id { get; set; }
        public JobStatus Status { get; set; }
    }
}
