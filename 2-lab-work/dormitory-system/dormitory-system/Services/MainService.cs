using dormitory_system.DataTransferObjects;
using dormitory_system.Models;
using dormitory_system.Repositories.Interfaces;
using dormitory_system.Utilities;

namespace dormitory_system.Services;

public class MainService
{ 
    private readonly IFacultyRepository _facultyRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IResidenceRepository _residenceRepository;
    private readonly IRoomRepository _roomRepository;
    
    private readonly FacultyDormitoryRelationService _facultyDormitoryRelationService;
    private readonly ResidenceService _residenceService;
    
    private readonly InputService _inputService;

    public MainService(IFacultyRepository facultyRepository, InputService inputService, 
        FacultyDormitoryRelationService facultyDormitoryRelationService, ResidenceService residenceService, 
        IStudentRepository studentRepository, IResidenceRepository residenceRepository, IRoomRepository roomRepository)
    {
        _facultyRepository = facultyRepository;
        _studentRepository = studentRepository;
        _residenceRepository = residenceRepository;
        _roomRepository = roomRepository;

        _facultyDormitoryRelationService = facultyDormitoryRelationService;
        _residenceService = residenceService;
        
        _inputService = inputService;
    }

    public async Task Run()
    {
        List<string> menuOptions = new List<string>
        {
            "Show current residents",
            "Show faculty list",
            "Show dormitory list",
            "Register dormitory",
            "Add student to list",
            "Accommodate student",
            "Evict student",
            "Remove student from list",
            "Find room residents",
            "Exit"
        };

        while (true)
        {
            try
            {
                Console.WriteLine("Available options:");
                ListUtilities.PrintNumberedList(menuOptions);

                int option = _inputService.GetOption();
                await FireFunction(option);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: operation unsuccessful");
                Console.WriteLine(ex.Message);
                Console.WriteLine();
            }
            
            PauseExecution();
        }
    }

    private async Task  FireFunction(int option)
    {
        switch ((MenuOptions) option)
        {
            case MenuOptions.ShowCurrentResidents:
                await ShowCurrentResidents();
                break;
            case MenuOptions.ShowFaculties:
                await ShowFaculties();
                break;
            case MenuOptions.ShowDormitories:
                await ShowDormitories();
                break;
            case MenuOptions.RegisterDormitory:
                await RegisterDormitory();
                break;
            case MenuOptions.AddStudent:
                await AddStudent();
                break;
            case MenuOptions.AccommodateStudent:
                await AccommodateStudent();
                break;
            case MenuOptions.EvictStudent:
                await EvictStudent();
                break;
            case MenuOptions.RemoveStudent:
                await DeleteStudent();
                break;
            case MenuOptions.FindRoomResidents:
                await FindRoomResidents();
                break;
            case MenuOptions.Exit:
                Console.WriteLine("Exiting application...");
                Environment.Exit(0);
                return;
            default:
                Console.WriteLine("ERROR: there is no such option");
                break;
        }
    }
    
    private void PauseExecution()
    {
        Console.WriteLine("Press enter to continue...");
        Console.ReadKey();
        Console.WriteLine();
    }
    
    private async Task ShowCurrentResidents()
    {
        List<CurrentResidentDto> list = (await _residenceService.GetCurrentResidents()).ToList();

        Console.WriteLine();
        Console.WriteLine("--- CURRENT RESIDENTS LIST ---");
        Console.WriteLine("FORMAT: id, name, surname, student faculty abbreviation, dormitory id, dormitory faculty abbreviation, dormitory address, room id, room number");
        ListUtilities.PrintList(list);
    }

    private async Task ShowFaculties()
    {
        List<Faculty> list = (await _facultyRepository.GetAll()).ToList();

        Console.WriteLine();
        Console.WriteLine("--- FACULTY LIST ---");
        Console.WriteLine("FORMAT: id, abbreviation, name");
        ListUtilities.PrintList(list);
    }

    private async Task ShowDormitories()
    {
        List<FacultyDormitoryRelationDto> list = (await _facultyDormitoryRelationService.FacultyDormitoryList()).ToList();
        
        Console.WriteLine();
        Console.WriteLine("--- DORMITORY LIST ---");
        Console.WriteLine("FORMAT: faculty id, faculty abbreviation, faculty name, dormitory id, dormitory address, manager name, manager surname, manager phone number");
        ListUtilities.PrintList(list);
    }

    private async Task ShowStudents()
    {
        List<Student> list = (await _studentRepository.GetAll()).ToList();
        
        Console.WriteLine();
        Console.WriteLine("--- STUDENT LIST ---");
        Console.WriteLine("FORMAT: id, student id, name, surname, email, address, phone number, faculty id");
        ListUtilities.PrintList(list); 
    }

    private async Task ShowRooms()
    {
        List<Room> list = (await _roomRepository.GetAll()).ToList();
        
        Console.WriteLine();
        Console.WriteLine("--- ROOM LIST ---");
        Console.WriteLine("FORMAT: id, dormitory id, number, capacity, availability");
        ListUtilities.PrintList(list); 
    }

    private async Task RegisterDormitory()
    {
        await ShowFaculties();
        Console.WriteLine();
        Console.WriteLine("--- NEW DORMITORY REGISTRATION ---");
        Dormitory dormitory = _inputService.GetDormitory();
        int facultyId = int.Parse(_inputService.GetInput("faculty id"));

        await _facultyDormitoryRelationService.AddDormitory(dormitory, facultyId);

        await ShowDormitories();
    }
    
    private async Task AddStudent()
    {
        Console.WriteLine();
        Console.WriteLine("--- ADDING STUDENT ---");
        Student student = _inputService.GetStudent();

        await _studentRepository.Add(student);

        await ShowStudents();
    }

    private async Task AccommodateStudent()
    {
        await ShowStudents();
        await ShowRooms();

        Console.WriteLine();
        Console.WriteLine("--- ACCOMMODATING STUDENT ---");
        Residence residence = _inputService.GetResidence();
        await _residenceRepository.Add(residence);
        
        await ShowCurrentResidents();
    }

    private async Task EvictStudent()
    {
        await ShowCurrentResidents();
        
        Console.WriteLine();
        Console.WriteLine("--- EVICTING STUDENT ---");
        int studentId = int.Parse(_inputService.GetInput("student id"));

        await _residenceService.EvictResident(studentId);

        await ShowCurrentResidents();
    }

    private async Task DeleteStudent()
    {
        await ShowStudents();

        Console.WriteLine();
        Console.WriteLine("--- REMOVING STUDENT ---");
        
        int studentId = int.Parse(_inputService.GetInput("student id"));
        await _studentRepository.Delete(studentId);

        await ShowCurrentResidents();
    }

    private async Task FindRoomResidents()
    {
        await ShowRooms();
        int roomId = int.Parse(_inputService.GetInput("room id"));
        
        Console.WriteLine();
        Console.WriteLine("--- ROOM RESIDENTS LIST ---");
        
        List<CurrentResidentDto> list = (await _residenceService.FindRoomResidents(roomId)).ToList();
        ListUtilities.PrintList(list);
    }
}