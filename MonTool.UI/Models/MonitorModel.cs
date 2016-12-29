using MonTool.Logic.DomainModels;
using MonTool.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonTool.UI.Models
{
    class MonitorModel : BaseModel
    {


        Monitor monitor;
        ICapabilitiesLogic capabilitesLogic;
        IVCPFeatureLogic vcpFeatureLogic;
        private IList<VCPFeatureModel> refreshFeatures = new List<VCPFeatureModel>();



        ObservableCollection<InputSourceModel> inputSources = new ObservableCollection<InputSourceModel>();
        InputSourceModel selectedInputSource;

        ObservableCollection<ColorPresetModel> colorPresets = new ObservableCollection<ColorPresetModel>();
        ColorPresetModel selectedColorPreset;

        #region Properties
        public VCPFeatureModel LuminanceFeature { get; }
        public VCPFeatureModel ContrastFeature { get; }

        //Gain
        public VCPFeatureModel VideoGainRedFeature { get; }
        public VCPFeatureModel VideoGainGreenFeature { get; }
        public VCPFeatureModel VideoGainBlueFeature { get; }

        //Blacklevel
        public VCPFeatureModel VideoBlackLevelRedFeature { get; }
        public VCPFeatureModel VideoBlackLevelGreenFeature { get; }
        public VCPFeatureModel VideoBlackLevelBlueFeature { get; }

        public VCPFeatureModel InputSourceFeature { get; }
        public VCPFeatureModel ColorPresetFeature { get; }

        public string Name { get { return monitor.Model; } }

        public InputSourceModel SelectedInputSource { get { return selectedInputSource; } set { selectedInputSource = value; OnPropertyChanged(nameof(SelectedInputSource)); } }
        public ObservableCollection<InputSourceModel> InputSources { get { return inputSources; } set { inputSources = value; OnPropertyChanged(nameof(InputSources)); } }

        public ColorPresetModel SelectedColorPreset
        {
            get { return selectedColorPreset; }
            set
            {
                var isInit = selectedColorPreset == null;
                selectedColorPreset = value;
                OnPropertyChanged(nameof(SelectedColorPreset));
                selectedColorPreset.SetThisColorPreset();

                if (!isInit)
                {
                    Refresh();
                }
            }
        }
        public ObservableCollection<ColorPresetModel> ColorPresets { get { return colorPresets; } set { colorPresets = value; OnPropertyChanged(nameof(ColorPresets)); } }

        public bool IsCapableVideoBlackLevelFeature
        {
            get
            {
                return (!(!VideoBlackLevelBlueFeature.IsCapable && !VideoBlackLevelGreenFeature.IsCapable && !VideoBlackLevelRedFeature.IsCapable));
            }
        }

        public bool IsCapableVideoGainFeature
        {
            get
            {
                return (!(!VideoGainBlueFeature.IsCapable && !VideoGainGreenFeature.IsCapable && !VideoGainRedFeature.IsCapable));
            }
        }

        #endregion



        public MonitorModel(Monitor monitor, IVCPFeatureLogic vcpFeatureLogic, ICapabilitiesLogic capabilitesLogic)
        {
            this.monitor = monitor;
            this.capabilitesLogic = capabilitesLogic;
            this.vcpFeatureLogic = vcpFeatureLogic;

            //INIT VCCP FEATURES
            LuminanceFeature = new VCPFeatureModel(Logic.Enums.VCPFeature.LUMINANCE, monitor, vcpFeatureLogic, capabilitesLogic);
            ContrastFeature = new VCPFeatureModel(Logic.Enums.VCPFeature.CONTRAST, monitor, vcpFeatureLogic, capabilitesLogic);

            VideoGainRedFeature = new VCPFeatureModel(Logic.Enums.VCPFeature.VIDEO_GAIN_RED, monitor, vcpFeatureLogic, capabilitesLogic);
            VideoGainGreenFeature = new VCPFeatureModel(Logic.Enums.VCPFeature.VIDEO_GAIN_GREEN, monitor, vcpFeatureLogic, capabilitesLogic);
            VideoGainBlueFeature = new VCPFeatureModel(Logic.Enums.VCPFeature.VIDEO_GAIN_BLUE, monitor, vcpFeatureLogic, capabilitesLogic);

            VideoBlackLevelRedFeature = new VCPFeatureModel(Logic.Enums.VCPFeature.VIDEO_BLACK_LEVEL_RED, monitor, vcpFeatureLogic, capabilitesLogic);
            VideoBlackLevelGreenFeature = new VCPFeatureModel(Logic.Enums.VCPFeature.VIDEO_BLACK_LEVEL_GREEN, monitor, vcpFeatureLogic, capabilitesLogic);
            VideoBlackLevelBlueFeature = new VCPFeatureModel(Logic.Enums.VCPFeature.VIDEO_BLACK_LEVEL_BLUE, monitor, vcpFeatureLogic, capabilitesLogic);


            InputSourceFeature = new VCPFeatureModel(Logic.Enums.VCPFeature.INPUT_SOURCE, monitor, vcpFeatureLogic, capabilitesLogic);
            ColorPresetFeature = new VCPFeatureModel(Logic.Enums.VCPFeature.COLOR_PRESET, monitor, vcpFeatureLogic, capabilitesLogic);


            //ADD USER PREST BOUND FEATURES

            refreshFeatures.Add(VideoGainBlueFeature);
            refreshFeatures.Add(VideoGainRedFeature);
            refreshFeatures.Add(VideoGainGreenFeature);


            if (InputSourceFeature.IsCapable)
            {
                monitor.InputSources.ToList().ForEach(input =>
                {
                    inputSources.Add(new InputSourceModel(input, monitor, vcpFeatureLogic));
                    if (InputSourceFeature.CurrentValue == input)
                    {
                        selectedInputSource = inputSources.Last();
                    }
                });
            }



            if (ColorPresetFeature.IsCapable)
            {
                monitor.ColorPresets.ToList().ForEach(colorPreset =>
                {
                    colorPresets.Add(new ColorPresetModel(colorPreset, monitor, vcpFeatureLogic));
                    if (ColorPresetFeature.CurrentValue == colorPreset)
                    {
                        selectedColorPreset = colorPresets.Last();
                    }
                });
            }

        }


        private void Refresh()
        {
            refreshFeatures.ToList().ForEach(feature =>
            {

                feature.Refresh();
            });
        }

    }
}
