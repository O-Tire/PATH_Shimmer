using System.Text;

class ShimmerProgram
{
    const string EXECUTABLE_EXTENSION = ".exe";
    
    static void Main(string[] args)
    {
        Console.WriteLine("--Welcome to path shimmer--");
        Console.WriteLine($"Press 1 to register an {EXECUTABLE_EXTENSION} to system's search list. (add to PATH)");
        Console.WriteLine("Press 2 for quick guide.");
        Console.WriteLine($"Press 3 to unregister an {EXECUTABLE_EXTENSION} from system's search list. (remove from PATH)");
        Console.WriteLine("Press 4 to print a list of all registered apps. (apps which are being redirected by PATH Shimmer)");
        
        char userInputNum = Console.ReadKey(true).KeyChar;

        if (userInputNum == '1')
        {
            InstantiateRedirector();
        }

        if (userInputNum == '2')
        {
            DisplayGuide();
        }

        if (userInputNum == '3')
        {
            RemoveRedirector();
        }

        if (userInputNum == '4')
        {
            PrintRegisteredApps();
        }
        
        Console.WriteLine("Invalid key detected.");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static void InstantiateRedirector()
    {
        Console.WriteLine($"\n\nWrite the path to the {EXECUTABLE_EXTENSION} which you want to registered:");
            
        string targetFile = Console.ReadLine()!.Trim();
        string rootDir = Path.GetDirectoryName(Environment.ProcessPath!)!;
        string fileToCopy = rootDir + $"/tools/redirector{EXECUTABLE_EXTENSION}";
        string fileToPaste = rootDir + "/" + Path.GetFileName(targetFile);
            
        File.Copy(fileToCopy, fileToPaste, true);
            
        // Add config.
        FileStream fs = File.Create(rootDir + "/" + Path.GetFileNameWithoutExtension(targetFile) + ".cfg");
        fs.Write(Encoding.UTF8.GetBytes(targetFile), 0, targetFile.Length);
        fs.Close();

        Console.WriteLine("File was successfully registered.\nPress any key to exit...");
        Console.ReadKey();
        Environment.Exit(0);
    }

    static void DisplayGuide()
    {
        Console.WriteLine(
            $"\n\nHow to use PATH Shimmer:\n" +
            $"Step 1: Manually add the root directory of this program (path_shimmer{EXECUTABLE_EXTENSION}) to PATH.\n" +
            $"Step 2: After that, run this program, press 1, then write the path of the " +
            $"{EXECUTABLE_EXTENSION} file which you wish to be registered in the system's search list. And that is it.\n" +
            $"Step 3: If you want to remove a program from the search list, press 3 instead. Then only write the name of the program" +
            $"and press enter. (e.g. my_program NOT my_program{EXECUTABLE_EXTENSION} NOR C:/Folder/my_program{EXECUTABLE_EXTENSION})");

        Console.WriteLine("\n\nPress any key to exit...");
        Console.ReadKey();
        Environment.Exit(0);
    }

    static void RemoveRedirector()
    {
        Console.WriteLine("\n\nPlease write the name of the program which you wish to be removed (without the extension):");
        string nameOfTarget = Console.ReadLine()!.Trim();
        string rootDir = Path.GetDirectoryName(Environment.ProcessPath!)!;

        if (nameOfTarget == "path_shimmer")
        {
            Console.WriteLine(
                "PATH Shimmer cannot be removed this way! Please consider deleting & unregistering the program " +
                "manually.");
            goto EndProgram;
        }

        try
        {
            File.Delete(rootDir + "/" + nameOfTarget + EXECUTABLE_EXTENSION);
            File.Delete(rootDir + "/" + nameOfTarget + ".cfg");
        }
        catch (Exception e)
        {
            Console.WriteLine("FATAL ERROR: The deletion process was unsuccessful. Full error message:");
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("File was successfully deleted.");
        }

        EndProgram:
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
        Environment.Exit(0);
    }

    static void PrintRegisteredApps()
    {
        string[] filePaths = Directory.GetFiles(Path.GetDirectoryName(Environment.ProcessPath!)!);

        Console.WriteLine("Found items:");
        foreach (string filePath in filePaths)
        {
            if (!filePath.EndsWith(EXECUTABLE_EXTENSION) || filePath.EndsWith("path_shimmer" + EXECUTABLE_EXTENSION)) continue;

            Console.WriteLine("-" + Path.GetFileNameWithoutExtension(filePath));
        }
        
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
        Environment.Exit(0);
    }
}