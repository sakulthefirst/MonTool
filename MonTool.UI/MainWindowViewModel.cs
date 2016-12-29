using MonTool.UI.Views.MainPageView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MonTool.UI
{
    class MainWindowViewModel : BaseModel
    {

        private Page currentPage;

        public ICommand ApplicationCloseCommand { get; } = new ApplicationCloseCommand();

        public MainWindowViewModel()
        {
            CurrentPage = new MainPage();
        }


        public Page CurrentPage { get { return currentPage; } set { currentPage = value; OnPropertyChanged(nameof(CurrentPage)); } }
    }

    public class ApplicationCloseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return Application.Current != null && Application.Current.MainWindow != null;
        }

        public void Execute(object parameter)
        {
            Application.Current.MainWindow.Close();
        }
    }
}
