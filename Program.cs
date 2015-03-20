using System;

namespace UpVer
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new Settings(args);
            var updater = new VersionUpdater(settings);
            var changes = updater.Process();

            Console.WriteLine("Bumped version from " + changes.Version(x => x.From) + " to " + changes.Version(x => x.To));
        }
    }
}
