using MonTool.Logic.DomainModels;
using MonTool.Logic.Enums;
using MonTool.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonTool.UI.Models
{
    public class VCPFeatureModel : BaseModel
    {

        private VCPFeature vcpFeature;
        private Monitor monitor;
        private IVCPFeatureLogic vcpFeatureLogic;
        private ICapabilitiesLogic capabilitiesLogic;

        private bool isCapable;
        private VCPFeatureValue vcpFeatureValue = null;

        public VCPFeatureModel(VCPFeature vcpFeature, Monitor monitor, IVCPFeatureLogic vcpFeatureLogic, ICapabilitiesLogic capabilitiesLogic)
        {
            this.vcpFeature = vcpFeature;
            this.monitor = monitor;
            this.vcpFeatureLogic = vcpFeatureLogic;
            this.capabilitiesLogic = capabilitiesLogic;

            isCapable = capabilitiesLogic.IsCapable(monitor, vcpFeature);
        }

        public uint CurrentValue
        {

            get
            {

                if (!isCapable)
                {
                    return 0;
                }

                if (vcpFeatureValue == null)
                {
                    GetVcpFeatureValue();
                }

                return vcpFeatureValue.CurrentValue;
            }

            set
            {
                if (isCapable)
                {
                    vcpFeatureValue.CurrentValue = value;

                    if (!vcpFeatureLogic.SetVCPFeature(monitor, vcpFeature, value))
                    {
                        GetVcpFeatureValue();
                    }
                    OnPropertyChanged(nameof(CurrentValue));
                }
            }
        }

        public uint MaximumValue
        {

            get
            {
                if (!isCapable)
                {
                    return 0;
                }

                if (vcpFeatureValue == null)
                {
                    GetVcpFeatureValue();
                }

                return vcpFeatureValue.MaximumValue;
            }

        }

        public bool IsCapable
        {
            get { return isCapable; }
        }


        public void Refresh()
        {
            GetVcpFeatureValue();
            OnPropertyChanged(nameof(CurrentValue));
        }

        public string Name
        {
            get
            {
                switch (vcpFeature)
                {
                    case VCPFeature.COLOR_PRESET:
                        return "Color Preset";
                    case VCPFeature.CONTRAST:
                        return "Contrast";
                    case VCPFeature.INPUT_SOURCE:
                        return "Input Source";
                    case VCPFeature.LUMINANCE:
                        return "Luminance";
                    case VCPFeature.RESTORE_FACTORY_COLOR_DEFAULTS:
                        return "Restore Color Defaults";
                    case VCPFeature.RESTORE_FACTORY_DEFAULTS:
                        return "Restore Factory Defaults";
                    case VCPFeature.RESTORE_FACTORY_LUMINANCE_DEFAULTS:
                        return "Restore Luminance Defaults";
                    case VCPFeature.SPEAKER_VOLUME:
                        return "Speaker Volume";
                    case VCPFeature.STORE_RESTORE_SETTINGS:
                        return "Store/Restore Settings";
                    case VCPFeature.VIDEO_BLACK_LEVEL_BLUE:
                        return "Blue Level";
                    case VCPFeature.VIDEO_BLACK_LEVEL_GREEN:
                        return "Green Level";
                    case VCPFeature.VIDEO_BLACK_LEVEL_RED:
                        return "Red Level";
                    case VCPFeature.VIDEO_GAIN_BLUE:
                        return "Gain Blue";
                    case VCPFeature.VIDEO_GAIN_GREEN:
                        return "Gain Green";
                    case VCPFeature.VIDEO_GAIN_RED:
                        return "Gain Red";
                    default:
                        return "Unknown";
                }

            }
        }

        private void GetVcpFeatureValue()
        {
            vcpFeatureValue = vcpFeatureLogic.GetVCPFeature(monitor, vcpFeature);
        }

    }
}
