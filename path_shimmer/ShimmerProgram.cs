using System.Text;

class ShimmerProgram
{
    const string EXECUTABLE_EXTENSION = ".exe";
    
    static void Main(string[] args)
    {
        Console.WriteLine("--Welcome to path shimmer--");
        Console.WriteLine($"Press 1 to register a {EXECUTABLE_EXTENSION} to system's search list. (add to PATH)");
        Console.WriteLine("Press 2 for quick guide.");
        
        char userInputNum = Console.ReadKey(true).KeyChar;

        if (userInputNum == '1')
        {
            // Instantiate redirector.
            Console.WriteLine($"\n\nWrite the path to the {EXECUTABLE_EXTENSION} which you want to registered.");
            
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
            return;
        }

        if (userInputNum == '2')
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