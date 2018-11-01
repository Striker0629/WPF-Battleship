using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace WpfApplication4
{

    enum Deploy
    {
        Auto, Manual
    }
    enum GameMode
    {
        Deploy, Game
    }

    class GameModel
    {
        List<Unit> leftMap;
        List<Unit> rightMap;
        Boolean isFirstUserTurn;
        Deploy leftUser;
        Deploy rightUser;
        GameMode mode;
        public GameModel()
        {
            leftMap = new List<Unit>();
            rightMap = new List<Unit>();
            leftUser = rightUser = Deploy.Auto;
            //SetShip(ref leftMap,Deploy.Auto);
            isFirstUserTurn = true;
            mode = GameMode.Deploy;
        }
        public void Restart()
        {
            leftMap.Clear();
            rightMap.Clear();
            //SetShip(ref leftMap, leftUser);
            //SetShip(ref rightMap, Deploy.Auto);
        }

        private void SetShip(ref List<Unit> forset, Deploy user)
        {
            if (user == Deploy.Auto)
            {
                //leftMap.Add(new Boat(ShipType.DoubleDeck, Direction.Horizontal, new Point(3, 2)));
                //leftMap.Add(new Boat(ShipType.OneDeck, Direction.Vertical, new Point(5, 1)));
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Enter");
            }
        }
        public Boat SetShip(ShipType type, Direction dir, Point point)
        {
            if (/*!Contains(point, (Int32)type) &&*/ !HaveShip(point))
            {
                var t = new Boat(type, dir, point);
                leftMap.Add(t);
                return t;
            }
            return null;
        }



        public Boolean Contains(Point point, Int32 shipSize)
        {
            List<Unit> map = isFirstUserTurn ? leftMap : rightMap;
            foreach (var i in map)
            {
                if (i.Exists(point, shipSize))
                    return true;
            }
            return false;
        }


        public void Shoot(Point point)
        {
            Boat boat;
            List<Unit> map = isFirstUserTurn ? leftMap : rightMap;
            boat = map.Find((p) =>
            {
                if (p is Boat)
                {
                    return (p as Boat).Exists(point);
                }
                else
                    return false;
            }) as Boat;

            if (boat != null)
            {
                if (boat.TryDamage(point) == DamageType.None) isFirstUserTurn = !isFirstUserTurn;
            }


        }

        public Boolean IsGameOver()
        {
            if (leftMap.Count == 0 || rightMap.Count == 0) return true;
            return false;
        }
        public Deploy PlayerDeploy
        {
            set
            {
                leftUser = value;
            }
        }

        public IEnumerable<Unit> LeftMap
        {
            get
            {
                return leftMap;
            }
        }

        public IEnumerable<Unit> RightMap
        {
            get
            {
                return rightMap;
            }
        }
        public GameMode Mode
        {
            get
            {
                return mode;
            }
        }

        private List<Unit> ActiveField()
        {
            var map = isFirstUserTurn ? leftMap : rightMap;
            return map;
        }

        /// <summary>
        /// Доработать проверки корабля, особенно ответственную за Y
        public Boolean HaveShip(Point check)
        {
            List<Unit> map = ActiveField();
            //{
        //    System.Windows.Forms.MessageBox.Show(String.Format("Check X == {0},Check  Y =={1}", check.X, check.Y));
            foreach (var item in map)
            {
                var tempShip = item as Boat;
                if (tempShip != null)
                {

                    //if ((item.Cord.X == check.X && (item.Cord.Y + 1 == check.Y || item.Cord.Y - 1 == check.Y || item.Cord.Y == check.Y))
                    //    || item.Cord.Y == check.Y && (item.Cord.X == check.X || item.Cord.X + tempShip.Body.Length == check.X) ||
                    //    item.Cord.Y == check.Y && item.Cord.X - 1 == check.X || item.Cord.X + tempShip.Body.Length + 1 == check.X)
                 
                    if ((item.Cord.X == check.X - 1 && item.Cord.Y == check.Y) ||
                        (item.Cord.X == check.X && item.Cord.Y == check.Y + 1) ||
                        (item.Cord.X == check.X + 1 && item.Cord.Y == check.Y + 1) ||
                        (item.Cord.X == check.X && item.Cord.Y == check.Y - 1) || 
                        (item.Cord.X==check.X+1 && item.Cord.Y==check.Y-1) ||
                        (item.Cord.X==check.X-1 && item.Cord.Y==check.Y-1) ||
                        (item.Cord.X + tempShip.Body.Length == check.X && item.Cord.Y == check.Y) ||
                        (item.Cord.X + tempShip.Body.Length == check.X && item.Cord.Y == check.Y - 1) ||
                        (item.Cord.X + tempShip.Body.Length == check.X && item.Cord.Y == check.Y + 1) || 
                        (item.Cord.X-tempShip.Body.Length==check.X && item.Cord.Y==check.Y) || 
                         (item.Cord.X - tempShip.Body.Length == check.X && item.Cord.Y == check.Y - 1) ||
                        (item.Cord.X - tempShip.Body.Length == check.X && item.Cord.Y == check.Y + 1))
                    {
                        return true;
                    }
                }
            }
            return false;

            // (gameAr[coordX - 1][coordY - 1] == toc)) &&
            //((coordX == 1) || (gameAr[coordX - 1][coordY] == toc)) &&
            //    ((coordX == 1 || coordY == 10) || (gameAr[coordX - 1][coordY + 1] == toc)) &&
            //    ((coordY == 1) || (gameAr[coordX][coordY - 1] == toc)) &&
            //    (gameAr[coordX][coordY] == toc) &&
            //    ((coordY == 10) || (gameAr[coordX][coordY + 1] == toc)) &&
            //    ((coordX == 10 || coordY == 1) || (gameAr[coordX + 1][coordY - 1] == toc)) &&
            //    ((coordX == 10) || (gameAr[coordX + 1][coordY] == toc)) &&
            //    ((coordX == 10 || coordY == 10) || (gameAr[coordX + 1][coordY + 1] == toc))
            //    );

        }
        /// </summary>
        /// <param name="boat"></param>
        internal void Delete(Boat boat)
        {
            leftMap.Remove(boat);
        }
    }
}
