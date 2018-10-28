using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WpfApplication4
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
        Direction shipDirection;
        ShipType type;
        Boolean[] body;
        public Boat(ShipType type, Direction direct, Point cord)
            : base(cord)
        {
            body = new Boolean[(int)type];
            this.type = type;
            shipDirection = direct;
        }
        public override Boolean Exists(Point cord)
        {

            if (shipDirection == Direction.Horizontal)
            {

                for (int i = 0; i < body.Length; ++i)
                {
                    if (cord.Y == base.Cord.Y && base.Cord.X + i == cord.X) return true;
                }
            }
            else
            {
                //+i Необходим для проверки всего диапозона
                //добавить проверку с учетом пересечения


                for (int i = 0; i < body.Length; ++i)
                {
                    if (cord.X == base.Cord.X && cord.Y == base.Cord.Y + i) return true;
                    //) return true;
                }
                    
            }
            return false;
        }
        public override Boolean Exists(Point cord,Int32 resize) 
        {

            if (shipDirection == Direction.Horizontal)
            {
                for (int j = 0; j < resize; ++j)
                {
                    for (int i = 0; i < body.Length; ++i)
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
                    for (int i = 0; i < body.Length; ++i)
                    {
                        if (cord.X == base.Cord.X && cord.Y+j == base.Cord.Y + i) return true;
                        //) return true;

                    }
            }
            return false;
        }

        public DamageType TryDamage(Point cord)
        {
            if (shipDirection == Direction.Horizontal)
            {

                for (int i = 0; i < body.Length; ++i)
                {
                    if (cord.X == base.Cord.X + i && body[i])
                    {
                        body[i] = false;
                        return body.Contains(true) ? DamageType.Damaged : DamageType.Destroyed;
                    }
                }
            }
            else
            {
                for (int i = 0; i < body.Length; ++i)
                {
                    if (cord.Y == base.Cord.Y + i && body[i])
                    {
                        body[i] = false;
                        return body.Contains(true) ? DamageType.Damaged : DamageType.Destroyed;

                    }
                }
            }
            return DamageType.None;
        }
        #region Properties
        public Boolean[] Body
        {
            get
            {
                return body;
            }
            set
            {
                body = value;
            }

        }
        public Direction Direction
        {
            get
            {
                return shipDirection;
            }
        }
        #endregion
    }
}
