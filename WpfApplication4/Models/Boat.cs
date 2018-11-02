using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Battleship.Models
{

    enum DamageType
    {
        None, Damaged, Destroyed
    }
    enum ShipType : int
    {
        OneDeck=1, DoubleDeck, ThreeDeck, FourDeck
    }

    enum ShipPart
    {
        Body, Stern, Tank
    }

    enum Direction
    {
        Horizontal, Vertical,HorizontalReverse,VerticalReverse
    }
    class Boat : Unit
    {
        #region Properties
        public Boolean[] Body { get; set; }
        public Direction Direction { get; private set; }

        #endregion

        private ShipType type;
        //private Boolean[] Body;
        public Boat(ShipType type, Direction direct, Point cord)
            : base(cord)
        {
            Body = new Boolean[(int)type];
            this.type = type;
            Direction = direct;
        }
        public override Boolean Exists(Point cord)
        {

            if (Direction == Direction.Horizontal)
            {

                for (int i = 0; i < Body.Length; ++i)
                {
                    if (cord.Y == base.Cord.Y && base.Cord.X + i == cord.X) return true;
                }
            }
            else
            {
                //+i Необходим для проверки всего диапозона
                //добавить проверку с учетом пересечения


                for (int i = 0; i < Body.Length; ++i)
                {
                    if (cord.X == base.Cord.X && cord.Y == base.Cord.Y + i) return true;
                    //) return true;
                }
                    
            }
            return false;
        }
        public override Boolean Exists(Point cord,Int32 resize) 
        {

            if (Direction == Direction.Horizontal)
            {
                for (int j = 0; j < resize; ++j)
                {
                    for (int i = 0; i < Body.Length; ++i)
                    {
                        if (cord.Y == base.Cord.Y && base.Cord.X + i == cord.X+j) return true;
                    }
                }
            }
            else
            {
                //+i Необходим для проверки всего диапозона
                //добавить проверку с учетом пересечения

                for (int j = 0; j < resize;++j )
                    for (int i = 0; i < Body.Length; ++i)
                    {
                        if (cord.X == base.Cord.X && cord.Y+j == base.Cord.Y + i) return true;
                        //) return true;

                    }
            }
            return false;
        }

        public DamageType TryDamage(Point cord)
        {
            if (Direction == Direction.Horizontal)
            {

                for (int i = 0; i < Body.Length; ++i)
                {
                    if (cord.X == base.Cord.X + i && Body[i])
                    {
                        Body[i] = false;
                        return Body.Contains(true) ? DamageType.Damaged : DamageType.Destroyed;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Body.Length; ++i)
                {
                    if (cord.Y == base.Cord.Y + i && Body[i])
                    {
                        Body[i] = false;
                        return Body.Contains(true) ? DamageType.Damaged : DamageType.Destroyed;

                    }
                }
            }
            return DamageType.None;
        }
       
    }
}
