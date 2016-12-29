using MonTool.Logic.DomainModels;
using MonTool.Logic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonTool.Logic.Interfaces
{
    public interface ICapabilitiesLogic
    {

        Monitor GetMonitorCapabilities(Monitor monitor);

        bool IsCapable(Monitor monitor, VCPFeature feature);

    }
}
