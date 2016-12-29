using MonTool.Logic.DomainModels;
using MonTool.Logic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonTool.Logic.Interfaces
{
    public interface IVCPFeatureLogic
    {
        VCPFeatureValue GetVCPFeature(Monitor monitor, VCPFeature vcpFeature);

        bool SetVCPFeature(Monitor monitor, VCPFeature vcpFeature, uint newValue);

    }
}
