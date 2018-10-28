using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication4
{
    class ShipViewModel:BindableBase
    {
        static int counter = 0;
        Boat boat;
        int currentPart;
        string forwrite;
        public ShipViewModel()
        {
            forwrite = counter.ToString();
            ++counter;
        }
        
        public ShipViewModel(Boat boat, int index)
        {
            
            this.boat = boat;
            this.currentPart = index;
            forwrite = index.ToString();
        }
        internal void Refresh()
        {
            OnPropertyChanged("Part");
        }
        public String Part
        {
            get 
            {
                return boat == null ? forwrite : "*" ;  
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
        public override string ToString()
        {
            return forwrite;
        }
        
    }
}
