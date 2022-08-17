#region Парсер Пользователей

// Получить всех пользователей из старой базы

/*var users = await MsSqlService.GetAllUsers();

foreach (var item in users)
{
    // Создать Роль
    var itemRole = await PostSqlService.AddRole(item.Role);

    Console.WriteLine("Роль созадана:" + itemRole);
    // Создать пользователя
    var itemUser = await PostSqlService.AddUsers(item);

    Console.WriteLine("Пользователь созадан:" + item.Id);

    // Создать Связь Роль-Пользователя
    await PostSqlService.AddUserRoles(itemRole, itemUser);

    Console.WriteLine("Связь Пользователь-Роль созадана:" + item.Id);
    //----------------------Логи-----------------------------//
    var logsUsers = await MsSqlService.GetLogsByUser(itemUser);

    foreach (var log in logsUsers)
    {
        await PostSqlService.AddLogsToUser(log);
        Console.WriteLine("Создание логов:" + log.TypeSql);
    }
    
}
*/

#endregion

#region Отделы

/*var departments = await MsSqlService.GetAllDepartments();

foreach (var item in departments)
{
    await PostSqlService.AddDepartment(item.Name);
    Console.WriteLine("Add Department");
}
*/
#endregion

#region Города и Регионы
/*var city = await MsSqlService.GetAllCity();

foreach (var item in city)
{
    await PostSqlService.AddCity(item);
    Console.WriteLine("Insert City");
}
*/
#endregion

#region Персоны
// Забираем все людей из старой базы
var persons = await MsSqlService.GetAllPersons();

foreach (var item in persons)
{
    var idPerson = await PostSqlService.AddPerson(item);
    Console.WriteLine($"Add Person: {idPerson}");

    var persWork = await MsSqlService.GetWorksToPerson(idPerson);

    if (persWork.Any())
    {
        foreach (var works in persWork)
        {
            var workAdress = await Exists.ExistsCity(works.WorkAdress);
            var freeWork = await Exists.ExistsFreeWork(works.FreeWork);
            var newPerson = await Exists.ExistsPerson(idPerson);
            works.IdPerson = newPerson;
            if(freeWork == 0) freeWork = 1;
            if(workAdress == 0) workAdress = 1;


            var itemPersonToWork = await PostSqlService.AddPersonToWorks(works, workAdress, freeWork);
            Console.WriteLine($"Add PersonToWork: {itemPersonToWork}");
            
           
        }
    }
}
#endregion

