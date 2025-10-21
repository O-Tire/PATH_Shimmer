class ShimmerProgram
{
    const string EXECUTABLE_EXTENSION = ".exe";
    
    static void Main(string[] args)
    {
        Console.WriteLine("--Welcome to path shimmer--");
        Console.WriteLine("Press 1 to add a PATH.");
        Console.WriteLine("Press 2 for quick guide.");
        
        char userInput = Console.ReadKey(true).KeyChar;

        if (userInput == '1')
        {
            // TODO: Add a PATH
            return;
        }

        if (userInput == '2')
        {
            // Show guide
            Console.WriteLine(
                $"\n\n1. Manually add the directory of this program (path_shimmer{EXECUTABLE_EXTENSION}) to PATH.\n" +
                $"2. After that run this program, press 1, then write the path of the " +
                $"{EXECUTABLE_EXTENSION} file which you wish to be registered in the system's search list.\n" +
                $"And that's it! Now you can add individual {EXECUTABLE_EXTENSION} programs (instead of whole directories) " +
                $"and keep your PATH list clean.\n" +
                $"*If you ever want to remove a program from the search list, go to the root directory of " +
                $"path_shimmer{EXECUTABLE_EXTENSION} and remove the {EXECUTABLE_EXTENSION} and .cfg files that are named after " +
                $"that program. (there must be only two files)");

            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadKey();
            return;
        }
        
        Console.WriteLine("Invalid key detected.");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}