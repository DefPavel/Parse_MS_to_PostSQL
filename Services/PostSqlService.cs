namespace Parse_MS_to_PostSQL.Services;
public static class PostSqlService
{
    #region Пользователи
    public static async Task<int> AddUsers(Users user)
    {
        await using var con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["postgres"].ConnectionString);
        con.Open();

        var exists = await Exists.ExistsUsers(user.Login);
        // Если не существует такого пользователя
        if (exists == 0)
        {
            const string sql = @"INSERT INTO public.""users""(login , fio , password) values(@login, @fio ,@password) RETURNING id";
            await using var cmd = new NpgsqlCommand(sql, con);

            cmd.Parameters.AddWithValue("login", user.Login);
            cmd.Parameters.AddWithValue("fio", user.FullName);
            cmd.Parameters.AddWithValue("password", user.Password);

            await cmd.PrepareAsync();

            var row = (int)cmd.ExecuteScalar();

            return row;
        }
        // В противном случае вернуть id
        return exists;
    }
    // Занести логи пользователей
    public static async Task<int> AddLogsToUser(Logs logs)
    {
        await using var con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["postgres"].ConnectionString);
        con.Open();

        const string sql = @"INSERT INTO public.""logs""(iduser , typesql , nametable, fieltable, oldvalue, newvalue ,datecrt) values(@iduser, @typesql ,@nametable , @fieltable, @oldvalue, @newvalue, @datecrt) RETURNING id";
        await using var cmd = new NpgsqlCommand(sql, con);

        cmd.Parameters.AddWithValue("iduser", logs.IdUser);
        cmd.Parameters.AddWithValue("typesql", logs.TypeSql);
        cmd.Parameters.AddWithValue("nametable", logs.NameTable);
        cmd.Parameters.AddWithValue("fieltable", logs.FieldTable);
        cmd.Parameters.AddWithValue("oldvalue", logs.OldValue);
        cmd.Parameters.AddWithValue("newvalue", logs.NewValue);
        cmd.Parameters.AddWithValue("datecrt", logs.DateCreate);

        await cmd.PrepareAsync();

        var row = (int)cmd.ExecuteScalar();

        return row;
    }
    public static async Task<int> AddRole(string nameRole)
    {
        await using var con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["postgres"].ConnectionString);
        con.Open();

        var exists = await Exists.ExistsRoles(nameRole);
        // Если не существует такой роли
        if (exists == 0)
        {
            const string sql = @"INSERT INTO public.""role""(name) values(@name) RETURNING id";
            await using var cmd = new NpgsqlCommand(sql, con);

            cmd.Parameters.AddWithValue("name", nameRole);

            await cmd.PrepareAsync();

            var row = (int)cmd.ExecuteScalar();

            return row;
        }
        // В противном случае вернуть id
        return exists;
    }
    public static async Task<int> AddUserRoles(int idRole, int idUser)
    {
        await using var con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["postgres"].ConnectionString);
        con.Open();

        var exists = await Exists.ExistsUserRoles(idRole, idUser);
        // Если не существует такой роли
        if (exists == 0)
        {
            const string sql = @"INSERT INTO public.""usersrole""(iduser , idrole) values(@iduser, @idrole) RETURNING id";
            await using var cmd = new NpgsqlCommand(sql, con);

            cmd.Parameters.AddWithValue("iduser", idUser);
            cmd.Parameters.AddWithValue("idrole", idRole);

            await cmd.PrepareAsync();

            var row = (int)cmd.ExecuteScalar();

            return row;
        }
        return exists; 
    }



    #endregion

}

