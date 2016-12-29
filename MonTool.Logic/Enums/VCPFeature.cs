using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonTool.Logic.Enums
{

    [Flags]
    public enum VCPFeature
    {
        //Preset Operations VCP Codes  Value != 0
        RESTORE_FACTORY_DEFAULTS = 0x04,
        RESTORE_FACTORY_LUMINANCE_DEFAULTS=0x05,
        RESTORE_FACTORY_COLOR_DEFAULTS = 0x08,
        STORE_RESTORE_SETTINGS=0xB0,      //01 Store Restore 02

        //Image Adjustment
        LUMINANCE = 0x10,
        CONTRAST=0x12,
        COLOR_PRESET = 0x14,
        VIDEO_GAIN_RED=0x16,
        VIDEO_GAIN_GREEN=0x18,
        VIDEO_GAIN_BLUE=0x1A,
        VIDEO_BLACK_LEVEL_RED=0x6C,
        VIDEO_BLACK_LEVEL_GREEN = 0x6E,
        VIDEO_BLACK_LEVEL_BLUE = 0x70,

        // Miscellaneous Functions 
        INPUT_SOURCE = 0x60,

        // Audio Function
        SPEAKER_VOLUME = 0x62
    }
}
