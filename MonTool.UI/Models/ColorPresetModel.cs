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
    class ColorPresetModel : BaseModel
    {

        private uint colorPreset;
        Monitor monitor;
        IVCPFeatureLogic vcpFeatureLogic;

        public ColorPresetModel(uint colorPreset, Monitor monitor, IVCPFeatureLogic vcpFeatureLogic)
        {

            this.colorPreset = colorPreset;
            this.monitor = monitor;
            this.vcpFeatureLogic = vcpFeatureLogic;
        }


        public string Name
        {
            get
            {


                switch (colorPreset)
                {
                    case (uint)ColorPreset.DISPLAY_NATIVE:
                        return "Native";

                    case (uint)ColorPreset.SRGB:
                        return "sRGB";

                    case (uint)ColorPreset.USER1:
                        return "User 1";

                    case (uint)ColorPreset.USER2:
                        return "User 2";

                    case (uint)ColorPreset.USER3:
                        return "User 3";

                    case (uint)ColorPreset._10000K:
                        return "10000° K";

                    case (uint)ColorPreset._11500K:
                        return "11500° K";

                    case (uint)ColorPreset._4000K:
                        return "4000° K";

                    case (uint)ColorPreset._5000K:
                        return "5000° K";

                    case (uint)ColorPreset._6500K:
                        return "6500° K";

                    case (uint)ColorPreset._7500K:
                        return "7500° K";

                    case (uint)ColorPreset._8200K:
                        return "8200° K";

                    case (uint)ColorPreset._9300K:
                        return "9300° K";

                    default:
                        return "UNKOWN";

                }


            }
        }

        public void SetThisColorPreset()
        {
            this.vcpFeatureLogic.SetVCPFeature(monitor, VCPFeature.COLOR_PRESET, colorPreset);
        }

        public bool IsUserColorPreset
        {
            get
            {
                return (this.colorPreset == (uint)ColorPreset.USER1 || this.colorPreset == (uint)ColorPreset.USER2 || this.colorPreset == (uint)ColorPreset.USER3);
            }
        }







    }
}
