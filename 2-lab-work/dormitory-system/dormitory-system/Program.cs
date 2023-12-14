using dormitory_system.Models;
using dormitory_system.Repositories;
using dormitory_system.Services;
using Npgsql;

// TODO: change to user secret mb?
string connectionString = "Host=localhost;Username=postgres;Password=5up4dup45t4r;Database=studentu";
await using NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);

Dormitory dormitory = new Dormitory
{
    Address = "Sauletekio g. 12",
    ManagerName = "Vytatautas",
    ManagerSurname = "Alijosius",
    ManagerPhoneNumber = "+3706"
};

DormitoryRepository dormitoryRepository = new DormitoryRepository(dataSource);
FacultyDormitoryRelationRepository fdrRepo = new FacultyDormitoryRelationRepository(dataSource);

FacultyDormitoryRelationService repository = new FacultyDormitoryRelationService(dataSource, dormitoryRepository, fdrRepo);
await repository.AddDormitory(dormitory, 1);

// // TODO: mb think of something better?
// MenuOptions mainMenuOptions = new()
// {
//     Options = new List<string>
//     {
//         "Add new student",
//         "Delete student",
//         "Find student"
//     }
// };
//
// string? GetInput(string prompt)
// {
//     try
//     {
//         Console.WriteLine($"Enter {prompt}: ");
//         string input = Console.ReadLine()!;
//         return input;
//     }
//     catch(Exception)
//     {
//         Console.WriteLine("ERROR: couldn't get input");
//         return null;
//     }
// }
