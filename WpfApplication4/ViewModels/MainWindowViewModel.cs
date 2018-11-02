using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Drawing;
using Battleship.Models;

namespace Battleship.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        #region Properties
        public ICommand ClickCommand
        {
            get
            {

                return new RelayCommand((i) =>
                {
                    var t = i as ShipViewModel;

                    // System.Windows.Forms.MessageBox.Show(String.Format($"{t.Boat.Cord.X},{t.Boat.Cord.Y}"));
                    var index = leftMap.IndexOf(t);
                    var point = ConvertBack(index);
                    Boat res = null;
                    
                        if (CanInstall(point, selectedDirection, selectedType))
                            res = model.SetShip(selectedType, selectedDirection, point);
                        if (res != null)
                            ConvertModel(res);
                    
                        //else
                        //{
                        //    RemoveShip(t);
                        //}
                    

                    //else
                    //{
                    //    if (t != null)
                    //    {

                    //    }
                    //}

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
        public ShipType ShipSize
        {
            get
            {
                return selectedType;
            }
            set
            {
                selectedType = value;
            }
        }
        public Boolean OneDeckSelected
        {
            set
            {
                if (value)
                {
                    selectedType = ShipType.OneDeck;
                }
            }
        }
        public Boolean DoubleDeckSelected
        {
            set
            {
                if (value)
                {
                    selectedType = ShipType.DoubleDeck;
                }
            }
        }
        public Boolean ThreeDeckSelected
        {
            set
            {
                if (value)
                {
                    selectedType = ShipType.ThreeDeck;
                }
            }
        }
        public Boolean FourDeckSelected
        {
            set
            {
                if (value)
                {
                    selectedType = ShipType.FourDeck;
                }
            }
        }
        public Boolean HorizontalSelected
        {

            set
            {
                if (value)
                {
                    selectedDirection =  Direction.Horizontal;
                }
            }
        }
        public Boolean VerticalSelected
        {
            set
            {
                if (value)
                {
                    selectedDirection = Direction.Vertical;
                }
            }
        }
        #endregion

        private List<ShipViewModel> leftMap;
        private List<ShipViewModel> rightMap;
        private Action exit;
        private GameModel model;
        private ShipType selectedType;
        private Direction selectedDirection;
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
                        var tempResult = ConvertBack(result + ((Int32)size) * 10);
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
