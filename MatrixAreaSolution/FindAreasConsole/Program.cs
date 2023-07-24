using Services;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter matrix, seperate columns with , and rows with ; :");

        string input = Console.ReadLine()!;

        MatrixAreaFinderService areaFinder = new MatrixAreaFinderService(input);
        int areas = areaFinder.Calculate();

        Console.WriteLine(areas);

    }

}