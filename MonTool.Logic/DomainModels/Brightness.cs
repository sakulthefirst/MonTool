using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonTool.Logic.DomainModels
{
    public class Brightness
    {
        public uint MinValue { get; set; }
        public uint MaxValue { get; set; }
        public uint CurrentValue { get; set; }
    }
}
