namespace Parse_MS_to_PostSQL.Models;

public class Persons
{
    public int Id { get; set; }
    public string Department { get; set; } = string.Empty;
    public string Qualification { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public DateTime Bithday { get; set; }
    public string Country { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Home { get; set; } = string.Empty;
}

