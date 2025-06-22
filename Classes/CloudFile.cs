using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// CloudFile.cs
namespace VKR
{
    public class CloudFile
    {
        public string Id { get; set; }            // ID в облаке
        public string Name { get; set; }          // Имя
        public bool IsFolder { get; set; }      // Папка?
        public string Path { get; set; }
        public long Size { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
