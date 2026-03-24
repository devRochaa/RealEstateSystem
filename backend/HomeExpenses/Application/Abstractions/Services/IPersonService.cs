using HomeExpenses.Application.DTOs.Person;
using HomeExpenses.Application.DTOs.Reports;

namespace HomeExpenses.Application.Abstractions.Services;

public interface IPersonService
{
    Task<List<PersonDto>> GetAll();
    Task<PersonDto> GetById(Guid id);
    Task<PersonDto> Create(CreatePersonDto dto);
    Task<PersonDto?> Update(Guid id, UpdatePersonDto dto);
    Task<PersonTotalsDto> GetTotals();
    Task Remove(Guid id);
}
