using System.Data.SqlClient;
namespace Parse_MS_to_PostSQL.Services;
public static class MsSqlService
{
    private const string _connectionString = "Server=192.168.250.25;Database=graduates;user=Pavel;password=~Pss~JrY";
    // Получить всех users из старой базы
    public static async Task<IEnumerable<Users>> GetAllUsers()
    {
        var array = new List<Users>();
        await using var con = new SqlConnection(_connectionString);
        con.Open();
        const string sql = " select u.login , u.password , u.fio , r2.name from usersRole as UR "
                           + " inner join role r2 on UR.idRole = r2.id "
                           + " inner join users u on UR.idUser = u.id ";

        await using var cmd = new SqlCommand(sql, con);
        await using var rdr = await cmd.ExecuteReaderAsync();
        while (await rdr.ReadAsync())
        {
            array.Add(new Users
            {
                Login = rdr.GetString(0),
                Password = rdr.GetString(1),
                FullName = rdr.GetString(2),
                Role = rdr.GetString(3),
            });
        }
        return array;
    }

    public static async Task<IEnumerable<Persons>> GetAllPersons()
    {
        var array = new List<Persons>();
        await using var con = new SqlConnection(_connectionString);
        con.Open();
        const string sql = " select " +
                           " d.nameDepartment, " +      //0
                           " tQ.nameQualification, " +  //1
                           " surname, " +               //2
                           " name               , " +   //3
                           " patronymic          ," +   //4
                           " gender              ," +   //5
                           " birthday           ," +    //6
                           " addressCountry      ," +   //7
                           " addressRegion       ," +   //8
                           " addressArea         ," +   //9
                           " addressCity         ," +   //10
                           " addressStreet       ," +   //11
                           " addressHome        ," +    //12
                           " addressFlat        ," +    //13
                           " trainingDirection   ," +   //14
                           " profile             ," +   //15
                           " academLeave         ," +   //16
                           " dismiss           ," +     //17
                           " yearIssue          ," +    //18
                           " phone1            ," +     //19 
                           " phone2              ," +   //20
                           " cipher             ," +    //21
                           " mail               ," +    //22
                           " planningEnterMag   ," +    //23
                           " enteredMag          ," +   //24
                           " changeSurname       ," +   //25
                           " othere              ," +   //26
                           " phone3 " +                 //27
                           " from persons as per " +
                           " inner join department d on per.idTypeDepartment = d.id " +
                           " inner join  typeQualification tQ on per.idTypeQualification = tQ.id ";

        await using var cmd = new SqlCommand(sql, con);
        await using var rdr = await cmd.ExecuteReaderAsync();
        while (await rdr.ReadAsync())
        {
            array.Add(new Persons
            {
                Department = rdr.GetString(0),
                Qualification = rdr.GetString(1),
                FirstName = rdr.GetString(2),
                Name = rdr.GetString(3),
                LastName = rdr.GetString(4),
                Gender = rdr.GetString(5),
                Bithday = rdr.GetDateTime(6),
                Country = rdr[7] != DBNull.Value ? rdr.GetString(7) : "",
                Region = rdr[8] != DBNull.Value ? rdr.GetString(8) : "",
                City = rdr[10] != DBNull.Value ? rdr.GetString(10) : "",
                Area = rdr[9] != DBNull.Value ? rdr.GetString(9) : "",
                Street = rdr[11] != DBNull.Value ? rdr.GetString(11) : "",
                Home = rdr[12] != DBNull.Value ? rdr.GetString(12) : "",
                Flat = rdr[13] != DBNull.Value ? rdr.GetString(13) : "",
                TrainingDirection = rdr[14] != DBNull.Value ? rdr.GetString(14) : "",
                Profile = rdr[15] != DBNull.Value ? rdr.GetString(15) : "",
                AcademLeave = rdr[16] != DBNull.Value ? rdr.GetString(16) : "",
                Dismiss = rdr[17] != DBNull.Value ? rdr.GetString(17) : "",
                YearIssue = rdr[18] != DBNull.Value ? rdr.GetInt16(18) : 0,
                Phone1 = rdr[19] != DBNull.Value ? rdr.GetString(19) : "",
                Phone2 = rdr[20] != DBNull.Value ? rdr.GetString(20) : "",
                Cipher = rdr[21] != DBNull.Value ? rdr.GetString(21) : "",
                Mail = rdr[22] != DBNull.Value ? rdr.GetString(22) : "",
                PlanningEnterMag = rdr[23] != DBNull.Value ? rdr.GetString(23) : "",
                EnteredMag = rdr[24] != DBNull.Value ? rdr.GetString(24) : "",
                ChangeSurname = rdr[25] != DBNull.Value ? rdr.GetString(25) : "",
                Othere = rdr[26] != DBNull.Value ? rdr.GetString(26) : "",
                Phone3 = rdr[27] != DBNull.Value ? rdr.GetString(27) : "",

            });
        }
        return array;
    }
    public static async Task<IEnumerable<Departments>> GetAllDepartments()
    {
        var array = new List<Departments>();
        await using var con = new SqlConnection(_connectionString);
        con.Open();
        const string sql = " select * from department ";

        await using var cmd = new SqlCommand(sql, con);
        await using var rdr = await cmd.ExecuteReaderAsync();
        while (await rdr.ReadAsync())
        {
            array.Add(new Departments
            {
                Name = rdr.GetString(1),
            });
        }
        return array;
    }

    public static async Task<IEnumerable<City>> GetAllCity()
    {
        var array = new List<City>();
        await using var con = new SqlConnection(_connectionString);
        con.Open();
        const string sql = " select wc.nameCity , wA.nameArea from workCity as wc " +
            " inner join workArea wA on wc.idWorkArea = wA.id ";

        await using var cmd = new SqlCommand(sql, con);
        await using var rdr = await cmd.ExecuteReaderAsync();
        while (await rdr.ReadAsync())
        {
            array.Add(new City
            {
                NameCity = rdr.GetString(0),
                NameAria = rdr.GetString(1),
            });
        }
        return array;
    }

    public static async Task<IEnumerable<Logs>> GetLogsByUser(int idUser)
    {
        var array = new List<Logs>();
        await using var con = new SqlConnection(_connectionString);
        con.Open();
        var sql = " select l.typeSql , l.nameTable , l.fielTable , l.oldValue , l.newValue , l.dateCrt "
                           + " from logs as l "
                           + " where l.idUser = " + idUser;

        await using var cmd = new SqlCommand(sql, con);
        await using var rdr = await cmd.ExecuteReaderAsync();
        while (await rdr.ReadAsync())
        {
            array.Add(new Logs
            {
                TypeSql = rdr[0] != DBNull.Value ? rdr.GetString(0) : "Не указано",
                NameTable = rdr[1] != DBNull.Value ? rdr.GetString(1) : "Не указано",
                FieldTable = rdr[2] != DBNull.Value ?  rdr.GetString(2) : "",
                OldValue = rdr[3] != DBNull.Value ? rdr.GetString(3) : "",
                NewValue = rdr[4] != DBNull.Value ?  rdr.GetString(4) : "",
                DateCreate = rdr.GetDateTime(5),
                IdUser = idUser,
            });
        }
        return array;
    }
}

