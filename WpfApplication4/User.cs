using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace WpfApplication4
{
    class User
    {
        private List<Boat> userBoats;
       
        public User()
        {
            for (int i = 0; i < 5; ++i)
            {

                userBoats.Add(new Boat(ShipType.OneDeck,Direction.Vertical, new Point(2, 2)));
            }
        }

    }
}
