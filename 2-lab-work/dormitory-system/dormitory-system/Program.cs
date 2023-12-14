using dormitory_system.Models;
using Npgsql;

// TODO: change to user secret mb?
string connectionString = "connection-string";
await using NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);



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
