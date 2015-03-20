using System;

namespace UpVer
{
    public class Changes
    {
        public Change Major { get; set; }
        public Change Minor { get; set; }
        public Change Patch { get; set; }

        public static Change NoChange(int val)
        {
            return new Change {From = val, To = val};
        }

        public string Version(Func<Change, int> which)
        {
            return string.Format("{0}.{1}.{2}", which(Major), which(Minor), which(Patch));
        }
    }
}