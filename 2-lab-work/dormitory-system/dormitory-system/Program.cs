using dormitory_system.Models;
using dormitory_system.Repositories;
using Npgsql;

// TODO: change to user secret mb?
string connectionString = "there should be a connection string";
await using NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);

Faculty faculty = new Faculty
{
    Id = 3,
    Abbreviation = "FFS",
    Name = "For fuck's sake"
};

StudentRepository studentRepository = new StudentRepository(dataSource);
FacultyRepository facultyRepository = new FacultyRepository(dataSource);

// await facultyRepository.Add(faculty);
// Thread.Sleep(1000);
await facultyRepository.Delete(faculty);



// TODO: mb think of something better?
MenuOptions mainMenuOptions = new()
{
    Options = new List<string>
    {
        "Add new student",
        "Delete student",
        "Find student"
    }
};

string? GetInput(string prompt)
{
    try
    {
        Console.WriteLine($"Enter {prompt}: ");
        string input = Console.ReadLine()!;
        return input;
    }
    catch(Exception)
    {
        Console.WriteLine("ERROR: couldn't get input");
        return null;
    }
}
