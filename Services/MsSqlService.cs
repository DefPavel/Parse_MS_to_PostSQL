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
        await using var rdr = cmd.ExecuteReader();
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

    public static async Task<IEnumerable<Logs>> GetLogsByUser(int idUser)
    {
        var array = new List<Logs>();
        await using var con = new SqlConnection(_connectionString);
        con.Open();
        string sql = " select l.typeSql , l.nameTable , l.fielTable , l.oldValue , l.newValue , l.dateCrt "
                           + " from logs as l "
                           + " where l.idUser = " + idUser;

        await using var cmd = new SqlCommand(sql, con);
        await using var rdr = cmd.ExecuteReader();
        while (await rdr.ReadAsync())
        {
            array.Add(new Logs
            {
                TypeSql = rdr.GetString(0),
                NameTable = rdr.GetString(1),
                FieldTable = rdr.GetString(2),
                OldValue = rdr.GetString(3),
                NewValue = rdr.GetString(4),
                DateCreate = rdr.GetDateTime(5),
            });
        }
        return array;
    }
}

