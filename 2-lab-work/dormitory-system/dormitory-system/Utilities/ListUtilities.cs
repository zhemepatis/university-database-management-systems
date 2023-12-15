namespace dormitory_system.Utilities;

public class ListUtilities
{
    public static void PrintNumberedList<T>(List<T> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("list is empty.");
            return;
        }
        
        int i = 1;
        foreach (T item in list)
        {
            Console.WriteLine($"{i}. {item}");
            ++i;
        }
    }
    
    public static void PrintList<T>(List<T> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("list is empty.");
            return;
        }

        foreach (T item in list)
        {
            Console.WriteLine(item);
        }
    }
}