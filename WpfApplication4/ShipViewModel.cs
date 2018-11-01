using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace WpfApplication4
{
    class ShipViewModel:BindableBase
    {
        static int counter = 0;
        private Boat boat;
        private int currentPart;
        //string forwrite;
        static private Dictionary<String, ImageSource> ShipParts;
        private ImageSource part;
        static ShipViewModel()
        {
            //var res = Application.Current.Resources;
            try
            {

           
            var bak = new BitmapImage(new Uri("pack://application:,,,/WpfApplication4;component/Resources/ship-bak.jpg",UriKind.RelativeOrAbsolute));
            var end = new BitmapImage(new Uri("pack://application:,,,/WpfApplication4;component/Resources/ship-end.jpg", UriKind.RelativeOrAbsolute));
            var clear = new BitmapImage(new Uri("pack://application:,,,/WpfApplication4;component/Resources/clear.jpg", UriKind.RelativeOrAbsolute));
           
                ShipParts = new Dictionary<string, ImageSource>();

            //this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/myapp;component/Images/icon.png")));
            ShipParts.Add("bak", bak);
            ShipParts.Add("end", end);
            ShipParts.Add("clear",clear);
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            }
        public ShipViewModel()
        {
           //
            ++counter;
        }
        
        public ShipViewModel(Boat boat, int index)
        {
            part = ShipParts["bak"];
            this.boat = boat;
            this.currentPart = index;
            //forwrite = index.ToString();
        }
        internal void Refresh()
        {
            OnPropertyChanged("Part");
        }
        //private ImageSource CurrentPart()
        //{

        //    switch (currentPart)
        //    {
        //        case 1:
        //            imageSource = (ImageSource);
        //            break;
        //        case 2:
        //            break;
        //        case 3:
        //            break;
        //        case 4:
        //            break;
        //        default:
        //            break;
        //    }
        //}
        public ImageSource Part
        {
            get 
            {
//                return boat == null ?ShipParts["clear"]: part;  
                return ShipParts["end"];
                //return forwrite;
            }
        }
        public Boat Boat
        {
            set
            {
                 boat=value;

                 OnPropertyChanged("Part");
            }
            get
            {
                return boat;
            }
        }

        

        public Int32 IndexOfPart
        {
            set
            {
                currentPart = value;
            }
        }
        //public override string ToString()
        //{
        //    return forwrite;
        //}
        
    }
}
