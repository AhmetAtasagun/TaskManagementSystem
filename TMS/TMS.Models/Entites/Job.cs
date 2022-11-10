using System.ComponentModel.DataAnnotations;
using TMS.Models.ComplexTypes;
using TMS.Models.Entites.Base;

namespace TMS.Models.Entites;

public class Job : BaseEntity
{
    [StringLength(50)]
    public string Title { get; set; }

    [DataType(DataType.Text)]
    public string JobContent { get; set; }
    public JobStatus Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
}
