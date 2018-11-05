using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Battleship.Models;
namespace Battleship.ViewModels
{
    class ShipViewModel : BindableBase
    {
        public ImageSource Part
        {
            get
            {
                //                return boat == null ?ShipParts["clear"]: part;  
                return boat == null ? empty : part;
                //return forwrite;
            }
            set
            {
                part = value;
            }
        }
        public Boat Boat
        {
            set
            {
                boat = value;

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

        static int counter = 0;
        private Boat boat;
        private int currentPart;
        //string forwrite;
        static private Dictionary<ShipPart, ImageSource> horizontalShipParts;
        static private Dictionary<ShipPart, ImageSource> verticalShipParts;
        static private ImageSource empty;
        private ImageSource part;
        static ShipViewModel()
        {
            try
            {
                var bak = new BitmapImage(new Uri("pack://application:,,,/Battleship;component/Resources/h-ship-bak.jpg", UriKind.RelativeOrAbsolute));
                var end = new BitmapImage(new Uri("pack://application:,,,/Battleship;component/Resources/h-ship-end.jpg", UriKind.RelativeOrAbsolute));
                var clear = new BitmapImage(new Uri("pack://application:,,,/Battleship;component/Resources/clear.jpg", UriKind.RelativeOrAbsolute));
                horizontalShipParts = new Dictionary<ShipPart, ImageSource>();
                verticalShipParts = new Dictionary<ShipPart, ImageSource>();
                verticalShipParts.Add(ShipPart.Tank, new BitmapImage(new Uri("pack://application:,,,/Battleship;component/Resources/v-ship-bak.jpg", UriKind.RelativeOrAbsolute)));
                verticalShipParts.Add(ShipPart.Stern, end);
                verticalShipParts.Add(ShipPart.Body, end);
                horizontalShipParts.Add(ShipPart.Tank, bak);
                horizontalShipParts.Add(ShipPart.Stern, end);
                horizontalShipParts.Add(ShipPart.Body, end);
                empty = clear;

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public ShipViewModel()
        {
            //
            //part = horizontalShipParts[ShipPart.Tank];
            //this.boat = boat;
            ++counter;
        }

        public ShipViewModel(Boat boat, int index)
        {
           // part = horizontalShipParts[ShipPart.Tank];
            this.boat = boat;
            this.currentPart = index;
            //forwrite = index.ToString();
        }

        internal void Refresh()
        {
            OnPropertyChanged("Part");
        }

        public static ImageSource IndetifyShipPart(int index, Direction direction, ShipType type)
        {
            //var Parts = direction == Direction.Horizontal ? horizontalShipParts : verticalShipParts;

            if (direction == Direction.Horizontal)
            {
                if (type == ShipType.OneDeck) return horizontalShipParts[ShipPart.Tank];
                if (index == 0) return horizontalShipParts[ShipPart.Stern];
                if (index > 0 && index < (Int32)type - 1) return horizontalShipParts[ShipPart.Body];
                else return horizontalShipParts[ShipPart.Tank];
            }
            else if (direction == Direction.Vertical)
            {
                if (type == ShipType.OneDeck) return verticalShipParts[ShipPart.Tank];
                if (index == 0) return verticalShipParts[ShipPart.Stern];
                if (index > 0 && index < (Int32)type - 1) return verticalShipParts[ShipPart.Body];
                else return verticalShipParts[ShipPart.Tank];
            }
            return null;
        }

       

    }
}
