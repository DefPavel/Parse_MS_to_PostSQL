﻿namespace Parse_MS_to_PostSQL.Models;
public class Users
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

