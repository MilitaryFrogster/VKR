using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKR
{
    public class ResolvedPath
    {
        public ICloudService Service { get; set; }  // null, если это компьютер
        public bool IsComputer { get; set; }
        public string Path { get; set; }

        public ResolvedPath(ICloudService service, bool isComputer, string path)
        {
            Service = service;
            IsComputer = isComputer;
            Path = path;
        }
    }
}
