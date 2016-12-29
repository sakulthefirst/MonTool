using MonTool.Logic.Configuration;
using MonTool.Logic.DomainModels;
using MonTool.Logic.Enums;
using MonTool.Logic.Helper;
using MonTool.Logic.Interfaces;
using MonTool.Logic.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonTool.Logic.Implementation
{
    public class CapabilitiesLogic : ICapabilitiesLogic
    {


        enum VcpState
        {
            DEFAULT,
            INPUT_SOURCE,
            COLOR_PRESET,
            SKIP
        }

        enum ParseState
        {
            DEFAULT,
            VCP,
            PROT,
            TYPE,
            CMDS,
            MODEL,
            SKIP,
            MSWHQL,
            ASSET_EEP,
            MCCS_VER,
            ERROR
        }


        public Monitor GetMonitorCapabilities(Monitor monitor)
        {
            char[] pszASCIICapabilitiesString;
            uint dwCapabilitiesStringLengthInCharacters = 0;
            var result = GetCapabilitiesStringLength(monitor.PhysicalMonitor.hPhysicalMonitor, ref dwCapabilitiesStringLengthInCharacters);

            if (!result)
            {
                System.Threading.Thread.Sleep(MonToolConfiguration.REQUEST_TIMEOUT);
                GetCapabilitiesStringLength(monitor.PhysicalMonitor.hPhysicalMonitor, ref dwCapabilitiesStringLengthInCharacters);
            }



            pszASCIICapabilitiesString = new char[dwCapabilitiesStringLengthInCharacters];

            result = CapabilitiesRequestAndCapabilitiesReply(monitor.PhysicalMonitor.hPhysicalMonitor, pszASCIICapabilitiesString, dwCapabilitiesStringLengthInCharacters);

            if (!result)
            {
                System.Threading.Thread.Sleep(MonToolConfiguration.REQUEST_TIMEOUT);
                CapabilitiesRequestAndCapabilitiesReply(monitor.PhysicalMonitor.hPhysicalMonitor, pszASCIICapabilitiesString, dwCapabilitiesStringLengthInCharacters);
            }


            return ParseVcp(new string(pszASCIICapabilitiesString), monitor);


        }

        private Monitor ParseVcp(string capabilityString, Monitor monitor)
        {

            capabilityString = PrepareCapabilityString(capabilityString);
            IList<uint> capabilities = new List<uint>();
            IList<uint> inputSources = new List<uint>();
            IList<uint> colorPresets = new List<uint>();


            ParseState parseState = ParseState.DEFAULT;
            VcpState vcpState = VcpState.DEFAULT;
            uint? vcpCode = null;
            string model = "";
            foreach (string str in capabilityString.Split(' '))
            {


                ParseState beforeCheckParseState = parseState;

                switch (str)
                {
                    case "vcp":
                        parseState = ParseState.VCP;
                        break;
                    case "prot":
                        parseState = ParseState.PROT;
                        break;
                    case "type":
                        parseState = ParseState.TYPE;
                        break;
                    case "model":
                        parseState = ParseState.MODEL;
                        break;
                    case "cmds":
                        parseState = ParseState.CMDS;
                        break;
                    case "mswhql":
                        parseState = ParseState.MSWHQL;
                        break;
                    case "asset_eep":
                        parseState = ParseState.ASSET_EEP;
                        break;
                    case "mccs_ver":
                        parseState = ParseState.MCCS_VER;
                        break;
                }


                if (parseState == beforeCheckParseState)
                    switch (parseState)
                    {

                        case ParseState.VCP:


                            if (str == "(" && vcpCode != null)
                            {
                                switch (vcpCode.Value)
                                {
                                    case (int)VCPFeature.INPUT_SOURCE:
                                        vcpState = VcpState.INPUT_SOURCE;
                                        break;
                                    case (int)VCPFeature.COLOR_PRESET:
                                        vcpState = VcpState.COLOR_PRESET;
                                        break;
                                    default:
                                        vcpState = VcpState.SKIP;
                                        break;
                                }
                            }
                            else if (str == ")")
                            {
                                vcpState = VcpState.DEFAULT;
                            }
                            else if (str != ")" && str != "(" && str != "")
                            {
                                try
                                {
                                    vcpCode = (uint)HexToDecConverter.Convert(str);

                                    switch (vcpState)
                                    {

                                        case VcpState.DEFAULT:
                                            capabilities.Add(vcpCode.Value);
                                            break;
                                        case VcpState.INPUT_SOURCE:
                                            inputSources.Add(vcpCode.Value);
                                            break;
                                        case VcpState.COLOR_PRESET:
                                            colorPresets.Add(vcpCode.Value);
                                            break;
                                        case VcpState.SKIP:
                                            break;
                                    }


                                }
                                catch (FormatException ex)
                                {
                                    parseState = ParseState.ERROR;
                                }
                            }

                            break;

                        case ParseState.MODEL:
                            if (str != "(" && str != ")")
                            {
                                model += str;
                            }

                            break;

                        default:
                            break;
                    }

            }

            monitor.Capabilitys = capabilities;
            monitor.InputSources = inputSources;
            monitor.Model = model;
            monitor.ColorPresets = colorPresets;



            return monitor;
        }

        private string PrepareCapabilityString(string capabilityString)
        {

            capabilityString = capabilityString.Replace(" (", "(");
            capabilityString = capabilityString.Replace("( ", "(");
            capabilityString = capabilityString.Replace("(", " ( ");
            capabilityString = capabilityString.Replace(" )", ")");
            capabilityString = capabilityString.Replace(")", " ) ");

            return capabilityString;

        }








        [DllImport("dxva2.dll", EntryPoint = "GetCapabilitiesStringLength")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCapabilitiesStringLength(IntPtr hMonitor, ref uint pdwCapabilitiesStringLengthInCharacters);

        [DllImport("dxva2.dll", EntryPoint = "CapabilitiesRequestAndCapabilitiesReply")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CapabilitiesRequestAndCapabilitiesReply(IntPtr hMonitor, [Out] char[] pszASCIICapabilitiesString, uint dwCapabilitiesStringLengthInCharacters);

        public bool IsCapable(Monitor monitor, VCPFeature feature)
        {
            return monitor.Capabilitys.Contains((uint)feature);
        }
    }
}
