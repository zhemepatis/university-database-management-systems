using dormitory_system.Models;

namespace dormitory_system.Services;

public class InputService
{
    public InputService()
    {
    }

    public string GetInput(string prompt)
    {
        Console.Write($"Enter {prompt}: ");
        return Console.ReadLine()!;
    }
    
    public int GetOption()
    {
        try
        {
            return int.Parse(GetInput("option"));
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public Dormitory GetDormitory()
    {
        string address = GetInput("dormitory address");
        string managerName = GetInput("dormitory manager name");
        string managerSurname = GetInput("dormitory manager surname");
        string managerPhoneNumber = GetInput("dormitory manager phone number");

        Dormitory dormitory = new Dormitory
        {
            Address = address,
            ManagerName = managerName,
            ManagerSurname = managerSurname,
            ManagerPhoneNumber = managerPhoneNumber
        };

        return dormitory;
    }

    public Faculty GetFaculty()
    {
        string abbreviation = GetInput("faculty abbreviation");
        string name = GetInput("faculty name");

        Faculty faculty = new Faculty
        {
            Abbreviation = abbreviation,
            Name = name
        };

        return faculty;
    }

    public Student GetStudent()
    {
        int studentId = int.Parse(GetInput("student id"));
        string name = GetInput("student name");
        string surname = GetInput("student surname");
        string email = GetInput("student email");
        string? address = GetInput("student address");
        string? phoneNumber = GetInput("student phone number");
        int facultyId = int.Parse(GetInput("faculty id"));

        Student student = new Student
        {
            StudentId = studentId,
            Name = name,
            Surname = surname,
            Email = email,
            Address = address == "\n" ? null : address,
            PhoneNumber = phoneNumber == "\n" ? null : phoneNumber,
            FacultyId = facultyId
        };

        return student;
    }
}