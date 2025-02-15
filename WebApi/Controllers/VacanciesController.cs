using DataLayer.DataContexts;
using DataLayer.DTOs;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с вакансиями
    /// </summary>
    [Route("api/v1/")]
    [ApiController]
    public class VacanciesController(AppDbContext context) : ControllerBase
    {

        [HttpGet("Vacancies")]
        [SwaggerOperation(
            Summary = "Получение всех вакансий",
            Description = "Метод возвращающий список всех вакансий из БД")]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешное получение списка. Возврат списка вакансий.", Type = typeof(IEnumerable<VacancyDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Вакансии не найдены. Возврат сообщения об ошибке.", Type = typeof(ApiErrorDto))]
        public async Task<ActionResult<IEnumerable<VacancyDto>>> GetVacanciesAsync()
        {
            var vacancies = await context.Vacancies
                .Include(v => v.Skills)
                .Include(v => v.Position)
                .ToListAsync();

            if (vacancies.Count == 0)
                return NotFound(new ApiErrorDto("Вакансии не найдены", 2001));

            var vacancyDtos = vacancies.Select(v => v.ToDo()).ToList();

            return Ok(vacancyDtos);
        }

        [HttpGet("Vacancy/{id}/Skills")]
        [SwaggerOperation(
            Summary = "Получение списка навыков",
            Description = "Метод принимающий Id вакансии, возващает список навыков этой вакансии из БД")]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешное получение списка. Возврат списка навыков.", Type = typeof(IEnumerable<SkillDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Объект не найден. Возврат сообщения об ошибке.", Type = typeof(ApiErrorDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Неверный параметр. Возврат сообщения об ошибке.", Type = typeof(ApiErrorDto))]
        public async Task<ActionResult<IEnumerable<SkillDto>>> GetSkillsAsync(
            [SwaggerParameter("Id необходимой вакансии", Required = true)] int id)
        {
            if (id <= 0)
                return BadRequest(new ApiErrorDto("Неверный Id. Id должен быть положительным числом", 2002));

            var vacancy = await context.Vacancies
                .Include(v => v.Skills)
                .SingleOrDefaultAsync(v => v.VcancyId == id);

            if (vacancy is null)
                return NotFound(new ApiErrorDto("Вакансия не найдена.", 2004));
            if (!vacancy.Skills.Any())
                return NotFound(new ApiErrorDto("Вакансия не требувет навыков.", 2003));

            var skillsDto = vacancy.Skills
                .Select(s => new SkillDto
                {
                    Id = s.SkillId,
                    Name = s.Name,
                }).ToList();

            return Ok(skillsDto);

        }

        [HttpPost("Vacancy/{id}/Skills")]
        [SwaggerOperation(
            Summary = "Добавление навыка для вакансии",
            Description = "Метод принимающий Id вакансии и данные навыка, при наличии навыка в БД, добавляет навык к вакансии, иначе создает новый. Возвращает навык.")]
        [SwaggerResponse(StatusCodes.Status201Created, "Успешное добавлени навыка. Возврат навыка", Type = typeof(SkillDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Объект не найден. Возврат сообщения об ошибке.", Type = typeof(ApiErrorDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Неверный параметр. Возврат сообщения об ошибке.", Type = typeof(ApiErrorDto))]
        public async Task<ActionResult<SkillDto>> PostSkillAsync(
            [SwaggerParameter("Id необходимой вакансии", Required = true)] int id,
            [SwaggerRequestBody("Данные вакансии", Required = true)] SkillDto skillDto)
        {
            try
            {
                var skill = new Skill();

                var vacancy = await context.Vacancies
                    .Include(v => v.Skills)
                    .SingleOrDefaultAsync(v => v.VcancyId == id);

                var skills = await context.Skills.ToListAsync();

                if (vacancy is null)
                    return NotFound(new ApiErrorDto("Вакансия не найдена.", 2004));
                if (vacancy.Skills.Any(s => s.Name == skillDto.Name))
                    return BadRequest(new ApiErrorDto("Такой навык уже присутствует у вакансии", 2005));

                if (skills.Any(s => s.Name.ToLower() == skillDto.Name.ToLower()))
                {
                    skill = skills.FirstOrDefault(s => s.Name == skillDto.Name);

                    vacancy.Skills.Add(skill);

                    context.Vacancies.Update(vacancy);
                    await context.SaveChangesAsync();
                }
                else
                {
                    skill.Name = skillDto.Name;
                    skill.Vcancies.Add(vacancy);

                    context.Skills.Add(skill);
                    context.Vacancies.Update(vacancy);
                    await context.SaveChangesAsync();
                }

                await context.Vacancies
                    .Include(v => v.Skills)
                    .SingleOrDefaultAsync(v => v.VcancyId == id);

                return Created("", skill);

            }
            catch
            {
                return BadRequest(new ApiErrorDto("Неверные параметры", 2005));
            }
        }


        [HttpPatch("Vacancy/{id}")]
        [SwaggerOperation(
            Summary = "Закрытие вакансии",
            Description = "Метод принимающий Id вакансии, при наличии вакансии в бд, закрывает её. Возвращает закрытую вакансию. ")]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешное закрытие ванкансии. Возврат вакансии.", Type = typeof(VacancyDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Объект не найден. Возврат сообщения об ошибке.", Type = typeof(ApiErrorDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Неверный параметр. Возврат сообщения об ошибке.", Type = typeof(ApiErrorDto))]
        public async Task<ActionResult<VacancyDto>> PatchVacancyAsync(
            [SwaggerParameter("Id необходимой вакансии", Required = true)] int id)
        {

            if (id <= 0)
                return BadRequest(new ApiErrorDto("Неверный Id. Id должен быть положительным числом", 2002));

            var vacancy = await context.Vacancies
                .Include(v => v.Skills)
                .Include(v => v.Position)
                .SingleOrDefaultAsync(v => v.VcancyId == id);

            if (vacancy is null)
                return NotFound(new ApiErrorDto("Вакансия не найдена.", 2004));
            if (vacancy.ClosedDate != null)
                return NotFound(new ApiErrorDto("Вакансия уже закрыта.", 2006));

            vacancy.ClosedDate = DateTime.UtcNow;
            context.Vacancies.Update(vacancy);
            await context.SaveChangesAsync();

            var vacancyDto = vacancy.ToDo();

            return Ok(vacancyDto);
        }
    }
}
