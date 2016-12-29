using MonTool.UI.Views.MainPageView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MonTool.UI
{
    class MainWindowViewModel : BaseModel
    {

        private Page currentPage;

        public MainWindowViewModel()
        {
            CurrentPage = new MainPage();
        }


        public Page CurrentPage { get { return currentPage; } set { currentPage = value; OnPropertyChanged(nameof(CurrentPage)); } }



    }
}
