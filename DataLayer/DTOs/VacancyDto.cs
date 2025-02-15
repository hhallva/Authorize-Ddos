using DataLayer.Models;

namespace DataLayer.DTOs
{
    public class VacancyDto
    {
        public int VcancyId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        public string Position { get; set; } = null!;

        public string? Skills { get; set; } 
    }
}
