namespace Parse_MS_to_PostSQL.Models;
public class PersonWorks
{
    public int IdPerson { get; set; }
    //public int IdFreeWork { get; set; }
    public string? FreeWork { get; set; }
    public string? WorkAdress { get; set; }
    //public int IdWorkAdress { get; set; }
    public string NameStateOrg { get; set; } = string.Empty;
    public string NameOrg { get; set; } = string.Empty;
    public string Post { get; set; } = string.Empty;
    public string PedSpecialty { get; set; } = string.Empty;
    public string NumCertificate { get; set; } = string.Empty;
    public string NumReference { get; set; } = string.Empty;
    public string VerificationArrival { get; set; } = string.Empty;

    public string VerificationYear1 { get; set; } = string.Empty;
    public string VerificationYear2 { get; set; } = string.Empty;
    public string VerificationYear3 { get; set; } = string.Empty;

    public DateTime DatetContract { get; set; } = DateTime.MinValue;
    public string Commentary1 { get; set; } = string.Empty;
    public DateTime DateCrt { get; set; } = DateTime.Now;

    public string Educational { get; set; } = string.Empty;
    public string CityOrg { get; set; } = string.Empty;
    public string InfoOrg { get; set; } = string.Empty;

    public string Decree { get; set; } = string.Empty;
    public string InfoVerificationArrival { get; set; } = string.Empty;
    public string Commentary2 { get; set; } = string.Empty;
    public string PlanDop { get; set; } = string.Empty;
    public string PhoneOrg { get; set; } = string.Empty;
    public string PhoneCity { get; set; } = string.Empty;
    public string MailWork { get; set; } = string.Empty;
}

