using System.IO;

namespace UpVer
{
    public class Settings
    {
        public bool Major { get; set; }
        public bool Minor { get; set; }
        public bool Patch { get; set; }
        public string File { get; set; }

        private bool _setFile;

        public Settings(string[] args)
        {
            foreach (var arg in args)
            {
                if (arg.StartsWith("-f")) { _setFile = true; continue; }
                if (_setFile) { File = Path.GetFullPath(arg); Reset(); continue; }

                if (arg.StartsWith("--maj")) { Major = true; Reset(); continue; }
                if (arg.StartsWith("--min")) { Minor = true; Reset(); continue; }
                if (arg.StartsWith("--p")) { Patch = true; Reset(); continue; }
            }
        }

        private void Reset()
        {
            _setFile = false;
        }
    }
}
