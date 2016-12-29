using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonTool.Logic.Enums
{

    [Flags]
    public enum StoreRestoreSettings
    {
        STORE = 0x01,
        RESTORE = 0x02
    }
}
