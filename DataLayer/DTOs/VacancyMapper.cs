using DataLayer.Models;

namespace DataLayer.DTOs
{
    public static class VacancyMapper
    {
        public static VacancyDto ToDo(this Vacancy vacancy)
        {
            if(vacancy == null) 
                throw new ArgumentNullException(nameof(vacancy));

            return new VacancyDto
            {
                VcancyId = vacancy.VcancyId,
                Position = vacancy.Position.Name,
                Title = vacancy.Title,
                Description = vacancy.Description,
                CreatedDate = vacancy.CreatedDate,
                ClosedDate = vacancy.ClosedDate,
                Skills = string.Join(", ", vacancy.Skills.Select(s => s.Name)),
            };
        }
    }
}
