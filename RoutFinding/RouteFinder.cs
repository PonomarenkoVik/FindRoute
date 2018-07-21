using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RoutFinding
{
    public delegate void Show(object sender, RouteShowEventArgs args);
    class RouteFinder
    {
        public RouteFinder(Cell[,] field, Point init, Point finish)
        {
            _field = (Cell[,])field.Clone();
            InitPoint = init;
            FinishPoint = finish;
            _width = _field.GetLength(0);
            _height = field.GetLength(1);
        }
        public event Show ShowEvent;

        public Point InitPoint { get; set; }
        public Point FinishPoint { get; set; }

        public  Cell[,] FindRoute()
        {
            List<Point> route = new List<Point> {InitPoint};
            int index = 0;
            
           
            
            _field[InitPoint.X, InitPoint.Y] = Cell.Route;

            while (!(route[index].X == FinishPoint.X && route[index].Y == FinishPoint.Y))
            {

                Point nextStep = GetCoordNextStep(route[index]);
                if (!nextStep.IsEmpty)
                {
                    index++;                   
                    route.Add(nextStep);
                    _field[nextStep.X, nextStep.Y] = Cell.Route;
                    if (ShowEvent != null)
                    {
                        ShowEvent(this, new RouteShowEventArgs(((Cell[,])_field.Clone()), route[index], InitPoint, FinishPoint));
                    }
                }
                else
                {
                    if (route[index - 1] != InitPoint)
                    {
                        _field[route[index].X, route[index].Y] = Cell.Excep;
                        //_field[route[index - 1].X, route[index - 1].Y] = Cell.Route;
                        route.RemoveAt(route.Count - 1);
                        index--;

                        if (ShowEvent != null)
                        {
                            ShowEvent(this, new RouteShowEventArgs(((Cell[,])_field.Clone()), route[index], InitPoint, FinishPoint));
                        }
                    }
                    else
                    {
                        return null;
                    }
                }

            }

            return _field;
        }



        private Point GetCoordNextStep(Point currPoint)
        {
            Point nextStep = new Point();
            Point top = new Point(currPoint.X, currPoint.Y - 1);            
            Point bottom = new Point(currPoint.X, currPoint.Y + 1);
            Point left = new Point(currPoint.X - 1, currPoint.Y);
            Point right = new Point(currPoint.X + 1, currPoint.Y);

            if (IsField(top) && _field[top.X, top.Y] == Cell.Free)
            {
                nextStep = top;
            }
            else if (IsField(bottom) && _field[bottom.X, bottom.Y] == Cell.Free)
            {
                nextStep = bottom;
            }
            else if (IsField(left) && _field[left.X, left.Y] == Cell.Free)
            {
                nextStep = left;
            }
            else if (IsField(right) && _field[right.X, right.Y] == Cell.Free)
            {
                nextStep = right;
            }           
            return nextStep;
        }

        private bool IsField(Point point)
        {
            return (point.X >= 0) && (point.X < _field.GetLength(0)) && (point.Y >= 0) && (point.Y < _field.GetLength(1));
        }

        private readonly Cell[,] _field;
        private readonly int _width;
        private readonly int _height;
    }
}
