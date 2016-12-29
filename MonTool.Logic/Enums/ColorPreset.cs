using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonTool.Logic.Enums
{
    [Flags]
    public enum ColorPreset
    {
        SRGB = 0x01,
        DISPLAY_NATIVE = 0x02,
        _4000K = 0x03,
        _5000K = 0x04,
        _6500K = 0x05,
        _7500K = 0x06,
        _8200K = 0x07,
        _9300K = 0x08,
        _10000K = 0x09,
        _11500K = 0x0A,
        USER1 = 0x0B,
        USER2 = 0x0C,
        USER3 = 0x0D

    }
}
