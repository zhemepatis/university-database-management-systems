using dormitory_system.DataTransferObjects;
using dormitory_system.Models;
using dormitory_system.Repositories;
using dormitory_system.Repositories.Interfaces;
using dormitory_system.Utilities;

namespace dormitory_system.Services;

public class MainService
{ 
    private readonly IFacultyRepository _facultyRepository;
    private readonly FacultyDormitoryRelationService _facultyDormitoryRelationService;
    // private readonly ResidenceService _residenceService;
    
    private readonly InputService _inputService;

    public MainService(IFacultyRepository facultyRepository, InputService inputService, 
        FacultyDormitoryRelationService facultyDormitoryRelationService)
    {
        _facultyRepository = facultyRepository;
        
        _inputService = inputService;
        _facultyDormitoryRelationService = facultyDormitoryRelationService;
        
        // _residenceService = residenceService;
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
            "Move student",
            "Evict student",
            "Find room residents",
            "Exit"
        };

        while (true)
        {
            Console.WriteLine("Available options:");
            ListUtilities.PrintNumberedList(menuOptions);

            int option = _inputService.GetOption();
            await FireFunction(option);
            
            PauseExecution();
        }
    }

    private async Task  FireFunction(int option)
    {
        switch ((MenuOptions) option)
        {
            case MenuOptions.ShowCurrentResidents:
                Console.WriteLine("showing current residents");
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
                break;
            case MenuOptions.AccommodateStudent:
                Console.WriteLine("showing dormitories");
                break;
            case MenuOptions.MoveStudent:
                break;
            case MenuOptions.EvictStudent:
                break;
            case MenuOptions.FindRoomResidents:
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
        Console.WriteLine("FORMAT: , abbreviation, name");
        ListUtilities.PrintList(list);
    }

    private async Task RegisterDormitory()
    {
        Console.WriteLine();
        Console.WriteLine("--- NEW DORMITORY REGISTRATION ---");
        Dormitory dormitory = _inputService.GetDormitory();
        int facultyId = int.Parse(_inputService.GetInput("faculty id"));

        await _facultyDormitoryRelationService.AddDormitory(dormitory, facultyId);

        await ShowDormitories();
    }
}