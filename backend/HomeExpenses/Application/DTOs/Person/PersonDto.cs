namespace HomeExpenses.Application.DTOs.Person;

public class PersonDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; } 
    public int Age { get; set; }
}
