using MonTool.Logic.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonTool.Logic.DomainModels
{
    public class Monitor
    {

        public PHYSICAL_MONITOR PhysicalMonitor { get; set; }
        public string Model { get; set; }
        public IList<uint> Capabilitys { get; set; }
        public IList<uint> InputSources { get; set; }
        public IList<uint> ColorPresets { get; set; }








    }
}
