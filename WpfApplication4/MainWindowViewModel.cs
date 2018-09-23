using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApplication4
{
    class MainWindowViewModel:BindableBase
    {
        List<String> buttonList;
        List<String> userShips;
        public MainWindowViewModel()
        {
            buttonList = new List<String>();
            userShips = new List<String>();
            for (int i = 0; i < 81; ++i)
            {
                buttonList.Add(i.ToString());
                userShips.Add(i.ToString());
            }
        }
        public List<String> Collection
        {
            get 
            {
                return buttonList;
            }
        }
        public List<String> GameCollection
        {
            get
            {
                return userShips;
            }
        }
        public ICommand ClickCommand
        {
            get
            {
                return new RelayCommand((i) => { global::System.Windows.Forms.MessageBox.Show("Test"); });
            }
        }
    }
}
