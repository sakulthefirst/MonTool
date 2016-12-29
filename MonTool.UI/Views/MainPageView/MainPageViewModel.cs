using MonTool.Logic.Implementation;
using MonTool.Logic.Interfaces;
using MonTool.UI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonTool.UI.Views.MainPageView
{
    class MainPageViewModel : BaseModel
    {

        ICapabilitiesLogic capabilitiesLogic = new CapabilitiesLogic();
        IVCPFeatureLogic vcpFeatureLogic = new VCPFeatureLogic();

        IMonitorLogic monitorLogic;
        private ObservableCollection<MonitorModel> monitors = new ObservableCollection<MonitorModel>();
        private MonitorModel selectedMonitor; 

        public MainPageViewModel()
        {
            monitorLogic = new MonitorLogic(capabilitiesLogic);

            monitorLogic.GetAll().ToList().ForEach(m =>
            {
                monitors.Add(new MonitorModel(m,vcpFeatureLogic, capabilitiesLogic));
            });

            SelectedMonitor = monitors.First();

        }


        public ObservableCollection<MonitorModel> Monitors { get { return monitors; } set { monitors = value; OnPropertyChanged(nameof(Monitors)); } }
        public MonitorModel SelectedMonitor { get { return selectedMonitor; } set { selectedMonitor = value; OnPropertyChanged(nameof(SelectedMonitor)); } }

     

    }
}
