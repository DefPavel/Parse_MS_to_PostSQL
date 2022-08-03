#region Парсер Пользователей
// Получить всех пользователей из старой базы
var users = await MsSqlService.GetAllUsers();

foreach (var item in users)
{
    // Создать Роль
    var itemRole = await PostSqlService.AddRole(item.Role);
    // Создать пользователя
    var itemUser = await PostSqlService.AddUsers(item);
    // Создать Связь Роль-Пользователя
    await PostSqlService.AddUserRoles(itemRole, itemUser);

    //----------------------Логи-----------------------------//
    var logsUsers = await MsSqlService.GetLogsByUser(itemUser);

    foreach (var log in logsUsers)
    {
        await PostSqlService.AddLogsToUser(log);
    }
}

#endregion