using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Drawing;
namespace WpfApplication4
{
    class MainWindowViewModel : BindableBase
    {
        public ICommand ClickCommand
        {
            get
            {

                return new RelayCommand((i) =>
                {
                    var t = i as ShipViewModel;
                    if (model.Mode == GameMode.Deploy)
                    {
                        var index = leftMap.IndexOf(t);
                        var point = ConvertBack(index);
                        Boat res=null;
                        if(CanInstall(point,Direction.Horizontal,ShipType.DoubleDeck))
                            res=model.SetShip(ShipType.DoubleDeck, Direction.Horizontal, point);
                        if (res != null)
                            ConvertModel(res);
                        else
                        {
                            RemoveShip(t);
                        }
                    }
                    else
                    {
                        if (t != null)
                        {

                        }
                    }

                });
            }
        }
        public ICommand StartCommand
        {
            get
            {
                return new RelayCommand(i => Start());
            }
        }
        public ICommand ExitCommand
        {
            get
            {
                return new RelayCommand(i => exit());
            }
        }
        public Boolean IsPlayerAutoSet
        {
            set
            {
                model.PlayerDeploy = value ? Deploy.Auto : Deploy.Manual;
            }
        }
        public List<ShipViewModel> Collection
        {
            get
            {
                return leftMap;
            }
        }
        public List<ShipViewModel> GameCollection
        {
            get
            {
                return rightMap;
            }
        }
        List<ShipViewModel> leftMap;
        List<ShipViewModel> rightMap;
        Action exit;
        GameModel model;

        public Boolean CanInstall(Point forCheck, Direction direction, ShipType size)
        {
            switch (direction)
            {
                case Direction.Horizontal:
                    {
                        var result = ConvertCoordinate(forCheck);
                        var tempResult = ConvertBack(result + (Int32)size);
                        if (forCheck.Y != tempResult.Y) return false;
                        break;
                    }
                case Direction.Vertical:
                    {
                        var result = ConvertCoordinate(forCheck);
                        var tempResult = ConvertBack(result + ((Int32)size)*10);
                        if (forCheck.X != tempResult.X) return false;
                        break;
                    }
                default:
                    break;
            }
            return true;



        }

        public MainWindowViewModel(Action act)
        {
            exit = act;
            model = new GameModel();
            leftMap = new List<ShipViewModel>();
            rightMap = new List<ShipViewModel>();
            for (int i = 0; i < 100; ++i)
            {
                leftMap.Add(new ShipViewModel());
                //rightMap.Add(new ShipViewModel());
            }

        }
        private void ConvertModel(Boat boat)
        {
            var res = ConvertCoordinate(boat.Cord);
            for (var i = 0; i < boat.Body.Length; ++i)
            {
                if (boat.Direction == Direction.Horizontal)
                {
                    leftMap[res + i].Boat = boat;
                    leftMap[res + i].IndexOfPart = i;
                    leftMap[res + i].Refresh();
                }
                else
                {
                    leftMap[res + (i * 10)].Boat = boat;
                    leftMap[res + (i * 10)].IndexOfPart = i;
                    leftMap[res + (i * 10)].Refresh();
                }
            }
        }

        private Int32 ConvertCoordinate(Point point)
        {
            return point.Y * 10 + point.X;
        }
        private Point ConvertBack(int index)
        {
            Int32 y = index / 10;
            Int32 x = index - (y * 10);
            return new Point(x, y);
        }


        private void Start()
        {
            foreach (var item in leftMap)
            {
                item.Boat = null;
            }
            foreach (var item in rightMap)
            {
                item.Boat = null;
            }
            model.Restart();
        }

        private void RemoveShip(ShipViewModel forDelete)
        {

            leftMap.ForEach(p =>
            {
                if (p.Boat == forDelete.Boat)
                    p.Boat = null;
            });
            model.Delete(forDelete.Boat);
        }
        
    }
}
