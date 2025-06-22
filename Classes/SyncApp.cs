using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKR
{
    public static class SyncApp
    {
        public static SyncTaskManager TaskManager { get; } = new SyncTaskManager();
    }
}

