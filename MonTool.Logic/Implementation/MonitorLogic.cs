using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonTool.Logic.Win32;
using System.Runtime.InteropServices;
using MonitorTest;
using MonTool.Logic.DomainModels;
using MonTool.Logic.Interfaces;

namespace MonTool.Logic.Implementation
{
    public class MonitorLogic : IMonitorLogic
    {


        private ICapabilitiesLogic capabilitiesLogic;

        public MonitorLogic(ICapabilitiesLogic capabilitiesLogic)
        {
            this.capabilitiesLogic = capabilitiesLogic;
        }

        public IEnumerable<Monitor> GetAll()
        {


            IList<Monitor> physicalMonitors = new List<Monitor>();
            MonitorEnumProc callback = (IntPtr hDesktop, IntPtr hdc, ref Rect r, int d) =>
            {

                uint physicalMonitorsCount = 0;
                PHYSICAL_MONITOR[] physicalMonitorArray;

                if (!GetNumberOfPhysicalMonitorsFromHMONITOR(hDesktop, ref physicalMonitorsCount))
                {
                    throw new Exception("Cannot get monitor count!");
                }

                physicalMonitorArray = new PHYSICAL_MONITOR[physicalMonitorsCount];


                if (!GetPhysicalMonitorsFromHMONITOR(hDesktop, physicalMonitorsCount, physicalMonitorArray))
                {
                    throw new Exception("Cannot get phisical monitor handle!");
                }



                Monitor m = new Monitor { PhysicalMonitor = physicalMonitorArray.First() };
                m=capabilitiesLogic.GetMonitorCapabilities(m);
                physicalMonitors.Add(m);
              
                return true;
            };

            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, callback, 0);


            return physicalMonitors;
        }

  




        [DllImport("dxva2.dll", EntryPoint = "GetNumberOfPhysicalMonitorsFromHMONITOR")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetNumberOfPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, ref uint pdwNumberOfPhysicalMonitors);

        [DllImport("dxva2.dll", EntryPoint = "GetPhysicalMonitorsFromHMONITOR")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, uint dwPhysicalMonitorArraySize, [Out] PHYSICAL_MONITOR[] pPhysicalMonitorArray);

        [DllImport("user32")]
        private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lpRect, MonitorEnumProc callback, int dwData);

        private delegate bool MonitorEnumProc(IntPtr hDesktop, IntPtr hdc, ref Rect pRect, int dwData);



    }
}
