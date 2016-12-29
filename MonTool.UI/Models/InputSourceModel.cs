using MonTool.Logic.DomainModels;
using MonTool.Logic.Enums;
using MonTool.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonTool.UI.Models
{
    class InputSourceModel
    {

        private uint inputSource;
        private IVCPFeatureLogic vcpFeatureLogic;
        private Monitor monitor;

        public InputSourceModel(uint inputSource, Monitor monitor, IVCPFeatureLogic vcpFeatureLogic)
        {
            this.inputSource = inputSource;
            this.vcpFeatureLogic = vcpFeatureLogic;
            this.monitor = monitor;
            ChangeInputSourceCommand = new ChangeInputSourceCommand(this);
        }


        public string Name
        {
            get
            {
                switch (inputSource)
                {
                    case (uint)InputSource.ANALOG_VIDEO1:
                        return "VGA1";

                    case (uint)InputSource.ANALOG_VIDEO2:
                        return "VGA2";

                    case (uint)InputSource.DISPLAY_PORT1:
                        return "DP1";

                    case (uint)InputSource.DISPLAY_PORT2:
                        return "DP2";

                    case (uint)InputSource.DVI1:
                        return "DVI1";

                    case (uint)InputSource.DVI2:
                        return "DVI2";

                    case (uint)InputSource.HDMI1:
                        return "HDMI1";

                    case (uint)InputSource.HDMI2:
                        return "HDMI2";

                    default:
                        return "UNKNOWN";
                }

            }
        }

        public ChangeInputSourceCommand ChangeInputSourceCommand { get; }

        public void SetThisAsInputSource()
        {
            vcpFeatureLogic.SetVCPFeature(monitor, VCPFeature.INPUT_SOURCE, inputSource);
        }

    }

    class ChangeInputSourceCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private InputSourceModel inputSourceModel;

        public ChangeInputSourceCommand(InputSourceModel inputSourceModel)

        {
            this.inputSourceModel = inputSourceModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            inputSourceModel.SetThisAsInputSource();
        }
    }

}
