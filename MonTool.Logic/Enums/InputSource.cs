using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonTool.Logic.Enums
{


    [Flags]
    public enum InputSource
    {
        ANALOG_VIDEO1 = 0x01,
        ANALOG_VIDEO2 = 0x02,
        DVI1 = 0x03,
        DVI2 = 0x04,
        HDMI1 = 0x11,
        HDMI2 = 0x12,
        DISPLAY_PORT1 = 0x0F,
        DISPLAY_PORT2 = 0x10
    }
}
