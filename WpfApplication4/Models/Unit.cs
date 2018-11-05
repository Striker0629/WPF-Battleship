using System;
using System.Drawing;

namespace Battleship.Models
{
    abstract class Unit
    {
        protected Point boatCords;
        public Unit(Point cord)
        {
            boatCords = cord;
        }
        public abstract Boolean Exists(Point cord);
        public abstract Boolean Exists(Point cord,Int32 size);
        
        public Point Cord
        {
            get { return boatCords; }
        }
        
    }
}
