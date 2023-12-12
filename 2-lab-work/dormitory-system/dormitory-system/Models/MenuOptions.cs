namespace dormitory_system.Models;

public class MenuOptions
{
    public IEnumerable<string> Options { get; init; }

    public void Print()
    {
        int i = 1;
        foreach (string option in Options)
        {
            Console.WriteLine($"{i}.{option}");
            ++i;
        }
    }
}