using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Battleship.Models;
namespace Battleship.ViewModels
{
    class ShipViewModel:BindableBase
    {
        static int counter = 0;
        private Boat boat;
        private int currentPart;
        //string forwrite;
        static private Dictionary<ShipPart, ImageSource> ShipParts;
        private ImageSource part;
        static ShipViewModel()
        {
            //var res = Application.Current.Resources;
            try
            {

           
            var bak = new BitmapImage(new Uri("pack://application:,,,/Battleship;component/Resources/ship-bak.jpg",UriKind.RelativeOrAbsolute));
            var end = new BitmapImage(new Uri("pack://application:,,,/Battleship;component/Resources/ship-end.jpg", UriKind.RelativeOrAbsolute));
            var clear = new BitmapImage(new Uri("pack://application:,,,/Battleship;component/Resources/clear.jpg", UriKind.RelativeOrAbsolute));
           
                ShipParts = new Dictionary<ShipPart, ImageSource>();

            //this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/myapp;component/Images/icon.png")));
            ShipParts.Add(ShipPart.Tank, bak);
            ShipParts.Add(ShipPart.Stern, end);
            ShipParts.Add(ShipPart.Body,clear);
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            }
        public ShipViewModel()
        {
            //
            part = ShipParts[ShipPart.Tank];
            //this.boat = boat;
            ++counter;
        }
        
        public ShipViewModel(Boat boat, int index)
        {
            part = ShipParts[ShipPart.Tank];
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
        public static   ImageSource IndetifyShipPart(int index,Direction direction,ShipType type)
        {
            if(direction==Direction.Horizontal)
            {
                if (index == 0) return ShipParts[ShipPart.Stern];
                else if (index > 0 && index < (Int32)type) return ShipParts[ShipPart.Body];
                else return ShipParts[ShipPart.Tank];
            }
            else if(direction==Direction.Vertical)
            {
                return null;
            }
            return null;
        }
        public ImageSource Part
        {
            get 
            {
//                return boat == null ?ShipParts["clear"]: part;  
                return (boat == null) ? ShipParts[ShipPart.Body] : part;
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
