using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RoutFinding
{
    class RouteFinder
    {

        public RouteFinder(bool[,] field)
        {
            _field = (bool[,])field.Clone();
            _width = _field.GetLength(0);
            _height = field.GetLength(1);
        }

        public  bool[,] FindRoute(Point initPoint, Point finalPos)
        {
 
            Point currentPosition = initPoint;
            Point previousPoint = initPoint;
            int step = 0;
            bool[,] exceptPoints = new bool[_width, _height];
            bool[,] routePoints = new bool[_width, _height];
            InitExtraField(exceptPoints, routePoints);
            routePoints[initPoint.X, initPoint.Y] = true;

            while (!(currentPosition.X == finalPos.X && currentPosition.Y == finalPos.Y))
            {
                Point nextStep = GetCoordNextStep(currentPosition, routePoints, exceptPoints);
                if (!nextStep.IsEmpty)
                {
                    step++;
                    currentPosition = nextStep;
                    routePoints[nextStep.X, nextStep.Y] = true;
                }
                else
                {
                    if (previousPoint != initPoint)
                    {
                        exceptPoints[nextStep.X, nextStep.Y] = true;
                        currentPosition = previousPoint;
                        step++;
                    }
                    else
                    {
                        routePoints = null;
                        break;
                    }
                }

            }

            return routePoints;
        }



        private Point GetCoordNextStep(Point currPoint, bool[,] route, bool[,] excPoints)
        {
            Point nextStep = new Point();
            Point top = new Point(currPoint.X, currPoint.Y - 1);            
            Point bottom = new Point(currPoint.X, currPoint.Y + 1);
            Point left = new Point(currPoint.X - 1, currPoint.Y);
            Point right = new Point(currPoint.X + 1, currPoint.Y);

            if (IsFreePoint(top, route, excPoints))
            {
                nextStep = top;
            }
            else if (IsFreePoint(bottom, route, excPoints))
            {
                nextStep = bottom;
            }
            else if (IsFreePoint(left, route, excPoints))
            {
                nextStep = left;
            }
            else if (IsFreePoint(right, route, excPoints))
            {
                nextStep = right;
            }
            return nextStep;
        }

        private bool IsRoute(Point point, bool[,] route)
        {         
            return route[point.X, point.Y];
        }

        private bool IsExceptPoint(Point point, bool[,] excPoints)
        {            
            return excPoints[point.X, point.Y];
        }

        private bool IsField(Point point)
        {
            return (point.X >= 0) && (point.X < _field.GetLength(0)) && (point.Y >= 0) && (point.Y < _field.GetLength(1));
        }


        private bool IsFreePoint(Point point, bool[,] route, bool[,] excPoints)
        {          
            return (IsField(point)) && (!IsRoute(point, route)) && (!IsExceptPoint(point, excPoints)) && (_field[point.X, point.Y]);
        }


        private void InitExtraField(bool[,] exceptPoints, bool[,] routePoints)
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    exceptPoints[j, i] = false;
                    routePoints[j, i] = false;
                }
            }
        }


        private readonly bool[,] _field;
        private readonly int _width;
        private readonly int _height;
    }
}
