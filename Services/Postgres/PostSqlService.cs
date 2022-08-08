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
        if (exists != 0) return exists;
        const string sql = @"INSERT INTO public.""users""(login , fio , password) values(@login, @fio ,@password) RETURNING id";
        await using var cmd = new NpgsqlCommand(sql, con);

        cmd.Parameters.AddWithValue("login", user.Login);
        cmd.Parameters.AddWithValue("fio", user.FullName);
        cmd.Parameters.AddWithValue("password", user.Password);

        await cmd.PrepareAsync();

        var row = (int)(cmd.ExecuteScalar() ?? throw new InvalidOperationException());

        return row;
        // В противном случае вернуть id
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

        var row = (int)(cmd.ExecuteScalar() ?? throw new InvalidOperationException());

        return row;
    }
    public static async Task<int> AddRole(string nameRole)
    {
        await using var con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["postgres"].ConnectionString);
        con.Open();

        var exists = await Exists.ExistsRoles(nameRole);
        // Если не существует такой роли
        if (exists != 0) return exists;
        const string sql = @"INSERT INTO public.""role""(name) values(@name) RETURNING id";
        await using var cmd = new NpgsqlCommand(sql, con);

        cmd.Parameters.AddWithValue("name", nameRole);

        await cmd.PrepareAsync();

        var row = (int)(cmd.ExecuteScalar() ?? throw new InvalidOperationException());

        return row;
        // В противном случае вернуть id
    }

    public static async Task<int> AddDepartment(string nameDepartment)
    {
        await using var con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["postgres"].ConnectionString);
        con.Open();

        var exists = await Exists.ExistsDepartment(nameDepartment);
        // Если не существует такой роли
        if (exists != 0) return exists;
        const string sql = @"INSERT INTO public.""department""(nameDepartment) values(@name) RETURNING id";
        await using var cmd = new NpgsqlCommand(sql, con);

        cmd.Parameters.AddWithValue("name", nameDepartment);

        await cmd.PrepareAsync();

        var row = (int)(cmd.ExecuteScalar() ?? throw new InvalidOperationException());
        return row;
    }


    public static async Task<int> AddCity(City city)
    {
        await using var con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["postgres"].ConnectionString);
        con.Open();

        // Найти по имени
        var exists = await Exists.ExistsArea(city.NameAria);

        const string sql = @"INSERT INTO public.""workcity""(nameCity, idworkarea) values(@nameCity , @idworkarea) RETURNING id";
        await using var cmd = new NpgsqlCommand(sql, con);

        cmd.Parameters.AddWithValue("nameCity", city.NameCity);
        cmd.Parameters.AddWithValue("idworkarea", exists);

        await cmd.PrepareAsync();

        var row = (int)(cmd.ExecuteScalar() ?? throw new InvalidOperationException());
        return row;
    }

    public static async Task<int> AddPerson(Persons person)
    {
        await using var con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["postgres"].ConnectionString);
        con.Open();

        // Найти по имени
        var exists = await Exists.ExistsDepartment(person.Department);

        const string sql = @"INSERT INTO public.""persons""
            (
            idtypedepartment, 
            idtypequalification,
            surname,
            name,
            patronymic,
            gender,
            birthday,
            addresscountry,
            addressregion,
            addressarea,
            addresscity,
            addressstreet,
            addresshome,
            addressflat,
            trainingdirection,
            profile,
            academleave,
            dismiss,
            yearissue,
            phone1,
            phone2,
            cipher,
            mail,
            planningentermag,
            enteredmag,
            changesurname,
            othere,
            phone3,
            ) 
            values(
                    @idtypedepartment , 
                    @idtypequalification,
                    @surname,
                    @name,
                    @patronymic,
                    @gender,
                    @birthday,
                    @addresscountry,
                    @addressregion,
                    @addressarea,
                    @addresscity,
                    @addressstreet,
                    @addresshome,
                    @addressflat,
                    @trainingdirection,
                    @profile,
                    @academleave,
                    @dismiss,
                    @yearissue,
                    @phone1,
                    @phone2,
                    @cipher,
                    @mail,
                    @planningentermag,
                    @enteredmag,
                    @changesurname,
                    @othere,
                    @phone3

            ) RETURNING id";
        await using var cmd = new NpgsqlCommand(sql, con);

        cmd.Parameters.AddWithValue("idtypedepartment", exists);
        cmd.Parameters.AddWithValue("idtypequalification", person.Qualification == "бакалавр" ? 1 : 2);
        cmd.Parameters.AddWithValue("surname", person.FirstName);
        cmd.Parameters.AddWithValue("name", person.Name);
        cmd.Parameters.AddWithValue("patronymic", person.LastName);
        cmd.Parameters.AddWithValue("gender", person.Gender);

        cmd.Parameters.AddWithValue("addresscountry", person.Country);
        cmd.Parameters.AddWithValue("addressregion", person.Region);
        cmd.Parameters.AddWithValue("addressarea", person.Area);
        cmd.Parameters.AddWithValue("addresscity", person.City);
        cmd.Parameters.AddWithValue("addressstreet", person.Street);
        cmd.Parameters.AddWithValue("addresshome", person.Home);
        cmd.Parameters.AddWithValue("addressflat", person.Flat);

        cmd.Parameters.AddWithValue("trainingdirection", person.TrainingDirection);
        cmd.Parameters.AddWithValue("profile", person.Profile);
        cmd.Parameters.AddWithValue("academleave", person.AcademLeave);
        cmd.Parameters.AddWithValue("dismiss", person.Dismiss);
        cmd.Parameters.AddWithValue("yearissue", person.YearIssue);

        cmd.Parameters.AddWithValue("phone1", person.Phone1);
        cmd.Parameters.AddWithValue("phone2", person.Phone2);
        cmd.Parameters.AddWithValue("phone3", person.Phone3);

        cmd.Parameters.AddWithValue("cipher", person.Cipher);
        cmd.Parameters.AddWithValue("mail", person.Mail);
        cmd.Parameters.AddWithValue("planningentermag", person.PlanningEnterMag);
        cmd.Parameters.AddWithValue("enteredmag", person.EnteredMag);
        cmd.Parameters.AddWithValue("changesurname", person.ChangeSurname);
        cmd.Parameters.AddWithValue("othere", person.Othere);

        await cmd.PrepareAsync();

        var row = (int)(cmd.ExecuteScalar() ?? throw new InvalidOperationException());
        return row;
    }

    public static async Task<int> AddUserRoles(int idRole, int idUser)
    {
        await using var con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["postgres"].ConnectionString);
        con.Open();

        var exists = await Exists.ExistsUserRoles(idRole, idUser);
        // Если не существует такой роли
        if (exists != 0) return exists;
        const string sql = @"INSERT INTO public.""usersrole""(iduser , idrole) values(@iduser, @idrole) RETURNING id";
        await using var cmd = new NpgsqlCommand(sql, con);

        cmd.Parameters.AddWithValue("iduser", idUser);
        cmd.Parameters.AddWithValue("idrole", idRole);

        await cmd.PrepareAsync();

        var row = (int)(cmd.ExecuteScalar() ?? throw new InvalidOperationException());

        return row;
    }



    #endregion
}

