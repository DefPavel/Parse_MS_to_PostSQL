namespace Parse_MS_to_PostSQL.Models;
public class Logs
{
    public int IdUser { get; set; }
    public string TypeSql { get; set; } = string.Empty;
    public string NameTable { get; set; } = string.Empty;
    public string FieldTable { get; set; } = string.Empty;
    public string OldValue { get; set; } = string.Empty;
    public string NewValue { get; set; } = string.Empty;
    public DateTime DateCreate { get; set; } = DateTime.Now;
}

