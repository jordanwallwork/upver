using System;
using System.IO;

namespace UpVer
{
    public class Settings
    {
        public bool Major { get; set; }
        public bool Minor { get; set; }
        public bool Patch { get; set; }
        public bool Read { get; set; }
        public string File { get; set; }

        private bool _nextArgIsFilename;

        public Settings(string[] args)
        {
            foreach (var arg in args)
            {
                if (_nextArgIsFilename) File = Path.GetFullPath(arg); 
                else if (arg.StartsWith("-f")) { _nextArgIsFilename = true; continue; }
                else if (arg.StartsWith("--ma")) Major = true;
                else if (arg.StartsWith("--mi")) Minor = true;
                else if (arg.StartsWith("--p")) Patch = true;
                else if (arg.StartsWith("--r")) Read = true;
                else throw new Exception("Invalid argument: " + arg);

                Reset();
            }
        }

        private void Reset()
        {
            _nextArgIsFilename = false;
        }
    }
}
