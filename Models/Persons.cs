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
    public DateTime Bithday { get; set; } = DateTime.MinValue;
    public string Country { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Home { get; set; } = string.Empty;
    public string Flat { get; set; } = string.Empty;
    public string TrainingDirection { get;set; } = string.Empty;
    public string Profile { get;set;} = string.Empty;
    public string AcademLeave { get; set; } = string.Empty;
    public string Dismiss { get; set; } = string.Empty;
    public int YearIssue { get; set; } = 0;

    public string Phone1 { get; set; } = string.Empty;
    public string Phone2 { get; set; } = string.Empty;
    public string Phone3 { get; set; } = string.Empty;
    public string Cipher { get; set; } = string.Empty;
    public string Mail { get; set; } = string.Empty;
    public string PlanningEnterMag { get; set; } = string.Empty;
    public string EnteredMag { get; set; } = string.Empty;
    public string ChangeSurname { get; set; } = string.Empty;
    public string Othere { get; set; } = string.Empty;

}

