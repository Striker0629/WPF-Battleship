using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace WpfApplication4
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
