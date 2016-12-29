using MonTool.Logic.DomainModels;
using MonTool.Logic.Enums;
using MonTool.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MonTool.Logic.Configuration;

namespace MonTool.Logic.Implementation
{
    public class VCPFeatureLogic : IVCPFeatureLogic
    {

        public VCPFeatureValue GetVCPFeature(Monitor monitor, VCPFeature vcpFeature)
        {
            uint pvct = 0;
            uint pdwCurrentValue = 0;
            uint pdwMaximumValue = 0;
            var result = GetVCPFeatureAndVCPFeatureReply(monitor.PhysicalMonitor.hPhysicalMonitor, (byte)vcpFeature, ref pvct, ref pdwCurrentValue, ref pdwMaximumValue);

            //Bugfix for slow Monitors
            if (!result)
            {
                System.Threading.Thread.Sleep(MonToolConfiguration.REQUEST_TIMEOUT);
                GetVCPFeatureAndVCPFeatureReply(monitor.PhysicalMonitor.hPhysicalMonitor, (byte)vcpFeature, ref pvct, ref pdwCurrentValue, ref pdwMaximumValue);
            }

            return new VCPFeatureValue { CurrentValue = pdwCurrentValue, MaximumValue = pdwMaximumValue };

        }

        public bool SetVCPFeature(Monitor monitor, VCPFeature vcpFeature, uint newValue)
        {
            var result = SetVCPFeature(monitor.PhysicalMonitor.hPhysicalMonitor, (byte)vcpFeature, newValue);

            //Bugfix for slow Monitors
            if (!result)
            {
                System.Threading.Thread.Sleep(MonToolConfiguration.REQUEST_TIMEOUT);
                result = SetVCPFeature(monitor.PhysicalMonitor.hPhysicalMonitor, (byte)vcpFeature, newValue);
            }
            return result;
        }

        [DllImport("dxva2.dll", EntryPoint = "GetVCPFeatureAndVCPFeatureReply")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetVCPFeatureAndVCPFeatureReply(IntPtr handle, byte bVCPCode, ref uint pvct, ref uint pdwCurrentValue, ref uint pdwMaximumValue);

        [DllImport("dxva2.dll", EntryPoint = "SetVCPFeature")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetVCPFeature(IntPtr handle, byte bVCPCode, uint dwNewValue);


    }
}
