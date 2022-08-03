﻿namespace Parse_MS_to_PostSQL.Services.Postgres;
public static class Exists
{
    // Проверка соединения
    public static async Task<bool> TestConnection()
    {
        await using NpgsqlConnection conn = new(ConfigurationManager.ConnectionStrings["postgres"].ConnectionString);
        await conn.OpenAsync();
        if (conn.State == System.Data.ConnectionState.Open)
            return true;
        else return false;
    }
    // Проверка пользователя
    public static async Task<int> ExistsUsers(string username)
    {
        await using var con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["postgres"].ConnectionString);
        con.Open();
        string sql = $@" select id from public.""users"" where login = '{username}' LIMIT 1 ";
        await using var cmd = new NpgsqlCommand(sql, con);

        await using var rdr = cmd.ExecuteReader();

        return rdr.Read() ? rdr.GetInt32(0) : 0;

    }
    // Проверка Роли
    public static async Task<int> ExistsRoles(string rolename)
    {
        await using var con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["postgres"].ConnectionString);
        con.Open();
        string sql = $@" select id from public.""role"" where name = '{rolename}' LIMIT 1 ";
        await using var cmd = new NpgsqlCommand(sql, con);

        await using var rdr = cmd.ExecuteReader();

        return rdr.Read() ? rdr.GetInt32(0) : 0;

    }
     public static async Task<int> ExistsUserRoles(int idRole , int idUser)
    {
        await using var con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["postgres"].ConnectionString);
        con.Open();
        string sql = $@" select id from public.""usersrole"" where idrole = {idRole} and iduser = {idUser} LIMIT 1 ";
        await using var cmd = new NpgsqlCommand(sql, con);

        await using var rdr = cmd.ExecuteReader();

        return rdr.Read() ? rdr.GetInt32(0) : 0;

    }
}
