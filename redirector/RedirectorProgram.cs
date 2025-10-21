using System.Diagnostics;

class RedirectorProgram
{
    static void Main(string[] args)
    {
        string? processPath = Environment.ProcessPath;
        if (processPath == null)
        {
            Console.WriteLine("FATAL ERROR: .exe path of redirector could not be found.\nPress any key to exit.");
            Console.ReadKey();
            Process.GetCurrentProcess().Kill();
        }

        string configPath = Path.ChangeExtension(processPath!, ".cfg");
        
        using (StreamReader reader = new StreamReader(configPath))
        {
            string pathToOpen = reader.ReadLine()!.Trim(); // JIC trim.
            
            Process.Start(pathToOpen, args);
        }
    }
}