using System;

namespace UpVer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var settings = new Settings(args);
                var updater = new VersionUpdater(settings);

                var changes = updater.Process();

                if (settings.Read)
                {
                    Console.WriteLine("Current version: " + changes.Version(x => x.From));
                }
                else
                {
                    Console.WriteLine("Bumped version from " + changes.Version(x => x.From) + " to " + changes.Version(x => x.To));
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
            }
        }
    }
}
