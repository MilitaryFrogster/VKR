using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKR
{
    public static class PathUtils
    {
        public static string FormatPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return "";

            if (path.StartsWith("Компьютер"))
                return $"Компьютер / {System.IO.Path.GetFileName(path)}";

            if (path.StartsWith("Облако"))
                return $"Облако / {System.IO.Path.GetFileName(path)}";

            return path; // fallback
        }
    }
}

